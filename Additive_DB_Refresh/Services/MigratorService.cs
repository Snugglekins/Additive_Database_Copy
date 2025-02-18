using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.Extensions;
using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Additive_DB_Refresh.Services
{
	public static class Migrator<T> where T : class

	{
		public static async Task MigrateDataInsertUpdateAsync(IQueryable<T> sourceQuery, TargetContext target, int batchSize = 1000) 
		{

			BulkConfig bulkConfig = new BulkConfig() { BatchSize = batchSize, SqlBulkCopyOptions = EFCore.BulkExtensions.SqlBulkCopyOptions.KeepIdentity | EFCore.BulkExtensions.SqlBulkCopyOptions.KeepNulls };
			
			var sourceSet = sourceQuery.AsAsyncEnumerable();
			
			var batch = new List<T>(batchSize);

			await foreach (var entity in sourceSet)
			{
				batch.Add(entity);

				if (batch.Count >= batchSize)
				{
					await target.BulkInsertOrUpdateAsync(batch,bulkConfig);
					
					batch.Clear();
				}
			}

			// Save remaining entities
			if (batch.Count > 0)
			{
				await target.Set<T>().AddRangeAsync(batch);
				await target.BulkInsertOrUpdateAsync(batch, bulkConfig);
			}

			Console.WriteLine($"Migration completed for {typeof(T).Name}!");
		}

		public static async Task MigrateDataInsertAsync(IQueryable<T> sourceQuery, TargetContext target, int batchSize = 1000)
		{
			//Use for larger datasets where you are sure there will be no key conflicts
			BulkConfig bulkConfig = new BulkConfig() { BatchSize = batchSize, SqlBulkCopyOptions = EFCore.BulkExtensions.SqlBulkCopyOptions.KeepIdentity | EFCore.BulkExtensions.SqlBulkCopyOptions.KeepNulls };

			var sourceSet = sourceQuery.AsAsyncEnumerable();

			var batch = new List<T>(batchSize);

			await foreach (var entity in sourceSet)
			{
				batch.Add(entity);

				if (batch.Count >= batchSize)
				{
					
					await target.BulkInsertAsync(batch, bulkConfig);

					batch.Clear();
				}
			}

			// Save remaining entities
			if (batch.Count > 0)
			{
				await target.Set<T>().AddRangeAsync(batch);
				await target.BulkInsertAsync(batch, bulkConfig);
			}

			Console.WriteLine($"Migration completed for {typeof(T).Name}!");
		}
		public static async Task BulkInsertData(TargetContext target, SourceContext source, string query)  {


			SqlConnection sourceConn = new SqlConnection(source.Database.GetConnectionString());
			SqlConnection targetConn = new SqlConnection(target.Database.GetConnectionString());

			try
			{
				await sourceConn.OpenAsync();
				await targetConn.OpenAsync();

				SqlCommand cmd = new SqlCommand(query, sourceConn);
				using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
				{
					SqlBulkCopy bulkCopy = new SqlBulkCopy(targetConn);
					bulkCopy.DestinationTableName = target.GetTableName<T>();
					await bulkCopy.WriteToServerAsync(reader);
					bulkCopy.Close();
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally {
				sourceConn.Close();
				targetConn.Close();
			}

		
		}
		//public static async Task MigrateByteIdentity(IQueryable<T> sourceQuery, TargetContext target)
		//{
		//	var sourceSet = await sourceQuery.ToListAsync();


		//	if (sourceSet.Count > 0)
		//	{
		//		using (var transaction = await target.Database.BeginTransactionAsync())
		//		{
		//			await target.IdentityInsertOn<T>();
		//			await target.Set<T>().AddRangeAsync(sourceSet);
		//			await target.SaveChangesAsync();
		//			await target.IdentityInsertOff<T>();
		//			await target.Database.CommitTransactionAsync();
		//		}
		//	}

		//}
		}

}
