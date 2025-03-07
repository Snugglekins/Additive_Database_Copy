using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.DataStreams;
using Additive_DB_Refresh.Extensions;
using Additive_DB_Refresh.Repositories;
using Additive_DB_Refresh.Utilities;
using Azure.ResourceManager;
using Azure.ResourceManager.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Additive_DB_Refresh.Services
{
	public class DatabaseCopyService : IDatabaseCopyService
	{
		private List<DbCopyConfig> DbConfigs { get; }
		protected IHostApplicationLifetime appLifetime { get; set; }
		private IConfiguration Configuration { get; }
		private CancellationTokenSource cts { get; set; }
		int? exitCode;
		ArmClient ArmClient { get; }
		private ILogger<DatabaseCopyService> Logger { get; }
		private SourceContext Source { get; }
		private DatabaseCopierFactory CopierFactory { get; }
			
		public DatabaseCopyService(IConfiguration _configuration
								, ILogger<DatabaseCopyService> logger
								, IHostApplicationLifetime _appLifetime
								, ArmClient armClient, List<DbCopyConfig> dbConfigs
								, DatabaseCopierFactory databaseCopierFactory
								, SourceContext source)
								
		{
			appLifetime = _appLifetime;
			cts = new CancellationTokenSource();
			Configuration = _configuration;
			Logger = logger;
			ArmClient = armClient;
			DbConfigs = dbConfigs;
			CopierFactory = databaseCopierFactory;
			Source = source;

		}
		public Task StartAsync(CancellationToken cancellationToken)
		{
			Logger.LogDebug("Starting DatabaseCopyService");
			appLifetime.ApplicationStarted.Register(() =>
			{
				try
				{
					RunAsync(cts.Token).GetAwaiter().GetResult();
					exitCode = 0;
				}
				catch (TaskCanceledException)
				{
					exitCode = 0;
				}
				catch (Exception ex)
				{
					exitCode = 1;
					Logger.LogError($"Unhandled exception during DatabaseCopyService: {ex.Message}");
					throw;
				}
				finally
				{
					appLifetime.StopApplication();
				}
			}
				);

			appLifetime.ApplicationStopping.Register(() =>
			{
				cts.Cancel();
			});

			return Task.CompletedTask;
		}
		public Task StopAsync(CancellationToken cancellationToken)
		{
			Logger.LogDebug("Stopping DatabaseCopyService");
			appLifetime.StopApplication();
			return Task.CompletedTask;
		}
		public async Task RunAsync(CancellationToken cancellationToken)
		{
			var starttime = DateTime.Now;
			Logger.LogInformation($"Data transfer starting : {starttime}");
			
			//Validate database settings
			if (!SettingsValid()) {
				Logger.LogError("Database settings invalid. Please cehck and try again.");
				return;
			}

			//Copy empty model database
			List<DatabaseCopier> databaseCopiers = new List<DatabaseCopier>();

			List<Task<ArmOperation<SqlDatabaseResource>>> creationTasks = new List<Task<ArmOperation<SqlDatabaseResource>>>();

			foreach (DbCopyConfig config in DbConfigs) {
				DatabaseCopier databaseCopier = await CopierFactory.CreateDatabaseCopier(config);
				databaseCopiers.Add(databaseCopier);
				creationTasks.Add(databaseCopier.CopyModelDatabase());

			}

			await Task.WhenAll(creationTasks);//Await all database copy processes to complete before proceeding

			//Move data for each copy
			foreach (DatabaseCopier databaseCopier in databaseCopiers)
			{

				try
				{
					List<string> missingTables = await databaseCopier.GetAllMissingTablesAsync();
					
					if (missingTables != null && missingTables.Count > 0)
					{
						foreach (string missingTable in missingTables)
						{
							Logger.LogError($"Database missing table {missingTable}");
						}
					}
					else
					{
						await databaseCopier.CopyData(true);
						await databaseCopier.AddApplicationUsersAsync();
					}
				}
				catch (Exception e)
				{
					Logger.LogError(e.ToString());
					throw;
				}
				finally { 
					var endtime = DateTime.Now;
					TimeSpan ts = endtime - starttime;
					Logger.LogInformation("Process completed at {endtime}  ; runtime {ts}", endtime, ts);
				}
			}
		}
		public void Dispose()
		{

		}
		private bool SettingsValid() {
			bool valid = true;

			if (!Source.Database.CanConnect()) { 
				valid = false;
				Logger.LogError("Unable to connect to source server");
			}

			string? modelDatabaseServerResourceId = Configuration.GetValue<string>("ModelDatabaseServerResourceId");
			string? modelDatabase = Configuration.GetValue<string>("ModelDatabase");

			if (modelDatabaseServerResourceId != null && modelDatabase != null)
			{
				SqlServerResource modelServerResource = ArmClient.GetSqlServerResource(new Azure.Core.ResourceIdentifier(modelDatabaseServerResourceId));
				if (!DbManagement.DatabaseExists(modelServerResource, modelDatabase)) { 
					valid = false;
					Logger.LogError("Model Database {databaseName} not found.", modelDatabase);
				}
			}
			else { 
				valid = false;
				Logger.LogError("Model Database settings ModelDatabase and/or ModelDatabaseServerResourceId not found");
			}

			foreach (DbCopyConfig dbConfig in DbConfigs) {
				SqlServerResource destServerResourceId = ArmClient.GetSqlServerResource(new Azure.Core.ResourceIdentifier(dbConfig.DestinationDatabaseResourceId));
				if (DbManagement.DatabaseExists(destServerResourceId, dbConfig.DestinationDatabase)) {
					valid = false;
					Logger.LogError("Destination database {databaseName} already exists. Please choose another name or delete the database and run again.", dbConfig.DestinationDatabase);
				}

			}
			return valid;
		}
		
	}
}
