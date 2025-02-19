using Additive_DB_Refresh;
using Additive_DB_Refresh.DataStreams;
using Additive_DB_Refresh.Logger;
using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.Repositories;
using Additive_DB_Refresh.Services;
using Additive_DB_Refresh.Utilities;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Sql;
using Azure.ResourceManager.Sql.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
using NetTopologySuite.Geometries.Implementation;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.EntityFrameworkCore.Internal;


//ModelPartials.bat $(ProjectDir) $(ProjectName)
//TODO : Find way to set default schema in the DbContextFactory or using EF Power Core customizations

int x = 0;
Console.WriteLine(x);

var logLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

var configPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
// start with the local configuration sources
string? environment = "Local";

var configurationBuilder = new ConfigurationBuilder()
	.AddEnvironmentVariables()
	.AddJsonFile("appsettings.json", optional: false)
	.AddJsonFile($"appsettings.{environment}.json", optional: true)
	.AddJsonFile($"{configPath}/appsettings.{environment}-common.json", optional: true)
	.AddJsonFile($"{configPath}/dbcopyconfig.json", optional: false)
	.AddUserSecrets<Program>();

var config = configurationBuilder.Build();

Directory.CreateDirectory(logLocation);
var loggerFactory = BuildLoggerFactory(logLocation, "Additive_Refresh",config);

var logger = loggerFactory.CreateLogger<Program>();
logger.LogInformation("Configuring process");

List<DbCopyConfig> dbCopyConfigs = config.GetSection("DbCopyConfigs").Get<List<DbCopyConfig>>();

ArmClient armClient = new ArmClient(new DefaultAzureCredential());

logger.LogInformation(armClient.ToString());

var builder = new HostApplicationBuilder();

string targetConnectionString = config.GetValue<string>("Connection:TargetConnection");

string sourceConnectionString = config.GetValue<string>("Connection:SourceConnection");

builder.Services
		.AddHostedService<DatabaseCopyService>()
		.AddDbContextFactory<TargetContext>(options =>
			{
				options.UseSqlServer(targetConnectionString, x=> x.UseHierarchyId().UseNetTopologySuite())
					   .EnableSensitiveDataLogging();
			}, ServiceLifetime.Scoped)
		.AddDbContextFactory<SourceContext>(options =>
			{
				options.UseSqlServer(sourceConnectionString, x => x.UseHierarchyId().UseNetTopologySuite())
					   .EnableSensitiveDataLogging();
			}, ServiceLifetime.Scoped
			)
		.AddSingleton(loggerFactory)
		.AddSingleton(armClient)
		.AddSingleton(config)
		.AddScoped<SourceRepository>()
		.AddScoped<TargetRepository>()
		.AddScoped<SystemTablesStream>()
		.AddScoped<ClientStream>()
		.AddScoped<ClientLocationStream>()
		.AddSingleton<List<DbCopyConfig>>(dbCopyConfigs)
		.AddSingleton<DatabaseCopierFactory>();

builder.Services.AddSingleton<IConfiguration>(config);

var app = builder.Build();

await app.RunAsync().ConfigureAwait(true);

logger.LogInformation("Process complete");

static ILoggerFactory BuildLoggerFactory(string directory, string prefix, IConfiguration config)
{
	ServiceProvider serviceProvider = new ServiceCollection()
	.AddLogging((loggingBuilder) => loggingBuilder
	.AddConfiguration(config.GetSection("Logging"))
	//.SetMinimumLevel(LogLevel.Warning)
	//   .AddFilter("LoggingTest.Program", LogLevel.Debug)
	.AddConsole()
	.AddDebug()
	)
	.BuildServiceProvider();

	//ILoggerFactory
	ILoggerFactory loggerFactory = serviceProvider.GetService<ILoggerFactory>();
	loggerFactory.AddFile(prefix, directory);
	return loggerFactory;
}
