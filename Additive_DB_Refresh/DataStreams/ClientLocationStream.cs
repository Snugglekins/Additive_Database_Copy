using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.Extensions;
using Additive_DB_Refresh.Models;
using Additive_DB_Refresh.Services;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Additive_DB_Refresh.DataStreams
{
	public class ClientLocationStream : IDisposable
	{
		private ILogger Logger;
		IDbContextFactory<SourceContext> SourceFactory;
		IDbContextFactory<TargetContext> TargetFactory;
		private bool CopyParnterLocations { get; }
		private SourceContext Source { get; set; }
		private TargetContext Target { get; set; }

		private string TargetDatabaseName;
		public ClientLocationStream(IDbContextFactory<SourceContext> sourceFactory, IDbContextFactory<TargetContext> targetFactory, ILogger<ClientLocationStream> logger, string targetDatabase)
		{
			Logger = logger;
			SourceFactory = sourceFactory;
			TargetFactory = targetFactory;
			TargetDatabaseName = targetDatabase;
			Source = SourceFactory.CreateDbContextNoTimeout();
			Target = TargetFactory.CreateDbContextNoTimeout(TargetDatabaseName);
		}
		public async Task CopyClientLocationAsync(int clientLocationKey, bool copyPartnerLocations, List<int> otherClientLocations)
		{
			Logger.LogInformation("Copying Client Location {0}", clientLocationKey);
			Source = await SourceFactory.CreateDbContextNoTimeoutAsync();
			Target = await TargetFactory.CreateDbContextNoTimeoutAsync(TargetDatabaseName);

			await Target.InsertUpdateAsync(GetClientLocation(clientLocationKey));

			#region Load Security
			Logger.LogInformation("Copying Client Location {0} Security", clientLocationKey);
			await Target.InsertUpdateAsync(GetClientLocationRoles(clientLocationKey));
			await Target.InsertUpdateAsync(GetClientLocationRoleApplicationObjects(clientLocationKey));
			await Target.InsertUpdateAsync(GetClientLocationLogins(clientLocationKey));
			await Target.InsertUpdateAsync(GetClientLocationLoginRoles(clientLocationKey));

			#endregion Load Security
			#region Load Activities
			Logger.LogInformation("Copying Client Location {0} Activities", clientLocationKey);
			Source = await SourceFactory.CreateDbContextNoTimeoutAsync();
			Target = await TargetFactory.CreateDbContextNoTimeoutAsync(TargetDatabaseName);

			await Target.InsertUpdateAsync(GetClientLocationEntities(clientLocationKey));
			await Target.InsertUpdateAsync(GetEntityHierarchies(clientLocationKey));
			await Target.InsertUpdateAsync(GetEntityHierarchyEmployees(clientLocationKey));
			await Target.InsertUpdateAsync(GetEntityHierarchyInventories(clientLocationKey));
			await Target.InsertUpdateAsync(GetEntityHierarchyTranslations(clientLocationKey));
			await Target.InsertUpdateAsync(GetEntityHierarchyRates(clientLocationKey));
			await Target.InsertUpdateAsync(GetHistory_EntityHierarchyRates(clientLocationKey));
			await Target.InsertUpdateAsync(GetEntityHierarchyRateResources(clientLocationKey));
			await Target.InsertUpdateAsync(GetEntityHierarchyOptions(clientLocationKey));
			await Target.InsertUpdateAsync(GetEntityHierarchyOptionRates(clientLocationKey));
			await Target.InsertUpdateAsync(GetClientLocationEntitySchedules(clientLocationKey));
			await Target.InsertUpdateAsync(GetClientLocationScheduleTimes(clientLocationKey));
			await Target.InsertUpdateAsync(GetClientLocationScheduleTimeDays(clientLocationKey),10000);
			await Target.InsertUpdateAsync(GetClientLocationScheduleTimeDayRates(clientLocationKey), 10000);
			await Target.InsertUpdateAsync(GetClientLocationEntityScheduleHours(clientLocationKey));
			await Target.InsertUpdateAsync(GetClientLocationEntityScheduleRates(clientLocationKey));
			await Target.InsertUpdateAsync(GetClientLocationEntityScheduleResources(clientLocationKey));
			await Target.InsertUpdateAsync(GetClientLocationScheduleDaysEnums(clientLocationKey), 10000);

			#endregion Load Activities
			#region Load Merchandise
			Logger.LogInformation("Copying Client Location {0} Merchandise", clientLocationKey);
			Source = await SourceFactory.CreateDbContextNoTimeoutAsync();
			Target = await TargetFactory.CreateDbContextNoTimeoutAsync(TargetDatabaseName);

			await Target.InsertUpdateAsync(GetMerchandise_Products(clientLocationKey));
			await Target.InsertUpdateAsync(GetMerchandise_ProductCategories(clientLocationKey));
			await Target.InsertUpdateAsync(GetMerchandise_ProductCategoryAssociations(clientLocationKey));
			await Target.InsertAsync(GetMerchandise_Inventory(clientLocationKey));
			await Target.InsertUpdateAsync(GetMerchandise_ProductColors(clientLocationKey));
			await Target.InsertUpdateAsync(GetMerchandise_ProductFlavors(clientLocationKey));
			await Target.InsertUpdateAsync(GetMerchandise_ProductSizes(clientLocationKey));
			await Target.InsertUpdateAsync(GetMerchandise_ProductVariablePrices(clientLocationKey));
			await Target.InsertUpdateAsync(GetHistory_ProductVariablePrices(clientLocationKey));
			await Target.InsertAsync(GetMerchandise_InventoryChangeLog(clientLocationKey), 10000);
			#endregion Load Merchandise
			Logger.LogInformation("Copying Client Location {0} Photo Packages, Cash Drawer, Communication", clientLocationKey);
			#region Load PhotoPackages
			await Target.InsertUpdateAsync(GetPhotoPackages(clientLocationKey));
			#endregion Load PhototPackages
			#region Load CashDrawer
			await Target.InsertUpdateAsync(GetRegisters(clientLocationKey));
			#endregion Load CashDrawer
			#region Load Communication
			await Target.InsertUpdateAsync(GetEmailTemplates(clientLocationKey));
			await Target.InsertUpdateAsync(GetEmailTemplateSections(clientLocationKey));
			#endregion Load Communication
			#region BookingAgents
			Logger.LogInformation("Copying Client Location {0} Booking Agents", clientLocationKey);
			await Target.InsertUpdateAsync(GetBookingAgents(clientLocationKey, copyPartnerLocations, otherClientLocations));
			await Target.InsertUpdateAsync(GetBookingAgentEntityHierarchies(clientLocationKey,copyPartnerLocations, otherClientLocations));
			await Target.InsertUpdateAsync(GetBookingAgentEntityHierarchyRates(clientLocationKey, copyPartnerLocations, otherClientLocations));
			await Target.InsertUpdateAsync(GetClientLocationEntityScheduleBookingAgents(clientLocationKey,copyPartnerLocations, otherClientLocations));
			await Target.InsertUpdateAsync(GetClientLocationEntityScheduleBookingAgentRates(clientLocationKey, copyPartnerLocations, otherClientLocations));
			await Target.InsertUpdateAsync(GetClientLocationScheduleTimeBookingAgents(clientLocationKey, copyPartnerLocations, otherClientLocations));
			await Target.InsertUpdateAsync(GetClientLocationScheduleDayBookingAgentRates(clientLocationKey, copyPartnerLocations, otherClientLocations));
			await Target.InsertUpdateAsync(GetClientLocationScheduleTimeDayBookingAgentRateEnums(clientLocationKey, copyPartnerLocations, otherClientLocations), 10000);
			#endregion BookingAgents
			#region Load Consent Forms
			Logger.LogInformation("Copying Client Location {0} Consent Forms", clientLocationKey);
			await Target.InsertUpdateAsync(GetConsentForms(clientLocationKey));
			//Consent Form Headings has no primary key defined
			await Target.BulkInsertAsync(GetConsentFormHeadings(clientLocationKey));
			await Target.InsertUpdateAsync(GetConsentFormQuestions(clientLocationKey));
			await Target.InsertUpdateAsync(GetConsentQuestions(clientLocationKey));
			//Consent Question Languages has no primary key defined
			await Target.BulkInsertAsync(GetConsentQuestionLanguages(clientLocationKey));
			#endregion Load Consent Forms
			#region Load Custom Forms
			Logger.LogInformation("Copying Client Location {0} Custom Forms", clientLocationKey);
			Source = await SourceFactory.CreateDbContextNoTimeoutAsync();
			Target = await TargetFactory.CreateDbContextNoTimeoutAsync(TargetDatabaseName);
			await Target.InsertUpdateAsync(GetDIN_ExperienceLevelsQueryable(clientLocationKey));
			await Target.InsertUpdateAsync(GetDIN_HeightsQueryable(clientLocationKey));
			await Target.InsertUpdateAsync(GetDIN_ShoeSizesQueryable(clientLocationKey));
			await Target.InsertUpdateAsync(GetDIN_WeightsQueryable(clientLocationKey));
			#endregion Load Custom Forms
			#region Load Fees
			Logger.LogInformation("Copying Client Location {0} Fees", clientLocationKey);
			await Target.InsertUpdateAsync(GetFees(clientLocationKey));
			await Target.InsertUpdateAsync(GetFeeAssociations(clientLocationKey));
			#endregion Load Fees
			#region Load Misc
			Logger.LogInformation("Copying Client Location {0} Misc", clientLocationKey);
			await Target.InsertUpdateAsync(GetClientLocationHeardAboutUs(clientLocationKey));
			await Target.InsertUpdateAsync(GetClientLocationOtherPaymentTypes(clientLocationKey));
			await Target.InsertUpdateAsync(GetClientLocationWeathers(clientLocationKey));
			await Target.InsertUpdateAsync(GetDaylightSavingTimeWindows(clientLocationKey));
			await Target.InsertUpdateAsync(GetExplicitDays(clientLocationKey));
			await Target.InsertUpdateAsync(GetOrganizationTypes(clientLocationKey));
			#endregion Load Misc
			#region Load Packages
			Logger.LogInformation("Copying Client Location {0} Packages", clientLocationKey);
			await Target.InsertUpdateAsync(GetPackages(clientLocationKey));
			await Target.InsertUpdateAsync(GetPackageDetails(clientLocationKey,copyPartnerLocations, otherClientLocations));
			await Target.InsertUpdateAsync(GetPackageDetailRates(clientLocationKey,copyPartnerLocations, otherClientLocations));
			await Target.InsertUpdateAsync(GetPackageGroups(clientLocationKey));
			await Target.InsertUpdateAsync(GetPackageDetailGroups(clientLocationKey,copyPartnerLocations, otherClientLocations));
			#endregion Load Packages
			#region Load PickupRoutes
			Logger.LogInformation("Copying Client Location {0} Pickup Routes", clientLocationKey);
			await Target.InsertUpdateAsync(GetPickupRoutes(clientLocationKey));
			await Target.InsertUpdateAsync(GetPickupRouteDays(clientLocationKey));
			await Target.InsertUpdateAsync(GetPickupRouteEmployees(clientLocationKey));
			await Target.InsertUpdateAsync(GetPickupRouteSchedules(clientLocationKey));
			await Target.InsertUpdateAsync(GetPickupRouteScheduleTimes(clientLocationKey));
			await Target.InsertUpdateAsync(GetPickupRouteScheduleTimeDays(clientLocationKey));
			await Target.InsertUpdateAsync(GetPickupRouteStops(clientLocationKey));
			await Target.InsertUpdateAsync(GetPickupRouteStopDays(clientLocationKey));
			await Target.InsertUpdateAsync(GetPickupRouteVehicles(clientLocationKey));
			await Target.InsertUpdateAsync(GetPickupVehicles(clientLocationKey));
			await Target.InsertUpdateAsync(GetPickupLocations(clientLocationKey));
			#endregion Load PickupRoutes

		}
		#region Security Queryable
		private IQueryable<ClientLocation> GetClientLocation(int clientLocationKey)
		{
			return Source.ClientLocations.Where(cl => clientLocationKey == cl.ClientLocationKey);
		}
		private IQueryable<ClientLocationRole> GetClientLocationRoles(int clientLocationKey) { 
			return Source.ClientLocationRoles.Where(clr => clr.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<ClientLocationLogin> GetClientLocationLogins(int clientLocationKey) {
			return Source.ClientLocationLogins.Where(cll => cll.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<ClientLocationLoginRole> GetClientLocationLoginRoles(int clientLocationKey) {
			return from cllr in Source.ClientLocationLoginRoles
				   join cll in Source.ClientLocationLogins on cllr.ClientLocationLoginKey equals cll.ClientLocationLoginKey
				   where cll.ClientLocationKey == clientLocationKey
				   select cllr;	   
		}
		private IQueryable<ClientLocationRoleApplicationObject> GetClientLocationRoleApplicationObjects(int clientLocationKey) {
			return from clrao in Source.ClientLocationRoleApplicationObjects
				   join clr in Source.ClientLocationRoles on clrao.ClientLocationRoleKey equals clr.ClientLocationRoleKey
				   where clr.ClientLocationKey == clientLocationKey
				   select clrao;
		}
		#endregion Security Queryable
		#region Activities Queryable
		private IQueryable<ClientLocationEntity> GetClientLocationEntities(int clientLocationKey) { 
			return Source.ClientLocationEntities.Where(cle => cle.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<EntityHierarchy> GetEntityHierarchies(int clientLocationKey) { 
			return Source.EntityHierarchies.Where(eh => eh.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<EntityHierarchyEmployee> GetEntityHierarchyEmployees(int clientLocationKey) { 
			return from eh in Source.EntityHierarchies
				   join ehe in Source.EntityHierarchyEmployees
					on eh.EntityHierarchyKey equals ehe.EntityHierarchyKey
				   where eh.ClientLocationKey == clientLocationKey
				   select ehe;
		}
		private IQueryable<EntityHierarchyInventory> GetEntityHierarchyInventories(int clientLocationKey)
		{
			return from eh in Source.EntityHierarchies
				   join ehi in Source.EntityHierarchyInventories
					on eh.EntityHierarchyKey equals ehi.EntityHierarchyKey
				   where eh.ClientLocationKey == clientLocationKey
				   select ehi;
		}
		private IQueryable<EntityHierarchyTranslation> GetEntityHierarchyTranslations(int clientLocationKey) {
			return from eh in Source.EntityHierarchies
				   join eht in Source.EntityHierarchyTranslations
					on eh.EntityHierarchyKey equals eht.EntityHierarchyKey
				   where eh.ClientLocationKey == clientLocationKey
				   select eht;
		}
		private IQueryable<EntityHierarchyRate> GetEntityHierarchyRates(int clientLocationKey) { 
			return from ehr in Source.EntityHierarchyRates
					join eh in Source.EntityHierarchies on ehr.EntityHierarchyKey equals eh.EntityHierarchyKey
				where eh.ClientLocationKey == clientLocationKey
				select ehr;
		}
		private IQueryable<History_EntityHierarchyRate> GetHistory_EntityHierarchyRates(int clientLocationKey) { 
			return from hehr in Source.History_EntityHierarchyRates
					join eh in Source.EntityHierarchies on hehr.EntityHierarchyKey equals eh.EntityHierarchyKey
				where eh.ClientLocationKey == clientLocationKey
				select hehr;
		}
		private IQueryable<EntityHierarchyRateResource> GetEntityHierarchyRateResources(int clientLocationKey) {
			return from ehr in Source.EntityHierarchyRates
				   join eh in Source.EntityHierarchies on ehr.EntityHierarchyKey equals eh.EntityHierarchyKey
				   join ehrr in Source.EntityHierarchyRateResources on ehr.EntityHierarchyRateKey equals ehrr.EntityHierarchyRateKey
				   where eh.ClientLocationKey == clientLocationKey
				   select ehrr;
		}
		private IQueryable<EntityHierarchyOption> GetEntityHierarchyOptions(int clientLocationKey)
		{
			return from eh in Source.EntityHierarchies
				   join eho in Source.EntityHierarchyOptions
					on eh.EntityHierarchyKey equals eho.EntityHierarchyKey
				   where eh.ClientLocationKey == clientLocationKey
				   select eho;
		}
		private IQueryable<EntityHierarchyOptionRate> GetEntityHierarchyOptionRates(int clientLocationKey)
		{
			return from eh in Source.EntityHierarchies
				   join eho in Source.EntityHierarchyOptions
					on eh.EntityHierarchyKey equals eho.EntityHierarchyKey
					join ehor in Source.EntityHierarchyOptionRates
					on eho.EntityHierarchyOptionKey equals ehor.EntityHierarchyOptionKey
				where eh.ClientLocationKey == clientLocationKey
				   select ehor;
		}
		private IQueryable<ClientLocationEntitySchedule> GetClientLocationEntitySchedules(int clientLocationKey) { 
			return Source.ClientLocationEntitySchedules.Where(c => c.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<ClientLocationScheduleTime> GetClientLocationScheduleTimes(int clientLocationKey) {
			return from cles in Source.ClientLocationEntitySchedules
				   join clst in Source.ClientLocationScheduleTimes on cles.ClientLocationEntityScheduleKey equals clst.ClientLocationEntityScheduleKey
				   where cles.ClientLocationKey == clientLocationKey
				   select clst;
		}
		private IQueryable<ClientLocationScheduleTimeDay> GetClientLocationScheduleTimeDays(int clientLocationKey)
		{
			return from cles in Source.ClientLocationEntitySchedules
				   join clst in Source.ClientLocationScheduleTimes on cles.ClientLocationEntityScheduleKey equals clst.ClientLocationEntityScheduleKey
				   join clstd in Source.ClientLocationScheduleTimeDays on clst.ClientLocationScheduleTimeKey equals clstd.ClientLocationScheduleTimeKey
				   where cles.ClientLocationKey == clientLocationKey
				   select clstd;
		}
		private IQueryable<ClientLocationScheduleTimeDayRate> GetClientLocationScheduleTimeDayRates(int clientLocationKey)
		{
			return from cles in Source.ClientLocationEntitySchedules
				   join clst in Source.ClientLocationScheduleTimes 
						on cles.ClientLocationEntityScheduleKey equals clst.ClientLocationEntityScheduleKey
				   join clstd in Source.ClientLocationScheduleTimeDays 
						on clst.ClientLocationScheduleTimeKey equals clstd.ClientLocationScheduleTimeKey
				   join clstdr in Source.ClientLocationScheduleTimeDayRates
						on new { clstd.ClientLocationScheduleTimeKey,  clstd.ClientLocationScheduleTimeDayKey } 
							equals new { clstdr.ClientLocationScheduleTimeKey , ClientLocationScheduleTimeDayKey = clstdr.ClientLocationScheduleTimeDayKey.GetValueOrDefault(-1) }
				   where cles.ClientLocationKey == clientLocationKey
				   select clstdr;
		}
		private IQueryable<ClientLocationEntityScheduleHour> GetClientLocationEntityScheduleHours(int clientLocationKey) {
			return from cles in Source.ClientLocationEntitySchedules
				   join clesh in Source.ClientLocationEntityScheduleHours
						on cles.ClientLocationEntityScheduleKey equals clesh.ClientLocationEntityScheduleKey
				   where cles.ClientLocationKey == clientLocationKey
				   select clesh;
		}
		private IQueryable<ClientLocationEntityScheduleRate> GetClientLocationEntityScheduleRates(int clientLocationKey) { 
			return from cles in Source.ClientLocationEntitySchedules
				   join clesr in Source.ClientLocationEntityScheduleRates
						on cles.ClientLocationEntityScheduleKey equals clesr.ClientLocationEntityScheduleKey
				   where cles.ClientLocationKey == clientLocationKey
				   select clesr;
		}
		private IQueryable<ClientLocationEntityScheduleResource> GetClientLocationEntityScheduleResources(int clientLocationKey) {
			return from cles in Source.ClientLocationEntitySchedules
				   join clesr in Source.ClientLocationEntityScheduleResources
						on cles.ClientLocationEntityScheduleKey equals clesr.ClientLocationEntityScheduleKey
				   where cles.ClientLocationKey == clientLocationKey
				   select clesr;
		}

		private IQueryable<ClientLocationScheduleDaysEnum> GetClientLocationScheduleDaysEnums(int clientLocationKey) { 
			return Source.ClientLocationScheduleDaysEnums.Where(e => e.ClientLocationKey == clientLocationKey);
		}

		#endregion Activites Queryable
		#region Merchandise Queryable
		private IQueryable<Merchandise_Product> GetMerchandise_Products(int clientLocationKey)
		{
			return Source.Merchandise_Products.Where(p => p.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<Merchandise_ProductCategory> GetMerchandise_ProductCategories(int clientLocationKey)
		{
			return Source.Merchandise_ProductCategories.Where(p => p.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<Merchandise_ProductCategoryAssociation> GetMerchandise_ProductCategoryAssociations(int clientLocationKey)
		{
			return Source.Merchandise_ProductCategoryAssociations.Where(p => p.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<Merchandise_Inventory> GetMerchandise_Inventory(int clientLocationKey)
		{
			return from p in Source.Merchandise_Products
				   join i in Source.Merchandise_Inventories on p.ProductKey equals i.ProductKey
				   where p.ClientLocationKey == clientLocationKey
				   select i;
		}
		private IQueryable<Merchandise_InventoryChangeLog> GetMerchandise_InventoryChangeLog(int clientLocationKey)
		{
			return from p in Source.Merchandise_Products
				   join i in Source.Merchandise_InventoryChangeLogs on p.ProductKey equals i.ProductKey
				   where p.ClientLocationKey == clientLocationKey
				   select i;
		}
		private IQueryable<Merchandise_ProductColor> GetMerchandise_ProductColors(int clientLocationKey)
		{
			return Source.Merchandise_ProductColors.Where(pc => pc.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<Merchandise_ProductFlavor> GetMerchandise_ProductFlavors(int clientLocationKey)
		{
			return Source.Merchandise_ProductFlavors.Where(pf => pf.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<Merchandise_ProductSize> GetMerchandise_ProductSizes(int clientLocationKey)
		{
			return Source.Merchandise_ProductSizes.Where(ps => ps.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<Merchandise_ProductVariablePrice> GetMerchandise_ProductVariablePrices(int clientLocationKey)
		{
			return from p in Source.Merchandise_Products
				   join pvp in Source.Merchandise_ProductVariablePrices on p.ProductKey equals pvp.ProductKey
				   where p.ClientLocationKey == clientLocationKey
				   select pvp;
		}
		private IQueryable<History_ProductVariablePrice> GetHistory_ProductVariablePrices(int clientLocationKey)
		{
			return from p in Source.Merchandise_Products
				   join pvp in Source.History_ProductVariablePrices on p.ProductKey equals pvp.ProductKey
				   where p.ClientLocationKey == clientLocationKey
				   select pvp;
		}
		#endregion Merchandise Queryable
		#region PhotoPackage Queryable
		public IQueryable<PhotoPackage> GetPhotoPackages(int clientLocationKey) {
			return from cle in Source.ClientLocationEntities
				   join pp in Source.PhotoPackages
					   on cle.ClientLocationEntityKey equals pp.ClientLocationEntityKey
				   where cle.ClientLocationKey == clientLocationKey
				   select pp;
		}
		#endregion PhotoPackage Queryable
		#region CashDrawer Queryable
		private IQueryable<Register> GetRegisters(int clientLocationKey) {
			return Source.Registers.Where(r => r.ClientLocationKey == clientLocationKey);
		}

		#endregion CashDrawer Queryable
		#region Communication Queryable
		private IQueryable<EmailTemplate> GetEmailTemplates(int clientLocationKey) { 
			return Source.EmailTemplates.Where(e => e.ClientLocationKey==clientLocationKey);
		}
		private IQueryable<EmailTemplateSection> GetEmailTemplateSections(int clientLocationKey) {
			return from et in Source.EmailTemplates
				   join ets in Source.EmailTemplateSections
					on et.EmailTemplateKey equals ets.EmailTemplateKey
					where et.ClientLocationKey == clientLocationKey
				   select ets;
		}

		#endregion Communication Queryable
		#region BookingAgents Queryable

		private IQueryable<BookingAgent> GetBookingAgents(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			if (copyParnterLocations) {
				return Source.BookingAgents.Where(ba => ba.ClientLocationKey == clientLocationKey);
			}
			else {
			var bookingAgents =  Source.BookingAgents.Where(ba => ba.ClientLocationKey == clientLocationKey && ba.PartnerClientLocationKey == null);
				if (otherClientLocationKeys.Count() > 0) {
					//otherClientLocationKeys.Contains(ba.PartnerClientLocationKey.GetValueOrDefault(-1))
					bookingAgents = bookingAgents.Union(Source.BookingAgents.Where(ba => ba.ClientLocationKey == clientLocationKey && otherClientLocationKeys.Contains(ba.PartnerClientLocationKey.GetValueOrDefault(-1))));
				}
				return bookingAgents;
			}
		}
		private IQueryable<BookingAgentEntityHierarchy> GetBookingAgentEntityHierarchies(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from ba in bookingAgents
				   join baeh in Source.BookingAgentEntityHierarchies on ba.BookingAgentKey equals baeh.BookingAgentKey
				   select baeh;
		}
		private IQueryable<BookingAgentEntityHierarchyRate> GetBookingAgentEntityHierarchyRates(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys) {
			{
				var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

				return from ba in bookingAgents
					   join baehr in Source.BookingAgentEntityHierarchyRates on ba.BookingAgentKey equals baehr.BookingAgentKey
					   select baehr;
			}
		}
		private IQueryable<ClientLocationEntityScheduleBookingAgent> GetClientLocationEntityScheduleBookingAgents(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from ba in bookingAgents
				   join clesba in Source.ClientLocationEntityScheduleBookingAgents
						on ba.BookingAgentKey equals clesba.BookingAgentKey
				select clesba;
		}
		private IQueryable<ClientLocationEntityScheduleBookingAgentRate> GetClientLocationEntityScheduleBookingAgentRates(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from ba in bookingAgents
				   join clesbar in Source.ClientLocationEntityScheduleBookingAgentRates
						on ba.BookingAgentKey equals clesbar.BookingAgentKey
				   select clesbar;
		}
		private IQueryable<ClientLocationScheduleTimeBookingAgent> GetClientLocationScheduleTimeBookingAgents(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from ba in bookingAgents
				   join clstba in Source.ClientLocationScheduleTimeBookingAgents
						on ba.BookingAgentKey equals clstba.BookingAgentKey
				   select clstba;
		}
		private IQueryable<ClientLocationScheduleDayBookingAgentRate> GetClientLocationScheduleDayBookingAgentRates(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from ba in bookingAgents
				   join clsdbar in Source.ClientLocationScheduleDayBookingAgentRates
					on ba.BookingAgentKey equals clsdbar.BookingAgentKey
				   select clsdbar;
		}
		private IQueryable<ClientLocationScheduleTimeDayBookingAgentRateEnum> GetClientLocationScheduleTimeDayBookingAgentRateEnums(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from ba in bookingAgents
				   join clstdbare in Source.ClientLocationScheduleTimeDayBookingAgentRateEnums
				   on ba.BookingAgentKey equals clstdbare.BookingAgentKey
				   select clstdbare;
		}
		#endregion BookingAgents Queryable
		#region Consent Forms Queryable
		private IQueryable<ConsentForm> GetConsentForms(int clientLocationKey)
		{
			return Source.ConsentForms.Where(cf => cf.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<ConsentFormHeading> GetConsentFormHeadings(int clientLocationKey) { 
			return from cf in Source.ConsentForms
				   join cfh in Source.ConsentFormHeadings
					on  cf.ConsentFormKey equals cfh.ConsentFormKey
					where cf.ClientLocationKey == clientLocationKey
					select cfh;
		}
		private IQueryable<ConsentFormQuestion> GetConsentFormQuestions(int clientLocationKey) {
			return from cf in Source.ConsentForms
				   join cfq in Source.ConsentFormQuestions
					on cf.ConsentFormKey equals cfq.ConsentFormKey
				   where cf.ClientLocationKey == clientLocationKey
				   select cfq;
		}
		private IQueryable<ConsentQuestion> GetConsentQuestions(int clientLocationKey)
		{
			return from cf in Source.ConsentForms
				   join cfq in Source.ConsentFormQuestions
					on cf.ConsentFormKey equals cfq.ConsentFormKey
				   join cq in Source.ConsentQuestions
					on cfq.ConsentQuestionKey equals cq.ConsentQuestionKey
				   where cf.ClientLocationKey == clientLocationKey
				   select cq;
		}
		private IQueryable<ConsentQuestionLanguage> GetConsentQuestionLanguages(int clientLocationKey)
		{
			return from cf in Source.ConsentForms
				   join cfq in Source.ConsentFormQuestions
					on cf.ConsentFormKey equals cfq.ConsentFormKey
				   join cq in Source.ConsentQuestions
					on cfq.ConsentQuestionKey equals cq.ConsentQuestionKey
					join cql in Source.ConsentQuestionLanguages
					on cq.ConsentQuestionKey equals cql.ConsentQuestionKey
				   where cf.ClientLocationKey == clientLocationKey
				   select cql;
		}
		#endregion Consent Forms Queryable
		#region Custom Questions Queryable
		private IQueryable<DIN_ExperienceLevel> GetDIN_ExperienceLevelsQueryable(int clientLocationKey)
		{
			return Source.DIN_ExperienceLevels.Where(d => d.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<DIN_Height> GetDIN_HeightsQueryable(int clientLocationKey)
		{
			return Source.DIN_Heights.Where(d => d.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<DIN_ShoeSize> GetDIN_ShoeSizesQueryable(int clientLocationKey)
		{
			return Source.DIN_ShoeSizes.Where(d => d.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<DIN_Weight> GetDIN_WeightsQueryable(int clientLocationKey)
		{
			return Source.DIN_Weights.Where(d => d.ClientLocationKey == clientLocationKey);
		}
		#endregion Custom Questions Queryable
		#region Fees Queryable
		private IQueryable<Fee> GetFees(int clientLocationKey) {
			return Source.Fees.Where(f => f.ClientLocationKey == clientLocationKey);	
		}
		private IQueryable<FeeAssociation> GetFeeAssociations(int clientLocationKey) {
			return Source.FeeAssociations.Where(f => f.ClientLocationKey == clientLocationKey);
		}
		#endregion Fees Queryable
		#region Misc Queryable
		private IQueryable<ClientLocationHeardAboutU> GetClientLocationHeardAboutUs(int clientLocationKey) { 
			return Source.ClientLocationHeardAboutUs.Where(cl => cl.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<ClientLocationOtherPaymentType> GetClientLocationOtherPaymentTypes(int clientLocationKey) { 
			return Source.ClientLocationOtherPaymentTypes.Where(op => op.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<ClientLocationWeather> GetClientLocationWeathers(int clientLocationKey) { 
			return Source.ClientLocationWeathers.Where(c => c.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<DaylightSavingTimeWindow> GetDaylightSavingTimeWindows(int clientLocationKey) { 
			return Source.DaylightSavingTimeWindows.Where(d=>d.ClientLocationKey==clientLocationKey);
		}
		private IQueryable<ExplicitDay> GetExplicitDays(int clientLocationKey) { 
			return Source.ExplicitDays.Where(e => e.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<OrganizationType> GetOrganizationTypes(int clientLocationKey) { 
			return Source.OrganizationTypes.Where(o => o.ClientLocationKey == clientLocationKey);
		}
		#endregion Misc Queryable
		#region Packages Queryables
		private IQueryable<Package> GetPackages(int clientLocationKey) { 
			return Source.Packages.Where(p => p.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<PackageDetail> GetPackageDetails(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys) {

			if (copyParnterLocations)
			{
				return from p in Source.Packages
					   join pd in Source.PackageDetails
						on p.PackageKey equals pd.PackageKey
					   where p.ClientLocationKey == clientLocationKey
					   select pd;
			}
			else { 
				var packageDetails = from p in Source.Packages
						   join pd in Source.PackageDetails
						   on p.PackageKey equals pd.PackageKey
						   join eh in Source.EntityHierarchies
						   on pd.EntityHierarchyKey equals eh.EntityHierarchyKey
						   where p.ClientLocationKey == clientLocationKey && eh.ClientLocationKey == clientLocationKey
						   select pd;
				if (otherClientLocationKeys.Count > 0)
				{
					packageDetails = packageDetails.Union(from p in Source.Packages
										 join pd in Source.PackageDetails
										 on p.PackageKey equals pd.PackageKey
										 join eh in Source.EntityHierarchies
										 on pd.EntityHierarchyKey equals eh.EntityHierarchyKey
										 where p.ClientLocationKey == clientLocationKey && otherClientLocationKeys.Contains(eh.ClientLocationKey)
										 select pd);
				}
				return packageDetails;
			}
		}
		private IQueryable<PackageDetailRate> GetPackageDetailRates(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys) {
			
			var packageDetails = GetPackageDetails(clientLocationKey,copyParnterLocations,otherClientLocationKeys);

			return from pd in packageDetails
				   join pdr in Source.PackageDetailRates
				   on pd.PackageDetailKey equals pdr.PackageDetailKey
				   select pdr;
		}
		private IQueryable<PackageGroup> GetPackageGroups(int clientLocationKey) {
			return from p in Source.Packages
				   join pg in Source.PackageGroups
					on p.PackageKey equals pg.PackageKey
				   where p.ClientLocationKey == clientLocationKey
				   select pg;
		}
		private IQueryable<PackageDetailGroup> GetPackageDetailGroups(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var packageDetails = GetPackageDetails(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from pd in packageDetails
				   join pdg in Source.PackageDetailGroups
				   on pd.PackageDetailKey equals pdg.PackageDetailKey
				   select pdg;
		}
		#endregion Packages Queryables
		#region PickupRoutes Queryable
		private IQueryable<PickupRoute> GetPickupRoutes(int clientLocationKey) {
			return Source.PickupRoutes.Where(pr => pr.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<PickupRouteDay> GetPickupRouteDays(int clientLocationKey) {
			return from pr in Source.PickupRoutes
				   join prd in Source.PickupRouteDays
					on pr.PickupRouteKey equals prd.PickupRouteKey
				   where pr.ClientLocationKey == clientLocationKey
				   select prd;
		}
		private IQueryable<PickupRouteEmployee> GetPickupRouteEmployees(int clientLocationKey) {
			return from pr in Source.PickupRoutes
				   join pre in Source.PickupRouteEmployees
					on pr.PickupRouteKey equals pre.PickupRouteKey
				   where pr.ClientLocationKey == clientLocationKey
				   select pre;
		}
		private IQueryable<PickupRouteSchedule> GetPickupRouteSchedules(int clientLocationKey) {
			return from pr in Source.PickupRoutes
				   join prs in Source.PickupRouteSchedules
					on pr.PickupRouteKey equals prs.PickupRouteKey
				   where pr.ClientLocationKey == clientLocationKey
				   select prs;
		}
		private IQueryable<PickupRouteScheduleTime> GetPickupRouteScheduleTimes(int clientLocationKey) {
			return from pr in Source.PickupRoutes
				   join prs in Source.PickupRouteSchedules
					on pr.PickupRouteKey equals prs.PickupRouteKey
				   join prst in Source.PickupRouteScheduleTimes
				   on prs.PickupRouteScheduleKey equals prst.PickupRouteScheduleKey
				   where pr.ClientLocationKey == clientLocationKey
				   select prst;
		}
		private IQueryable<PickupRouteScheduleTimeDay> GetPickupRouteScheduleTimeDays(int clientLocationKey) {
			return from pr in Source.PickupRoutes
				   join prs in Source.PickupRouteSchedules
					on pr.PickupRouteKey equals prs.PickupRouteKey
				   join prst in Source.PickupRouteScheduleTimes
				   on prs.PickupRouteScheduleKey equals prst.PickupRouteScheduleKey
				   join prstd in Source.PickupRouteScheduleTimeDays
					on prst.PickupRouteScheduleTimeKey equals prstd.PickupRouteScheduleTimeKey
				   where pr.ClientLocationKey == clientLocationKey
				   select prstd;
		}
		private IQueryable<PickupRouteStop> GetPickupRouteStops(int clientLocationKey) { 
			return from pr in Source.PickupRoutes
					join prs in Source.PickupRouteStops
						on pr.PickupRouteKey equals prs.PickupRouteKey
				where pr.ClientLocationKey == clientLocationKey
				select prs;
		}
		private IQueryable<PickupRouteStopDay> GetPickupRouteStopDays(int clientLocationKey) { 
			return from pr in Source.PickupRoutes
				   join prsd in Source.PickupRouteStopDays
				   on pr.PickupRouteKey equals prsd.PickupRouteKey
				   where pr.ClientLocationKey== clientLocationKey
				   select prsd;
		}
		private IQueryable<PickupRouteVehicle> GetPickupRouteVehicles(int clientLocationKey) { 
			return from pr in Source.PickupRoutes
				   join prv in Source.PickupRouteVehicles
				   on pr.PickupRouteKey equals prv.PickupRouteKey
				   where pr.ClientLocationKey == clientLocationKey
				   select prv;
		}
		private IQueryable<PickupVehicle> GetPickupVehicles(int clientLocationKey) { 
			return Source.PickupVehicles.Where(v => v.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<PickupLocation> GetPickupLocations(int clientLocationKey) {
			return Source.PickupLocations.Where(pl => pl.ClientLocationKey == clientLocationKey);
		}
		#endregion PickupRoutes Queryable
		public void Dispose()
		{
			if (Source != null)
			{ Source.Dispose(); }
			if (Target != null)
			{ Target.Dispose(); }
		}
	}
}
