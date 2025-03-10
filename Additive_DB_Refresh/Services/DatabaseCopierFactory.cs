﻿using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.DataStreams;
using Additive_DB_Refresh.Extensions;
using Azure.ResourceManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additive_DB_Refresh.Services
{
	public class DatabaseCopierFactory
	{
		private IDbContextFactory<SourceContext> SourceFactory { get;}
		private IDbContextFactory<TargetContext> TargetFactory { get;}
		private ILoggerFactory LoggerFactory { get; }
		private IConfiguration Configuration { get; }
		private ArmClient ArmClient { get; }
		public DatabaseCopierFactory(IConfiguration configuration,IDbContextFactory<SourceContext> sourceFactory, IDbContextFactory<TargetContext> targetFactory, ILoggerFactory loggerFactory, ArmClient armClient) { 
			SourceFactory = sourceFactory;
			TargetFactory = targetFactory;
			LoggerFactory = loggerFactory;
			Configuration = configuration;
			ArmClient = armClient;
		}
		public async Task<DatabaseCopier> CreateDatabaseCopier(DbCopyConfig copyConfig) {
			string targetdatabase = copyConfig.DestinationDatabase;
			TargetContext target = await TargetFactory.CreateDbContextNoTimeoutAsync(targetdatabase);
			SystemTablesStream systemTablesStream = new SystemTablesStream(SourceFactory.CreateDbContext(), await TargetFactory.CreateDbContextNoTimeoutAsync(targetdatabase),LoggerFactory.CreateLogger<SystemTablesStream>());
			ClientStream clientStream = new ClientStream(SourceFactory.CreateDbContext(), await TargetFactory.CreateDbContextNoTimeoutAsync(targetdatabase), LoggerFactory.CreateLogger<ClientStream>());
			ClientLocationStream clientLocationStream = new ClientLocationStream(SourceFactory, TargetFactory, LoggerFactory.CreateLogger<ClientLocationStream>(), targetdatabase);
			DatabaseCopier databaseCopier = new DatabaseCopier(Configuration,systemTablesStream,clientStream,clientLocationStream,LoggerFactory.CreateLogger<DatabaseCopier>(), await TargetFactory.CreateDbContextNoTimeoutAsync(targetdatabase), SourceFactory.CreateDbContext(), copyConfig, ArmClient);
			return databaseCopier;
		}

	}
}
