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
		private SourceContext source;
		private TargetContext target;
		private ILogger<ClientStream> logger;
		public ClientStream(SourceContext _source, TargetContext _target, ILogger<ClientStream> _logger) {
			source = _source;
			target = _target;
			logger = _logger;
			source.Database.SetCommandTimeout(0);
			target.Database.SetCommandTimeout(0);
		}
		public async Task CopyClientAsync(int clientKey) {
			try
			{
				logger.LogInformation($"Copying client {clientKey}");
				await Migrator<Client>.MigrateDataInsertUpdateAsync(GetClientIQueryable(source, clientKey), target);
				await Migrator<ClientLogin>.MigrateDataInsertUpdateAsync(GetClientLoginsIQueryable(source, clientKey), target);
				await Migrator<ClientEmployee>.MigrateDataInsertUpdateAsync(GetClientEmployeesIQueryable(source, clientKey), target);
				logger.LogInformation($"Finished copying client {clientKey}");
			}
			catch (Exception ex) {
				logger.LogError(ex, "CopyClient");
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
			source.Dispose();
			target.Dispose();
		}
	}
}
