﻿using Additive_DB_Refresh.Contexts;
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
		private SourceContext source { get; set; }
		private TargetContext target { get; set; }

		private string TargetDatabaseName;
		public ClientLocationStream(IDbContextFactory<SourceContext> sourceFactory, IDbContextFactory<TargetContext> targetFactory, ILogger<ClientLocationStream> logger, string targetDatabase)
		{
			Logger = logger;
			SourceFactory = sourceFactory;
			TargetFactory = targetFactory;
			TargetDatabaseName = targetDatabase;

		}
		public async Task CopyClientLocationAsync(int clientLocationKey, bool copyPartnerLocations, List<int> otherClientLocations)
		{
			Logger.LogInformation("Copying Client Location {0}", clientLocationKey);
			source = await SourceFactory.CreateDbContextNoTimeoutAsync();
			target = await TargetFactory.CreateDbContextAsync(TargetDatabaseName);

			await target.InsertUpdateAsync(GetClientLocation(clientLocationKey));

			#region Load Security
			Logger.LogInformation("Copying Client Location {0} Security", clientLocationKey);
			await target.InsertUpdateAsync(GetClientLocationRoles(clientLocationKey));
			await target.InsertUpdateAsync(GetClientLocationRoleApplicationObjects(clientLocationKey));
			await target.InsertUpdateAsync(GetClientLocationLogins(clientLocationKey));
			await target.InsertUpdateAsync(GetClientLocationLoginRoles(clientLocationKey));

			#endregion Load Security
			#region Load Activities
			Logger.LogInformation("Copying Client Location {0} Activities", clientLocationKey);
			source = await SourceFactory.CreateDbContextNoTimeoutAsync();
			target = await TargetFactory.CreateDbContextAsync(TargetDatabaseName);

			await target.InsertUpdateAsync(GetClientLocationEntities(clientLocationKey));
			await target.InsertUpdateAsync(GetEntityHierarchies(clientLocationKey));
			await target.InsertUpdateAsync(GetEntityHierarchyEmployees(clientLocationKey));
			await target.InsertUpdateAsync(GetEntityHierarchyInventories(clientLocationKey));
			await target.InsertUpdateAsync(GetEntityHierarchyTranslations(clientLocationKey));
			await target.InsertUpdateAsync(GetEntityHierarchyRates(clientLocationKey));
			await target.InsertUpdateAsync(GetHistory_EntityHierarchyRates(clientLocationKey));
			await target.InsertUpdateAsync(GetEntityHierarchyRateResources(clientLocationKey));
			await target.InsertUpdateAsync(GetEntityHierarchyOptions(clientLocationKey));
			await target.InsertUpdateAsync(GetEntityHierarchyOptionRates(clientLocationKey));
			await target.InsertUpdateAsync(GetClientLocationEntitySchedules(clientLocationKey));
			await target.InsertUpdateAsync(GetClientLocationScheduleTimes(clientLocationKey));
			await target.InsertUpdateAsync(GetClientLocationScheduleTimeDays(clientLocationKey),10000);
			await target.InsertUpdateAsync(GetClientLocationScheduleTimeDayRates(clientLocationKey), 10000);
			await target.InsertUpdateAsync(GetClientLocationEntityScheduleHours(clientLocationKey));
			await target.InsertUpdateAsync(GetClientLocationEntityScheduleRates(clientLocationKey));
			await target.InsertUpdateAsync(GetClientLocationEntityScheduleResources(clientLocationKey));
			await target.InsertUpdateAsync(GetClientLocationScheduleDaysEnums(clientLocationKey), 10000);

			#endregion Load Activities
			#region Load Merchandise
			Logger.LogInformation("Copying Client Location {0} Merchandise", clientLocationKey);
			source = await SourceFactory.CreateDbContextNoTimeoutAsync();
			target = await TargetFactory.CreateDbContextAsync(TargetDatabaseName);

			await target.InsertUpdateAsync(GetMerchandise_Products(clientLocationKey));
			await target.InsertUpdateAsync(GetMerchandise_ProductCategories(clientLocationKey));
			await target.InsertUpdateAsync(GetMerchandise_ProductCategoryAssociations(clientLocationKey));
			await target.InsertAsync(GetMerchandise_Inventory(clientLocationKey));
			await target.InsertUpdateAsync(GetMerchandise_ProductColors(clientLocationKey));
			await target.InsertUpdateAsync(GetMerchandise_ProductFlavors(clientLocationKey));
			await target.InsertUpdateAsync(GetMerchandise_ProductSizes(clientLocationKey));
			await target.InsertUpdateAsync(GetMerchandise_ProductVariablePrices(clientLocationKey));
			await target.InsertUpdateAsync(GetHistory_ProductVariablePrices(clientLocationKey));
			await target.InsertAsync(GetMerchandise_InventoryChangeLog(clientLocationKey), 10000);
			#endregion Load Merchandise
			Logger.LogInformation("Copying Client Location {0} Photo Packages, Cash Drawer, Communication", clientLocationKey);
			#region Load PhotoPackages
			await target.InsertUpdateAsync(GetPhotoPackages(clientLocationKey));
			#endregion Load PhototPackages
			#region Load CashDrawer
			await target.InsertUpdateAsync(GetRegisters(clientLocationKey));
			#endregion Load CashDrawer
			#region Load Communication
			await target.InsertUpdateAsync(GetEmailTemplates(clientLocationKey));
			await target.InsertUpdateAsync(GetEmailTemplateSections(clientLocationKey));
			#endregion Load Communication
			#region BookingAgents
			Logger.LogInformation("Copying Client Location {0} Booking Agents", clientLocationKey);
			await target.InsertUpdateAsync(GetBookingAgents(clientLocationKey, copyPartnerLocations, otherClientLocations));
			await target.InsertUpdateAsync(GetBookingAgentEntityHierarchies(clientLocationKey,copyPartnerLocations, otherClientLocations));
			await target.InsertUpdateAsync(GetBookingAgentEntityHierarchyRates(clientLocationKey, copyPartnerLocations, otherClientLocations));
			await target.InsertUpdateAsync(GetClientLocationEntityScheduleBookingAgents(clientLocationKey,copyPartnerLocations, otherClientLocations));
			await target.InsertUpdateAsync(GetClientLocationEntityScheduleBookingAgentRates(clientLocationKey, copyPartnerLocations, otherClientLocations));
			await target.InsertUpdateAsync(GetClientLocationScheduleTimeBookingAgents(clientLocationKey, copyPartnerLocations, otherClientLocations));
			await target.InsertUpdateAsync(GetClientLocationScheduleDayBookingAgentRates(clientLocationKey, copyPartnerLocations, otherClientLocations));
			await target.InsertUpdateAsync(GetClientLocationScheduleTimeDayBookingAgentRateEnums(clientLocationKey, copyPartnerLocations, otherClientLocations), 10000);
			#endregion BookingAgents
			#region Load Consent Forms
			Logger.LogInformation("Copying Client Location {0} Consent Forms", clientLocationKey);
			await target.InsertUpdateAsync(GetConsentForms(clientLocationKey));
			//Consent Form Headings has no primary key defined
			await target.BulkInsertAsync(GetConsentFormHeadings(clientLocationKey));
			await target.InsertUpdateAsync(GetConsentFormQuestions(clientLocationKey));
			await target.InsertUpdateAsync(GetConsentQuestions(clientLocationKey));
			//Consent Question Languages has no primary key defined
			await target.BulkInsertAsync(GetConsentQuestionLanguages(clientLocationKey));
			#endregion Load Consent Forms
			#region Load Custom Forms
			Logger.LogInformation("Copying Client Location {0} Custom Forms", clientLocationKey);
			source = await SourceFactory.CreateDbContextNoTimeoutAsync();
			target = await TargetFactory.CreateDbContextAsync(TargetDatabaseName);
			await target.InsertUpdateAsync(GetDIN_ExperienceLevelsQueryable(clientLocationKey));
			await target.InsertUpdateAsync(GetDIN_HeightsQueryable(clientLocationKey));
			await target.InsertUpdateAsync(GetDIN_ShoeSizesQueryable(clientLocationKey));
			await target.InsertUpdateAsync(GetDIN_WeightsQueryable(clientLocationKey));
			#endregion Load Custom Forms
			#region Load Fees
			Logger.LogInformation("Copying Client Location {0} Fees", clientLocationKey);
			await target.InsertUpdateAsync(GetFees(clientLocationKey));
			await target.InsertUpdateAsync(GetFeeAssociations(clientLocationKey));
			#endregion Load Fees
			#region Load Misc
			Logger.LogInformation("Copying Client Location {0} Misc", clientLocationKey);
			await target.InsertUpdateAsync(GetClientLocationHeardAboutUs(clientLocationKey));
			await target.InsertUpdateAsync(GetClientLocationOtherPaymentTypes(clientLocationKey));
			await target.InsertUpdateAsync(GetClientLocationWeathers(clientLocationKey));
			await target.InsertUpdateAsync(GetDaylightSavingTimeWindows(clientLocationKey));
			await target.InsertUpdateAsync(GetExplicitDays(clientLocationKey));
			await target.InsertUpdateAsync(GetOrganizationTypes(clientLocationKey));
			#endregion Load Misc
			#region Load Packages
			Logger.LogInformation("Copying Client Location {0} Packages", clientLocationKey);
			await target.InsertUpdateAsync(GetPackages(clientLocationKey));
			await target.InsertUpdateAsync(GetPackageDetails(clientLocationKey,copyPartnerLocations, otherClientLocations));
			await target.InsertUpdateAsync(GetPackageDetailRates(clientLocationKey,copyPartnerLocations, otherClientLocations));
			await target.InsertUpdateAsync(GetPackageGroups(clientLocationKey));
			await target.InsertUpdateAsync(GetPackageDetailGroups(clientLocationKey,copyPartnerLocations, otherClientLocations));
			#endregion Load Packages
			#region Load PickupRoutes
			Logger.LogInformation("Copying Client Location {0} Pickup Routes", clientLocationKey);
			await target.InsertUpdateAsync(GetPickupRoutes(clientLocationKey));
			await target.InsertUpdateAsync(GetPickupRouteDays(clientLocationKey));
			await target.InsertUpdateAsync(GetPickupRouteEmployees(clientLocationKey));
			await target.InsertUpdateAsync(GetPickupRouteSchedules(clientLocationKey));
			await target.InsertUpdateAsync(GetPickupRouteScheduleTimes(clientLocationKey));
			await target.InsertUpdateAsync(GetPickupRouteScheduleTimeDays(clientLocationKey));
			await target.InsertUpdateAsync(GetPickupRouteStops(clientLocationKey));
			await target.InsertUpdateAsync(GetPickupRouteStopDays(clientLocationKey));
			await target.InsertUpdateAsync(GetPickupRouteVehicles(clientLocationKey));
			await target.InsertUpdateAsync(GetPickupVehicles(clientLocationKey));
			await target.InsertUpdateAsync(GetPickupLocations(clientLocationKey));
			#endregion Load PickupRoutes

		}
		#region Security Queryable
		private IQueryable<ClientLocation> GetClientLocation(int clientLocationKey)
		{
			return source.ClientLocations.Where(cl => clientLocationKey == cl.ClientLocationKey);
		}
		private IQueryable<ClientLocationRole> GetClientLocationRoles(int clientLocationKey) { 
			return source.ClientLocationRoles.Where(clr => clr.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<ClientLocationLogin> GetClientLocationLogins(int clientLocationKey) {
			return source.ClientLocationLogins.Where(cll => cll.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<ClientLocationLoginRole> GetClientLocationLoginRoles(int clientLocationKey) {
			return from cllr in source.ClientLocationLoginRoles
				   join cll in source.ClientLocationLogins on cllr.ClientLocationLoginKey equals cll.ClientLocationLoginKey
				   where cll.ClientLocationKey == clientLocationKey
				   select cllr;	   
		}
		private IQueryable<ClientLocationRoleApplicationObject> GetClientLocationRoleApplicationObjects(int clientLocationKey) {
			return from clrao in source.ClientLocationRoleApplicationObjects
				   join clr in source.ClientLocationRoles on clrao.ClientLocationRoleKey equals clr.ClientLocationRoleKey
				   where clr.ClientLocationKey == clientLocationKey
				   select clrao;
		}
		#endregion Security Queryable
		#region Activities Queryable
		private IQueryable<ClientLocationEntity> GetClientLocationEntities(int clientLocationKey) { 
			return source.ClientLocationEntities.Where(cle => cle.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<EntityHierarchy> GetEntityHierarchies(int clientLocationKey) { 
			return source.EntityHierarchies.Where(eh => eh.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<EntityHierarchyEmployee> GetEntityHierarchyEmployees(int clientLocationKey) { 
			return from eh in source.EntityHierarchies
				   join ehe in source.EntityHierarchyEmployees
					on eh.EntityHierarchyKey equals ehe.EntityHierarchyKey
				   where eh.ClientLocationKey == clientLocationKey
				   select ehe;
		}
		private IQueryable<EntityHierarchyInventory> GetEntityHierarchyInventories(int clientLocationKey)
		{
			return from eh in source.EntityHierarchies
				   join ehi in source.EntityHierarchyInventories
					on eh.EntityHierarchyKey equals ehi.EntityHierarchyKey
				   where eh.ClientLocationKey == clientLocationKey
				   select ehi;
		}
		private IQueryable<EntityHierarchyTranslation> GetEntityHierarchyTranslations(int clientLocationKey) {
			return from eh in source.EntityHierarchies
				   join eht in source.EntityHierarchyTranslations
					on eh.EntityHierarchyKey equals eht.EntityHierarchyKey
				   where eh.ClientLocationKey == clientLocationKey
				   select eht;
		}
		private IQueryable<EntityHierarchyRate> GetEntityHierarchyRates(int clientLocationKey) { 
			return from ehr in source.EntityHierarchyRates
					join eh in source.EntityHierarchies on ehr.EntityHierarchyKey equals eh.EntityHierarchyKey
				where eh.ClientLocationKey == clientLocationKey
				select ehr;
		}
		private IQueryable<History_EntityHierarchyRate> GetHistory_EntityHierarchyRates(int clientLocationKey) { 
			return from hehr in source.History_EntityHierarchyRates
					join eh in source.EntityHierarchies on hehr.EntityHierarchyKey equals eh.EntityHierarchyKey
				where eh.ClientLocationKey == clientLocationKey
				select hehr;
		}
		private IQueryable<EntityHierarchyRateResource> GetEntityHierarchyRateResources(int clientLocationKey) {
			return from ehr in source.EntityHierarchyRates
				   join eh in source.EntityHierarchies on ehr.EntityHierarchyKey equals eh.EntityHierarchyKey
				   join ehrr in source.EntityHierarchyRateResources on ehr.EntityHierarchyRateKey equals ehrr.EntityHierarchyRateKey
				   where eh.ClientLocationKey == clientLocationKey
				   select ehrr;
		}
		private IQueryable<EntityHierarchyOption> GetEntityHierarchyOptions(int clientLocationKey)
		{
			return from eh in source.EntityHierarchies
				   join eho in source.EntityHierarchyOptions
					on eh.EntityHierarchyKey equals eho.EntityHierarchyKey
				   where eh.ClientLocationKey == clientLocationKey
				   select eho;
		}
		private IQueryable<EntityHierarchyOptionRate> GetEntityHierarchyOptionRates(int clientLocationKey)
		{
			return from eh in source.EntityHierarchies
				   join eho in source.EntityHierarchyOptions
					on eh.EntityHierarchyKey equals eho.EntityHierarchyKey
					join ehor in source.EntityHierarchyOptionRates
					on eho.EntityHierarchyOptionKey equals ehor.EntityHierarchyOptionKey
				where eh.ClientLocationKey == clientLocationKey
				   select ehor;
		}
		private IQueryable<ClientLocationEntitySchedule> GetClientLocationEntitySchedules(int clientLocationKey) { 
			return source.ClientLocationEntitySchedules.Where(c => c.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<ClientLocationScheduleTime> GetClientLocationScheduleTimes(int clientLocationKey) {
			return from cles in source.ClientLocationEntitySchedules
				   join clst in source.ClientLocationScheduleTimes on cles.ClientLocationEntityScheduleKey equals clst.ClientLocationEntityScheduleKey
				   where cles.ClientLocationKey == clientLocationKey
				   select clst;
		}
		private IQueryable<ClientLocationScheduleTimeDay> GetClientLocationScheduleTimeDays(int clientLocationKey)
		{
			return from cles in source.ClientLocationEntitySchedules
				   join clst in source.ClientLocationScheduleTimes on cles.ClientLocationEntityScheduleKey equals clst.ClientLocationEntityScheduleKey
				   join clstd in source.ClientLocationScheduleTimeDays on clst.ClientLocationScheduleTimeKey equals clstd.ClientLocationScheduleTimeKey
				   where cles.ClientLocationKey == clientLocationKey
				   select clstd;
		}
		private IQueryable<ClientLocationScheduleTimeDayRate> GetClientLocationScheduleTimeDayRates(int clientLocationKey)
		{
			return from cles in source.ClientLocationEntitySchedules
				   join clst in source.ClientLocationScheduleTimes 
						on cles.ClientLocationEntityScheduleKey equals clst.ClientLocationEntityScheduleKey
				   join clstd in source.ClientLocationScheduleTimeDays 
						on clst.ClientLocationScheduleTimeKey equals clstd.ClientLocationScheduleTimeKey
				   join clstdr in source.ClientLocationScheduleTimeDayRates
						on new { clstd.ClientLocationScheduleTimeKey,  clstd.ClientLocationScheduleTimeDayKey } 
							equals new { clstdr.ClientLocationScheduleTimeKey , ClientLocationScheduleTimeDayKey = clstdr.ClientLocationScheduleTimeDayKey.GetValueOrDefault(-1) }
				   where cles.ClientLocationKey == clientLocationKey
				   select clstdr;
		}
		private IQueryable<ClientLocationEntityScheduleHour> GetClientLocationEntityScheduleHours(int clientLocationKey) {
			return from cles in source.ClientLocationEntitySchedules
				   join clesh in source.ClientLocationEntityScheduleHours
						on cles.ClientLocationEntityScheduleKey equals clesh.ClientLocationEntityScheduleKey
				   where cles.ClientLocationKey == clientLocationKey
				   select clesh;
		}
		private IQueryable<ClientLocationEntityScheduleRate> GetClientLocationEntityScheduleRates(int clientLocationKey) { 
			return from cles in source.ClientLocationEntitySchedules
				   join clesr in source.ClientLocationEntityScheduleRates
						on cles.ClientLocationEntityScheduleKey equals clesr.ClientLocationEntityScheduleKey
				   where cles.ClientLocationKey == clientLocationKey
				   select clesr;
		}
		private IQueryable<ClientLocationEntityScheduleResource> GetClientLocationEntityScheduleResources(int clientLocationKey) {
			return from cles in source.ClientLocationEntitySchedules
				   join clesr in source.ClientLocationEntityScheduleResources
						on cles.ClientLocationEntityScheduleKey equals clesr.ClientLocationEntityScheduleKey
				   where cles.ClientLocationKey == clientLocationKey
				   select clesr;
		}

		private IQueryable<ClientLocationScheduleDaysEnum> GetClientLocationScheduleDaysEnums(int clientLocationKey) { 
			return source.ClientLocationScheduleDaysEnums.Where(e => e.ClientLocationKey == clientLocationKey);
		}

		#endregion Activites Queryable
		#region Merchandise Queryable
		private IQueryable<Merchandise_Product> GetMerchandise_Products(int clientLocationKey)
		{
			return source.Merchandise_Products.Where(p => p.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<Merchandise_ProductCategory> GetMerchandise_ProductCategories(int clientLocationKey)
		{
			return source.Merchandise_ProductCategories.Where(p => p.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<Merchandise_ProductCategoryAssociation> GetMerchandise_ProductCategoryAssociations(int clientLocationKey)
		{
			return source.Merchandise_ProductCategoryAssociations.Where(p => p.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<Merchandise_Inventory> GetMerchandise_Inventory(int clientLocationKey)
		{
			return from p in source.Merchandise_Products
				   join i in source.Merchandise_Inventories on p.ProductKey equals i.ProductKey
				   where p.ClientLocationKey == clientLocationKey
				   select i;
		}
		private IQueryable<Merchandise_InventoryChangeLog> GetMerchandise_InventoryChangeLog(int clientLocationKey)
		{
			return from p in source.Merchandise_Products
				   join i in source.Merchandise_InventoryChangeLogs on p.ProductKey equals i.ProductKey
				   where p.ClientLocationKey == clientLocationKey
				   select i;
		}
		private IQueryable<Merchandise_ProductColor> GetMerchandise_ProductColors(int clientLocationKey)
		{
			return source.Merchandise_ProductColors.Where(pc => pc.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<Merchandise_ProductFlavor> GetMerchandise_ProductFlavors(int clientLocationKey)
		{
			return source.Merchandise_ProductFlavors.Where(pf => pf.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<Merchandise_ProductSize> GetMerchandise_ProductSizes(int clientLocationKey)
		{
			return source.Merchandise_ProductSizes.Where(ps => ps.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<Merchandise_ProductVariablePrice> GetMerchandise_ProductVariablePrices(int clientLocationKey)
		{
			return from p in source.Merchandise_Products
				   join pvp in source.Merchandise_ProductVariablePrices on p.ProductKey equals pvp.ProductKey
				   where p.ClientLocationKey == clientLocationKey
				   select pvp;
		}
		private IQueryable<History_ProductVariablePrice> GetHistory_ProductVariablePrices(int clientLocationKey)
		{
			return from p in source.Merchandise_Products
				   join pvp in source.History_ProductVariablePrices on p.ProductKey equals pvp.ProductKey
				   where p.ClientLocationKey == clientLocationKey
				   select pvp;
		}
		#endregion Merchandise Queryable
		#region PhotoPackage Queryable
		public IQueryable<PhotoPackage> GetPhotoPackages(int clientLocationKey) {
			return from cle in source.ClientLocationEntities
				   join pp in source.PhotoPackages
					   on cle.ClientLocationEntityKey equals pp.ClientLocationEntityKey
				   where cle.ClientLocationKey == clientLocationKey
				   select pp;
		}
		#endregion PhotoPackage Queryable
		#region CashDrawer Queryable
		private IQueryable<Register> GetRegisters(int clientLocationKey) {
			return source.Registers.Where(r => r.ClientLocationKey == clientLocationKey);
		}

		#endregion CashDrawer Queryable
		#region Communication Queryable
		private IQueryable<EmailTemplate> GetEmailTemplates(int clientLocationKey) { 
			return source.EmailTemplates.Where(e => e.ClientLocationKey==clientLocationKey);
		}
		private IQueryable<EmailTemplateSection> GetEmailTemplateSections(int clientLocationKey) {
			return from et in source.EmailTemplates
				   join ets in source.EmailTemplateSections
					on et.EmailTemplateKey equals ets.EmailTemplateKey
					where et.ClientLocationKey == clientLocationKey
				   select ets;
		}

		#endregion Communication Queryable
		#region BookingAgents Queryable

		private IQueryable<BookingAgent> GetBookingAgents(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			if (copyParnterLocations) {
				return source.BookingAgents.Where(ba => ba.ClientLocationKey == clientLocationKey);
			}
			else {
			var bookingAgents =  source.BookingAgents.Where(ba => ba.ClientLocationKey == clientLocationKey && ba.PartnerClientLocationKey == null);
				if (otherClientLocationKeys.Count() > 0) {
					//otherClientLocationKeys.Contains(ba.PartnerClientLocationKey.GetValueOrDefault(-1))
					bookingAgents = bookingAgents.Union(source.BookingAgents.Where(ba => ba.ClientLocationKey == clientLocationKey && otherClientLocationKeys.Contains(ba.PartnerClientLocationKey.GetValueOrDefault(-1))));
				}
				return bookingAgents;
			}
		}
		private IQueryable<BookingAgentEntityHierarchy> GetBookingAgentEntityHierarchies(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from ba in bookingAgents
				   join baeh in source.BookingAgentEntityHierarchies on ba.BookingAgentKey equals baeh.BookingAgentKey
				   select baeh;
		}
		private IQueryable<BookingAgentEntityHierarchyRate> GetBookingAgentEntityHierarchyRates(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys) {
			{
				var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

				return from ba in bookingAgents
					   join baehr in source.BookingAgentEntityHierarchyRates on ba.BookingAgentKey equals baehr.BookingAgentKey
					   select baehr;
			}
		}
		private IQueryable<ClientLocationEntityScheduleBookingAgent> GetClientLocationEntityScheduleBookingAgents(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from ba in bookingAgents
				   join clesba in source.ClientLocationEntityScheduleBookingAgents
						on ba.BookingAgentKey equals clesba.BookingAgentKey
				select clesba;
		}
		private IQueryable<ClientLocationEntityScheduleBookingAgentRate> GetClientLocationEntityScheduleBookingAgentRates(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from ba in bookingAgents
				   join clesbar in source.ClientLocationEntityScheduleBookingAgentRates
						on ba.BookingAgentKey equals clesbar.BookingAgentKey
				   select clesbar;
		}
		private IQueryable<ClientLocationScheduleTimeBookingAgent> GetClientLocationScheduleTimeBookingAgents(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from ba in bookingAgents
				   join clstba in source.ClientLocationScheduleTimeBookingAgents
						on ba.BookingAgentKey equals clstba.BookingAgentKey
				   select clstba;
		}
		private IQueryable<ClientLocationScheduleDayBookingAgentRate> GetClientLocationScheduleDayBookingAgentRates(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from ba in bookingAgents
				   join clsdbar in source.ClientLocationScheduleDayBookingAgentRates
					on ba.BookingAgentKey equals clsdbar.BookingAgentKey
				   select clsdbar;
		}
		private IQueryable<ClientLocationScheduleTimeDayBookingAgentRateEnum> GetClientLocationScheduleTimeDayBookingAgentRateEnums(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var bookingAgents = GetBookingAgents(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from ba in bookingAgents
				   join clstdbare in source.ClientLocationScheduleTimeDayBookingAgentRateEnums
				   on ba.BookingAgentKey equals clstdbare.BookingAgentKey
				   select clstdbare;
		}
		#endregion BookingAgents Queryable
		#region Consent Forms Queryable
		private IQueryable<ConsentForm> GetConsentForms(int clientLocationKey)
		{
			return source.ConsentForms.Where(cf => cf.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<ConsentFormHeading> GetConsentFormHeadings(int clientLocationKey) { 
			return from cf in source.ConsentForms
				   join cfh in source.ConsentFormHeadings
					on  cf.ConsentFormKey equals cfh.ConsentFormKey
					where cf.ClientLocationKey == clientLocationKey
					select cfh;
		}
		private IQueryable<ConsentFormQuestion> GetConsentFormQuestions(int clientLocationKey) {
			return from cf in source.ConsentForms
				   join cfq in source.ConsentFormQuestions
					on cf.ConsentFormKey equals cfq.ConsentFormKey
				   where cf.ClientLocationKey == clientLocationKey
				   select cfq;
		}
		private IQueryable<ConsentQuestion> GetConsentQuestions(int clientLocationKey)
		{
			return from cf in source.ConsentForms
				   join cfq in source.ConsentFormQuestions
					on cf.ConsentFormKey equals cfq.ConsentFormKey
				   join cq in source.ConsentQuestions
					on cfq.ConsentQuestionKey equals cq.ConsentQuestionKey
				   where cf.ClientLocationKey == clientLocationKey
				   select cq;
		}
		private IQueryable<ConsentQuestionLanguage> GetConsentQuestionLanguages(int clientLocationKey)
		{
			return from cf in source.ConsentForms
				   join cfq in source.ConsentFormQuestions
					on cf.ConsentFormKey equals cfq.ConsentFormKey
				   join cq in source.ConsentQuestions
					on cfq.ConsentQuestionKey equals cq.ConsentQuestionKey
					join cql in source.ConsentQuestionLanguages
					on cq.ConsentQuestionKey equals cql.ConsentQuestionKey
				   where cf.ClientLocationKey == clientLocationKey
				   select cql;
		}
		#endregion Consent Forms Queryable
		#region Custom Questions Queryable
		private IQueryable<DIN_ExperienceLevel> GetDIN_ExperienceLevelsQueryable(int clientLocationKey)
		{
			return source.DIN_ExperienceLevels.Where(d => d.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<DIN_Height> GetDIN_HeightsQueryable(int clientLocationKey)
		{
			return source.DIN_Heights.Where(d => d.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<DIN_ShoeSize> GetDIN_ShoeSizesQueryable(int clientLocationKey)
		{
			return source.DIN_ShoeSizes.Where(d => d.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<DIN_Weight> GetDIN_WeightsQueryable(int clientLocationKey)
		{
			return source.DIN_Weights.Where(d => d.ClientLocationKey == clientLocationKey);
		}
		#endregion Custom Questions Queryable
		#region Fees Queryable
		private IQueryable<Fee> GetFees(int clientLocationKey) {
			return source.Fees.Where(f => f.ClientLocationKey == clientLocationKey);	
		}
		private IQueryable<FeeAssociation> GetFeeAssociations(int clientLocationKey) {
			return source.FeeAssociations.Where(f => f.ClientLocationKey == clientLocationKey);
		}
		#endregion Fees Queryable
		#region Misc Queryable
		private IQueryable<ClientLocationHeardAboutU> GetClientLocationHeardAboutUs(int clientLocationKey) { 
			return source.ClientLocationHeardAboutUs.Where(cl => cl.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<ClientLocationOtherPaymentType> GetClientLocationOtherPaymentTypes(int clientLocationKey) { 
			return source.ClientLocationOtherPaymentTypes.Where(op => op.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<ClientLocationWeather> GetClientLocationWeathers(int clientLocationKey) { 
			return source.ClientLocationWeathers.Where(c => c.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<DaylightSavingTimeWindow> GetDaylightSavingTimeWindows(int clientLocationKey) { 
			return source.DaylightSavingTimeWindows.Where(d=>d.ClientLocationKey==clientLocationKey);
		}
		private IQueryable<ExplicitDay> GetExplicitDays(int clientLocationKey) { 
			return source.ExplicitDays.Where(e => e.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<OrganizationType> GetOrganizationTypes(int clientLocationKey) { 
			return source.OrganizationTypes.Where(o => o.ClientLocationKey == clientLocationKey);
		}
		#endregion Misc Queryable
		#region Packages Queryables
		private IQueryable<Package> GetPackages(int clientLocationKey) { 
			return source.Packages.Where(p => p.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<PackageDetail> GetPackageDetails(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys) {

			if (copyParnterLocations)
			{
				return from p in source.Packages
					   join pd in source.PackageDetails
						on p.PackageKey equals pd.PackageKey
					   where p.ClientLocationKey == clientLocationKey
					   select pd;
			}
			else { 
				var packageDetails = from p in source.Packages
						   join pd in source.PackageDetails
						   on p.PackageKey equals pd.PackageKey
						   join eh in source.EntityHierarchies
						   on pd.EntityHierarchyKey equals eh.EntityHierarchyKey
						   where p.ClientLocationKey == clientLocationKey && eh.ClientLocationKey == clientLocationKey
						   select pd;
				if (otherClientLocationKeys.Count > 0)
				{
					packageDetails = packageDetails.Union(from p in source.Packages
										 join pd in source.PackageDetails
										 on p.PackageKey equals pd.PackageKey
										 join eh in source.EntityHierarchies
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
				   join pdr in source.PackageDetailRates
				   on pd.PackageDetailKey equals pdr.PackageDetailKey
				   select pdr;
		}
		private IQueryable<PackageGroup> GetPackageGroups(int clientLocationKey) {
			return from p in source.Packages
				   join pg in source.PackageGroups
					on p.PackageKey equals pg.PackageKey
				   where p.ClientLocationKey == clientLocationKey
				   select pg;
		}
		private IQueryable<PackageDetailGroup> GetPackageDetailGroups(int clientLocationKey, bool copyParnterLocations, List<int> otherClientLocationKeys)
		{
			var packageDetails = GetPackageDetails(clientLocationKey, copyParnterLocations, otherClientLocationKeys);

			return from pd in packageDetails
				   join pdg in source.PackageDetailGroups
				   on pd.PackageDetailKey equals pdg.PackageDetailKey
				   select pdg;
		}
		#endregion Packages Queryables
		#region PickupRoutes Queryable
		private IQueryable<PickupRoute> GetPickupRoutes(int clientLocationKey) {
			return source.PickupRoutes.Where(pr => pr.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<PickupRouteDay> GetPickupRouteDays(int clientLocationKey) {
			return from pr in source.PickupRoutes
				   join prd in source.PickupRouteDays
					on pr.PickupRouteKey equals prd.PickupRouteKey
				   where pr.ClientLocationKey == clientLocationKey
				   select prd;
		}
		private IQueryable<PickupRouteEmployee> GetPickupRouteEmployees(int clientLocationKey) {
			return from pr in source.PickupRoutes
				   join pre in source.PickupRouteEmployees
					on pr.PickupRouteKey equals pre.PickupRouteKey
				   where pr.ClientLocationKey == clientLocationKey
				   select pre;
		}
		private IQueryable<PickupRouteSchedule> GetPickupRouteSchedules(int clientLocationKey) {
			return from pr in source.PickupRoutes
				   join prs in source.PickupRouteSchedules
					on pr.PickupRouteKey equals prs.PickupRouteKey
				   where pr.ClientLocationKey == clientLocationKey
				   select prs;
		}
		private IQueryable<PickupRouteScheduleTime> GetPickupRouteScheduleTimes(int clientLocationKey) {
			return from pr in source.PickupRoutes
				   join prs in source.PickupRouteSchedules
					on pr.PickupRouteKey equals prs.PickupRouteKey
				   join prst in source.PickupRouteScheduleTimes
				   on prs.PickupRouteScheduleKey equals prst.PickupRouteScheduleKey
				   where pr.ClientLocationKey == clientLocationKey
				   select prst;
		}
		private IQueryable<PickupRouteScheduleTimeDay> GetPickupRouteScheduleTimeDays(int clientLocationKey) {
			return from pr in source.PickupRoutes
				   join prs in source.PickupRouteSchedules
					on pr.PickupRouteKey equals prs.PickupRouteKey
				   join prst in source.PickupRouteScheduleTimes
				   on prs.PickupRouteScheduleKey equals prst.PickupRouteScheduleKey
				   join prstd in source.PickupRouteScheduleTimeDays
					on prst.PickupRouteScheduleTimeKey equals prstd.PickupRouteScheduleTimeKey
				   where pr.ClientLocationKey == clientLocationKey
				   select prstd;
		}
		private IQueryable<PickupRouteStop> GetPickupRouteStops(int clientLocationKey) { 
			return from pr in source.PickupRoutes
					join prs in source.PickupRouteStops
						on pr.PickupRouteKey equals prs.PickupRouteKey
				where pr.ClientLocationKey == clientLocationKey
				select prs;
		}
		private IQueryable<PickupRouteStopDay> GetPickupRouteStopDays(int clientLocationKey) { 
			return from pr in source.PickupRoutes
				   join prsd in source.PickupRouteStopDays
				   on pr.PickupRouteKey equals prsd.PickupRouteKey
				   where pr.ClientLocationKey== clientLocationKey
				   select prsd;
		}
		private IQueryable<PickupRouteVehicle> GetPickupRouteVehicles(int clientLocationKey) { 
			return from pr in source.PickupRoutes
				   join prv in source.PickupRouteVehicles
				   on pr.PickupRouteKey equals prv.PickupRouteKey
				   where pr.ClientLocationKey == clientLocationKey
				   select prv;
		}
		private IQueryable<PickupVehicle> GetPickupVehicles(int clientLocationKey) { 
			return source.PickupVehicles.Where(v => v.ClientLocationKey == clientLocationKey);
		}
		private IQueryable<PickupLocation> GetPickupLocations(int clientLocationKey) {
			return source.PickupLocations.Where(pl => pl.ClientLocationKey == clientLocationKey);
		}
		#endregion PickupRoutes Queryable
		public void Dispose()
		{
			if (source != null)
			{ source.Dispose(); }
			if (target != null)
			{ target.Dispose(); }
		}
	}
}
