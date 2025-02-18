using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.DataStreams;
using Additive_DB_Refresh.Extensions;
using Additive_DB_Refresh.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additive_DB_Refresh.Services
{
	public class DatabaseCopier
	{
		SystemTablesStream SystemTablesStream { get; }
		ClientStream ClientStream { get; }
		ClientLocationStream ClientLocationStream { get; }
		ILogger<DatabaseCopier> Logger { get; }
		SourceContext Source { get; }
		TargetContext Target { get; }
		List<int> ClientLocationKeys { get; set; } = new List<int>();
		private bool CopyPartnerLocations = false;
		DbCopyConfig CopyConfig { get; set; }
		public DatabaseCopier(SystemTablesStream systemTablesStream,ClientStream clientStream, ClientLocationStream clientLocationStream, ILogger<DatabaseCopier> logger, TargetContext target,SourceContext source) {
			SystemTablesStream = systemTablesStream;
			ClientStream = clientStream;
			ClientLocationStream = clientLocationStream;
			Logger = logger;
			Target = target;
			Source = source;
		}
		public async Task CopyData(bool clearTables) { 

			await PrepareForImportAsync(clearTables);

			ClientLocationKeys = CopyConfig.ClientLocationKeys;
			if (CopyPartnerLocations) {
				ClientLocationKeys = await GetCrossSellPartners(ClientLocationKeys);
			}

			foreach (int clientLocationKey in ClientLocationKeys) { 
				await CopyClientLocationAsync(clientLocationKey);
			}

			await PostImportCleanupAsync();
		}
		private async Task CopyClientLocationAsync(int clientLocationKey) {
			
			try {
				int clientKey = Source.ClientLocations.Where(cl => cl.ClientLocationKey == clientLocationKey).First().ClientKey;

				if (Target.Clients.Where(c => c.ClientKey == clientKey).Count() == 0)
				{
					await ClientStream.CopyClientAsync(clientKey);
				}
				if (Target.ClientLocations.Where(cl => cl.ClientLocationKey == clientLocationKey).Count() == 0)
				{
					await ClientLocationStream.CopyClientLocationAsync(clientLocationKey);
				}
			}
			catch {
				Logger.LogError("DatabaseCopier : Error copying data");
				throw;
			}
		}
		private async Task PrepareForImportAsync(bool clearTables) {
			try
			{
				Logger.LogInformation("Turning off triggers and foreign keys");
				await Target.TurnOffForeignKeysAsync();
				await Target.DisableTriggersAsync();
				if (clearTables)
				{
					await Target.ClearAndReseedAllTablesAsync();
				}
			}
			catch {
				Logger.LogError("PrepareForImport : error");
				throw;
			}
			finally
			{
				await PostImportCleanupAsync();
			}
		}
		private async Task PostImportCleanupAsync() {
			try
			{
				await Target.TurnOnForeignKeysAsync();
				await Target.EnableTriggersAsync();
			}
			catch {
				Logger.LogError("PostImportCleanup");
				throw;
			}
		}
		private async Task<List<int>> GetCrossSellPartners(List<int> clientLocationKeys) {
			List<int> clientLocationKeysOther = new List<int>();

			foreach (int clientLocationKey in clientLocationKeys) {
				List<int> partners = await (Source.BookingAgents.Where(ba => ba.ClientLocationKey == clientLocationKey && ba.PartnerClientLocationKey != null).Select(ba => ba.PartnerClientLocationKey.GetValueOrDefault(-1)))
											.Union(Source.BookingAgents.Where(ba => ba.PartnerClientLocationKey == clientLocationKey && ba.ClientLocationKey != clientLocationKey).Select(ba => ba.ClientLocationKey)).ToListAsync();

				clientLocationKeysOther.AddRange(partners);
				}

			clientLocationKeys.AddRange(clientLocationKeysOther);
			clientLocationKeys = clientLocationKeys.Distinct().ToList();
			return clientLocationKeys;
			}
	}
}

