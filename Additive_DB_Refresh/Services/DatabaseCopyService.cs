using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.DataStreams;
using Additive_DB_Refresh.Extensions;
using Additive_DB_Refresh.Repositories;
using Additive_DB_Refresh.Utilities;
using Azure.ResourceManager;
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
		private List<DbCopyConfig> dbConfigs { get; }
		private bool copyParnterLocations { get; }
		private bool copyLinkedOrders { get; }
		private string usersList { get; }

		protected IHostApplicationLifetime appLifetime { get; set; }
		private IConfiguration configuration { get; }
		private CancellationTokenSource cts { get; set; }
		int? exitCode;
		ArmClient armClient { get; }
		private ILogger<DatabaseCopyService> logger { get; }
		private SourceContext source { get; }
		private TargetContext target { get; }
		private DatabaseCopierFactory CopierFactory { get; }
			
		public DatabaseCopyService(IConfiguration _configuration
								, ILogger<DatabaseCopyService> _logger
								, IHostApplicationLifetime _appLifetime
								, ArmClient _armClient, List<DbCopyConfig> _dbConfigs
								, DatabaseCopierFactory databaseCopierFactory)
								
		{
			appLifetime = _appLifetime;
			cts = new CancellationTokenSource();
			configuration = _configuration;
			logger = _logger;
			armClient = _armClient;
			dbConfigs = _dbConfigs;
			CopierFactory = databaseCopierFactory;

		}
		public Task StartAsync(CancellationToken cancellationToken)
		{
			logger.LogDebug("Starting DatabaseCopyService");
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
					logger.LogError($"Unhandled exception during DatabaseCopyService : {ex.Message}");
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
			logger.LogDebug("Stopping DatabaseCopyService");
			appLifetime.StopApplication();
			return Task.CompletedTask;
		}
		public async Task RunAsync(CancellationToken cancellationToken)
		{
			var starttime = DateTime.Now;
			logger.LogInformation($"Data transfer starting : {starttime}");
			foreach (DbCopyConfig config in dbConfigs)
			{
				//TODO : Create method to generate DB copies from an empty original, then use those as targets for the copy process

				try
				{
					DatabaseCopier databaseCopier = await CopierFactory.CreateDatabaseCopier(config);
					await databaseCopier.CopyData(true);
				}
				catch (Exception e)
				{
					logger.LogError(e.ToString());
					throw;
				}
				finally { 
					var endtime = DateTime.Now;
					TimeSpan ts = endtime - starttime;
					logger.LogInformation($"Process completed at {endtime}  ; runtime {ts}");
				}
			}
		}
		public void Dispose()
		{

		}

	}
}
