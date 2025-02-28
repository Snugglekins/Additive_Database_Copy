using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.ResourceManager.Sql;
using Azure.ResourceManager.Sql.Models;
using Azure.ResourceManager;


namespace Additive_DB_Refresh.Utilities
{
	public static class DbManagement
	{
		public static async Task<ArmOperation<SqlDatabaseResource>> CreateDatabase(SqlServerResource destServerResource, SqlDatabaseData newSqlDatabaseData, string newDatabaseName)
		{

			try
			{
				newSqlDatabaseData.CreateMode = SqlDatabaseCreateMode.Default;
				SqlDatabaseCollection databaseCollection = destServerResource.GetSqlDatabases();
				return await databaseCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, newDatabaseName, newSqlDatabaseData);
			}
			catch
			{
				throw;
			}
		}
		public static async Task<ArmOperation<SqlDatabaseResource>> CopyDatabase(SqlServerResource sourceServerResource, string sourceDatabaseName, SqlServerResource destServerResource, string newDatabaseName, SqlDatabaseData newSqlDatabaseData)
		{
			
			try
			{

				SqlDatabaseCollection databaseCollection = destServerResource.GetSqlDatabases();
				SqlDatabaseResource? sourceDatabase = GetDatabase(sourceServerResource, sourceDatabaseName);
				//SqlDatabaseResource? sourceDatabase =  databaseCollection.ToList().FirstOrDefault(db => db.Data.Name== sourceDatabaseName);
				if (sourceDatabase == null) { throw new Exception($"Database {sourceDatabaseName} not found on server {sourceServerResource.Data.Name}"); }
				newSqlDatabaseData.CreateMode = SqlDatabaseCreateMode.Copy;
				newSqlDatabaseData.SourceDatabaseId = sourceDatabase.Id;
				return await databaseCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, newDatabaseName, newSqlDatabaseData);
			}
			catch
			{
				throw;
			}
			
		}
		public static SqlDatabaseResource? GetDatabase(SqlServerResource server, string databaseName)
		{

			List<SqlDatabaseResource> databases = server.GetSqlDatabases().ToList();
			SqlDatabaseResource? database = databases.FirstOrDefault(db => db.Data.Name.Equals(databaseName, StringComparison.OrdinalIgnoreCase));
			return database;

		}
		public static bool DatabaseExists(SqlServerResource resource, string databaseName)
		{
			if (GetDatabase(resource, databaseName) == null) { return false; }
			else { return true; }
		}
		public static async Task SetDatabaseSku(string sku_name,SqlDatabaseResource database)
		{

			try
			{
				
					SqlSku sku = new SqlSku(sku_name);
					SqlDatabasePatch patch = new SqlDatabasePatch();
					patch.Sku = sku;
					patch.SourceDatabaseId = database.Id;
					await database.UpdateAsync(Azure.WaitUntil.Completed, patch);
			}
			catch 
			{
				
				throw;
			}
			return;

		}

	}
}
