using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.Models;
using Additive_DB_Refresh.Services;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Additive_DB_Refresh.Extensions
{
	public static class TargetContextExtensions
	{
		public static bool HasIdentity<T>(this TargetContext context) where T : class
		{
			var set = context.Set<T>();
			foreach (var key in context.Model.FindEntityType(typeof(T)).GetKeys())
			{
				foreach (var property in key.Properties)
				{
					if (property.ValueGenerated == Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd)
					{
						return true;
					}
				}
			}

			return false;
		}
		public static IProperty? GetIdentityColumn<T>(this TargetContext context) where T : class
		{
			var set = context.Set<T>();
			foreach (var key in context.Model.FindEntityType(typeof(T)).GetKeys())
			{
				foreach (var property in key.Properties)
				{
					if (property.ValueGenerated == Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd)
					{
						return property;
					}
				}
			}
			return null;
		}
		public static string GetTableName<T>(this TargetContext context) where T : class {
			var tbl = context.Model.FindEntityType(typeof(T));
			string schema = tbl.GetSchema().IsNullOrEmpty() ? "dbo" : tbl.GetSchema();
			return $"[{schema}].[{tbl.GetTableName()}]";
		}
		public static async Task SaveChangesAsync<T>(this TargetContext context) where T : class
		{
			var set = context.Set<T>();
			
			if (context.HasIdentity<T>())
			{
				using (var transaction = await context.Database.BeginTransactionAsync())
				{
					await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {context.GetTableName<T>()} ON");
					await context.SaveChangesAsync();
					await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {context.GetTableName<T>()} OFF");
					await transaction.CommitAsync();

				}
			}
			else
			{
				await context.SaveChangesAsync();
			}
		}
		public static async Task SaveRangeAsync<T>(this TargetContext context, List<T> entityList) where T : class {
			var set = context.Set<T>();
			bool hasChanges = false;
			
			foreach (var entity in entityList)
			{
				if (!set.Any(s => s.Equals(entity)))
				{
					set.Attach(entity);
					context.Entry(entity).State = EntityState.Added;
					//set.Add(entity);
					if (!hasChanges) { 
						hasChanges = true;
					}
				}
			}
			if (hasChanges)
			{
				//await context.SaveChangesAsync<T>();
				await context.BulkSaveChangesAsync(bulkConfig: new BulkConfig() { BatchSize = 1000, SqlBulkCopyOptions = SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls  });
				context.ChangeTracker.Clear();
			}
		}
		public static async Task IdentityInsertOn<T>(this TargetContext context) where T : class
		{
			if (context.HasIdentity<T>()) {
				await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {context.GetTableName<T>()} ON");
			}
		}
		public static async Task IdentityInsertOff<T>(this TargetContext context) where T : class
		{
			if (context.HasIdentity<T>())
			{
				await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {context.GetTableName<T>()} OFF");
			}
		}
		public static async Task InsertUpdateAsync<T>(this TargetContext target, IQueryable<T> sourceQuery, int batchSize = 1000) where T : class { 
			  await Migrator<T>.MigrateDataInsertUpdateAsync(sourceQuery,target,batchSize);
		}
		public static async Task InsertAsync<T>(this TargetContext target, IQueryable<T> sourceQuery, int batchSize = 1000) where T : class
		{
			await Migrator<T>.MigrateDataInsertAsync(sourceQuery, target, batchSize);
		}
		public static async Task BulkInsertAsync<T>(this TargetContext target, SourceContext source, IQueryable<T> iqueryable) where T : class { 
			await Migrator<T>.BulkInsertData(target,source,iqueryable.ToQueryString());
		}
		#region Table Management
		public static List<string> GetAllTableNames(this TargetContext target) {
			return target.Model.GetEntityTypes().Select(t => $"[{(t.GetSchema() ?? "dbo")}].[{t.GetTableName()}]").ToList();
		}
		public async static Task<List<string>> GetAllMissingTableNamesAsync(this TargetContext target) { 
			List<string> dbTables =  await target.Database.SqlQueryRaw<string>("SELECT '['+ t.TABLE_SCHEMA +'].['+ t.TABLE_NAME +']' AS UserTables FROM INFORMATION_SCHEMA.TABLES AS t WHERE t.TABLE_TYPE LIKE 'BASE TABLE'").ToListAsync();
			List<string> modelTables = target.GetAllTableNames();
			List<string> missingTables = new List<string>();
			foreach (var modelTable in modelTables) {
				if (!dbTables.Any(t => t.Equals(modelTable))) 
				{
					missingTables.Add(modelTable);
				}
			}
			return missingTables;
		}
		public static async Task ClearAndReseedAllTablesAsync(this TargetContext target) {
			var tables = target.GetAllTableNames();

			foreach (var t in tables)
			{
				try
				{
					string cmd = $"DELETE FROM {t}";
					await target.Database.ExecuteSqlRawAsync(cmd);

					cmd = $"DBCC CHECKIDENT ('{t}', RESEED, 0);";
					await target.Database.ExecuteSqlRawAsync(cmd);
				}
				catch { 
					//	Do not return error, allow non-ident tables fail reseed
					//		continue process
				}
		
			}
		}
		public static async Task TruncateAllTablesAsync(this TargetContext target) {
			var tables = target.GetAllTableNames();

			foreach (var t in tables)
			{
			
				string cmd = $"TRUNCATE TABLE {t}";
				await target.Database.ExecuteSqlRawAsync(cmd);

			}
		}
		#endregion Table Management		
		#region SQL Script based operations
		public static async Task TurnOffForeignKeysAsync(this TargetContext target)
		{
			await RunSQLFileAsync(target, "ForeignKeys_TurnOff.sql");
		}
		public static async Task TurnOnForeignKeysAsync(this TargetContext target)
		{
			await RunSQLFileAsync(target, "ForeignKeys_TurnOn.sql");
		}
		public static async Task DropForeignKeysAsync(this TargetContext target) {
			await RunSQLFileAsync(target, "ForeignKeys_Drop.sql");
		}
		public static async Task AddForeignKeysAsync(this TargetContext target) {
			await RunSQLFileAsync(target, "ForeignKeys_Add.sql");
		}
		public static async Task DisableTriggersAsync(this TargetContext target)
		{
			await RunSQLFileAsync(target, "Triggers_Disable.sql");
		}
		public static async Task EnableTriggersAsync(this TargetContext target)
		{
			await RunSQLFileAsync(target, "Triggers_Enable.sql");
		}
		private static async Task RunSQLFileAsync(this TargetContext target, string filename)
		{
			string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SQL", filename);

			string sql = String.Empty;

			using (StreamReader r = new StreamReader(filepath))
			{
				sql = r.ReadToEnd();
			}
			await target.Database.ExecuteSqlRawAsync(sql);
		}

		#endregion SQL Script based operations
	}
}
