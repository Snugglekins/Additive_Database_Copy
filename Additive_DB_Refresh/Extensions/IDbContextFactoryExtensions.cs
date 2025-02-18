using Additive_DB_Refresh.Contexts;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Additive_DB_Refresh.Extensions
{
	public static class IDbContextFactoryExtensions
	{
		public async static Task<SourceContext> CreateDbContextNoTimeoutAsync(this IDbContextFactory<SourceContext> factory) {
			var context = await factory.CreateDbContextAsync();
			context.Database.SetCommandTimeout(0);
			return context;
		}
		public async static Task<TargetContext> CreateDbContextNoTimeoutAsync(this IDbContextFactory<TargetContext> factory)
		{
			var context = await factory.CreateDbContextAsync();
			context.Database.SetCommandTimeout(0);
			return context;
		}
		public static async Task<TargetContext> CreateDbContextAsync(this IDbContextFactory<TargetContext> factory, string databaseName) {
			var context = await factory.CreateDbContextAsync();
			var csBuilder = new SqlConnectionStringBuilder(connectionString: context.Database.GetConnectionString());
			csBuilder.InitialCatalog = databaseName;
			context.Database.SetConnectionString(csBuilder.ToString());
			context.Database.SetCommandTimeout(0);
			return context;
		}

	}
}
