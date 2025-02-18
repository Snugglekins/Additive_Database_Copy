using Microsoft.EntityFrameworkCore.Internal;
using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.Models;
using Additive_DB_Refresh.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Logging;
using EFCore.BulkExtensions;
using Additive_DB_Refresh.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Additive_DB_Refresh.Repositories
{
	public class TargetRepository
	{
		private readonly ILogger<TargetRepository> logger;
		private TargetContext context;
		public TargetRepository(TargetContext _context, ILogger<TargetRepository> _logger)
		{
			_context.Database.SetCommandTimeout(0);
			context = _context;
			logger = _logger;
		}
		public async Task SaveClientAsync(Client client)
		{
			await SaveAsync(client);
			await SaveRangeAsync(client.ClientEmployees.ToList());
			await SaveRangeAsync(client.ClientLogins.ToList());
		}

		public async Task SaveClientLocationAsync(ClientLocation clientLocation)
		{
			try
			{
				await SaveAsync(clientLocation);
				await SaveMerchandiseAsync(clientLocation);
			}
			catch (Exception e)
			{
				logger.LogError(e, "SaveClientLocationAsync");
				throw;
			}
		}
		private async Task SaveMerchandiseAsync(ClientLocation clientLocation)
		{
			try
			{
				await SaveRangeAsync(clientLocation.Merchandise_ProductCategories.ToList());
				await SaveRangeAsync(clientLocation.Merchandise_ProductColors.ToList());
				await SaveRangeAsync(clientLocation.Merchandise_Products.ToList());
				await SaveRangeAsync(clientLocation.Merchandise_ProductCategoryAssociations.ToList());
				await SaveRangeAsync(clientLocation.Merchandise_ProductSizes.ToList());
				await SaveRangeAsync(clientLocation.Merchandise_Products.Where(p => p.Merchandise_Inventory != null).Select(p => p.Merchandise_Inventory).ToList());
				await SaveRangeAsync(clientLocation.Merchandise_Products.SelectMany(p => p.Merchandise_InventoryChangeLogs).ToList());
				await SaveRangeAsync(clientLocation.Merchandise_Products.SelectMany(p => p.Merchandise_ProductVariablePrices).ToList());
				await SaveRangeAsync(clientLocation.Merchandise_Products.SelectMany(p => p.History_ProductVariablePrices).ToList());
			}
			catch (Exception e)
			{
				logger.LogError(e, "SaveMerhandiseAsync");
				throw;
			}
		}
		public async Task SaveAsync<T>(T entity) where T : class
		{
			var set = context.Set<T>();
			try
			{
				if (!set.Any(s => s.Equals(entity)))
				{
					set.Attach(entity);
					context.Entry(entity).State = EntityState.Added;
					//set.Add(entity);
					await context.SaveChangesAsync<T>();
					context.ChangeTracker.Clear();
				}
			}
			catch (Exception e)
			{
				logger.LogError(e, "SaveAsync");
			}
		}

		public async Task SaveRangeAsync<T>(List<T> entityList) where T : class
		{
			try
			{
				var set = context.Set<T>();
				bool hasChanges = false;


				await context.BulkInsertOrUpdateAsync(entityList, bulkConfig: new BulkConfig() { BatchSize = 1000, SqlBulkCopyOptions = SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls });
				context.ChangeTracker.Clear();
				//foreach (var entity in entityList)
				//{
				//	if (!set.Any(s => s.Equals(entity)))
				//	{
				//		set.Attach(entity);
				//		context.Entry(entity).State = EntityState.Added;
				//		if (!hasChanges)
				//		{
				//			hasChanges = true;
				//		}
				//	}
				//}
				//if (hasChanges)
				//{
				//	await context.BulkSaveChangesAsync(bulkConfig: new BulkConfig() { BatchSize = 1000, SqlBulkCopyOptions = SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls });
				//	context.ChangeTracker.Clear();
				//}

			}
			catch (Exception e)
			{
				logger.LogError(e, "SaveRangeAsync");
			}
		}


		#region SQL Script based operations
		public async Task TurnOffForeignKeysAsync()
		{
			await RunSQLFileAsync(context, "ForeignKeys_TurnOff.sql");
		}
		public async Task TurnOnForeignKeysAsync()
		{
			await RunSQLFileAsync(context, "ForeignKeys_TurnOn.sql");
		}
		public async Task DisableTriggersAsync()
		{
			await RunSQLFileAsync(context, "Triggers_Disable.sql");
		}
		public async Task EnableTriggersAsync()
		{
			await RunSQLFileAsync(context, "Triggers_Enable.sql");
		}
		private async Task RunSQLFileAsync(TargetContext context, string filename)
		{
			string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SQL", filename);

			string sql = String.Empty;

			using (StreamReader r = new StreamReader(filepath))
			{
				sql = r.ReadToEnd();
			}
			await context.Database.ExecuteSqlRawAsync(sql);
		}
		#endregion SQL Script based operations
		#region Table Management
		public async Task CreateAllTables() {
			RelationalDatabaseCreator databaseCreator =
	        (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
			await databaseCreator.CreateTablesAsync();
		}
		public async Task ClearAndReseedAllTablesAsync() {
			var tables = GetAllTableNames();

			foreach(var t in tables) {
				try {
					string cmd = $"DELETE FROM {t}";
					await context.Database.ExecuteSqlRawAsync(cmd);
					
					cmd = $"DBCC CHECKIDENT ('{t}', RESEED, 0);";
					await context.Database.ExecuteSqlRawAsync(cmd);
				}
				catch (Exception ex) {
					logger.LogError(ex, "TruncateAllTables");
				}
			}
		}
		public List<string> GetAllTableNames() {
			return context.Model.GetEntityTypes().Select(t => $"[{(t.GetSchema() ?? "dbo")}].[{t.GetTableName()}]").ToList();
		}
		#endregion Table Management
	}
}
