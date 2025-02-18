using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Additive_DB_Refresh.Repositories
{
	public class SourceRepository
	{
		private readonly SourceContext source;
		public SourceRepository(SourceContext _source)
		{
			_source.Database.SetCommandTimeout(0);
			source = _source;
		}
		public async Task<Client> GetClientAsync(int clientKey)
		{

			Client client = await source.Clients
							.Include(c => c.ClientLogins)
							.Where(c => c.ClientKey == clientKey).FirstAsync();
			client.ClientEmployees = source.ClientEmployees.Where(ce => ce.ClientKey == clientKey).ToList();
			return client;
		}
		#region Build Client Location
		public async Task<ClientLocation> GetClientLocationAsync(int clientLocationKey)
		{
			ClientLocation clientLocation = await source.ClientLocations
													.Where(cl => cl.ClientLocationKey == clientLocationKey).FirstAsync();
			await LoadActivitiesAsync(clientLocation);
			await LoadMerchandiseAsync(clientLocation);
			return clientLocation;
		}
		public async Task LoadMerchandiseAsync(ClientLocation clientLocation)
		{
			
			clientLocation.Merchandise_ProductCategories = await source.Merchandise_ProductCategories
															.Where(pc => pc.ClientLocationKey == clientLocation.ClientLocationKey).ToListAsync();

			clientLocation.Merchandise_ProductCategoryAssociations = await source.Merchandise_ProductCategoryAssociations
															.Where(pca => pca.ClientLocationKey == clientLocation.ClientLocationKey).ToListAsync();

			clientLocation.Merchandise_ProductColors = await source.Merchandise_ProductColors
															.Where(pc => pc.ClientLocationKey == clientLocation.ClientLocationKey).ToListAsync();

			clientLocation.Merchandise_Products = await source.Merchandise_Products
															.Include(p => p.History_Products)
															.Where(p => p.ClientLocationKey == clientLocation.ClientLocationKey).ToListAsync();

			List<Merchandise_Inventory> inventories = await (from i in source.Merchandise_Inventories
								join prod in source.Merchandise_Products on i.ProductKey equals prod.ProductKey
								where prod.ClientLocationKey == clientLocation.ClientLocationKey
								select i).ToListAsync();

			List<Merchandise_InventoryChangeLog> invChangeLogs = await ( from i in source.Merchandise_InventoryChangeLogs
								join prod in source.Merchandise_Products on i.ProductKey equals prod.ProductKey
								where prod.ClientLocationKey == clientLocation.ClientLocationKey
								select i).ToListAsync();

			List<Merchandise_ProductVariablePrice> pvps = await (from i in source.Merchandise_ProductVariablePrices
							  join prod in source.Merchandise_Products on i.ProductKey equals prod.ProductKey
							  where prod.ClientLocationKey == clientLocation.ClientLocationKey
							  select i).ToListAsync();

			List<History_ProductVariablePrice> pvphistories = await (from i in source.History_ProductVariablePrices
							  join prod in source.Merchandise_Products on i.ProductKey equals prod.ProductKey
							  where prod.ClientLocationKey == clientLocation.ClientLocationKey
							  select i).ToListAsync();

			foreach (Merchandise_Product p in clientLocation.Merchandise_Products) {



		  
				if (inventories.Where(i=>i.ProductKey == p.ProductKey).FirstOrDefault()!=null) {
					p.Merchandise_Inventory = inventories.Where(i => i.ProductKey == p.ProductKey).FirstOrDefault();
				}

				p.Merchandise_InventoryChangeLogs = invChangeLogs.Where(i => i.ProductKey == p.ProductKey).ToList();
				p.Merchandise_ProductVariablePrices = pvps.Where(pvp=> pvp.ProductKey==p.ProductKey).ToList();
				p.History_ProductVariablePrices = pvphistories.Where(pvp => pvp.ProductKey == p.ProductKey).ToList();
			}
			
			clientLocation.Merchandise_ProductSizes = await source.Merchandise_ProductSizes
															.Where(f => f.ClientLocationKey == clientLocation.ClientLocationKey).ToListAsync();
			
		}
		public async Task LoadActivitiesAsync(ClientLocation clientLocation)
		{
			clientLocation.ClientLocationEntities = await source.ClientLocationEntities
															.Where(cle => cle.ClientLocationKey == clientLocation.ClientLocationKey)
															.ToListAsync();
			clientLocation.EntityHierarchies = await source.EntityHierarchies
															.Include(e => e.EntityHierarchyRates)
															.Where(e => e.ClientLocationKey == clientLocation.ClientLocationKey).ToListAsync();
			foreach (EntityHierarchy eh in clientLocation.EntityHierarchies) {
				eh.History_EntityHierarchyRates = await source.History_EntityHierarchyRates.Where(r => r.EntityHierarchyKey == eh.EntityHierarchyKey).ToListAsync();
			}

		}
		public async void LoadLoginsAsync(ClientLocation clientLocation)
		{
			clientLocation.ClientLocationLogins = await source.ClientLocationLogins
														.Include(cll => cll.ClientLocationLoginRoles)
														.Where(cll => cll.ClientLocationKey == clientLocation.ClientLocationKey).ToListAsync();

		}
		public void LoadPhotoPackagesAsync(ClientLocation clientLocation)
		{

		}
		#endregion Client Location
	}
}
