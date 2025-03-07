using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.Models;
using Additive_DB_Refresh.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additive_DB_Refresh.DataStreams
{
	public class ClientStream : IDisposable
	{
		private SourceContext Source;
		private TargetContext Target;
		private ILogger<ClientStream> Logger;
		public ClientStream(SourceContext source, TargetContext target, ILogger<ClientStream> logger) {
			Source = source;
			Target = target;
			Logger = logger;
			Source.Database.SetCommandTimeout(0);
			Target.Database.SetCommandTimeout(0);
		}
		public async Task CopyClientAsync(int clientKey) {
			try
			{
				Logger.LogInformation($"Copying client {clientKey}");
				await Migrator<Client>.MigrateDataInsertUpdateAsync(GetClientIQueryable(Source, clientKey), Target);
				await Migrator<ClientLogin>.MigrateDataInsertUpdateAsync(GetClientLoginsIQueryable(Source, clientKey), Target);
				await Migrator<ClientEmployee>.MigrateDataInsertUpdateAsync(GetClientEmployeesIQueryable(Source, clientKey), Target);
				Logger.LogInformation($"Finished copying client {clientKey}");
			}
			catch (Exception ex) {
				Logger.LogError(ex, "CopyClient");
			}
		}
		#region Client Queryables
		private IQueryable<Client> GetClientIQueryable(SourceContext source, int clientKey)
		{
			return source.Clients.Where(c => c.ClientKey == clientKey);
		}
		private IQueryable<ClientLogin> GetClientLoginsIQueryable(SourceContext source, int clientKey)
		{
			return source.ClientLogins.Where(c => c.ClientKey == clientKey);
		}
		private IQueryable<ClientEmployee> GetClientEmployeesIQueryable(SourceContext source, int clientKey)
		{
			return source.ClientEmployees.Where(c => c.ClientKey == clientKey);
		}
		#endregion Client Queryables
		public void Dispose()
		{
			Source.Dispose();
			Target.Dispose();
		}
	}
}
