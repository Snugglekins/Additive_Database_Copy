using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.Extensions;
using Additive_DB_Refresh.Models;
using Additive_DB_Refresh.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additive_DB_Refresh.DataStreams
{
	public class SystemTablesStream
	{
		private SourceContext source;
		private TargetContext target;
		private ILogger<SystemTablesStream> Logger;
		public SystemTablesStream(SourceContext _source, TargetContext _target, ILogger<SystemTablesStream> logger) {
			source = _source;
			target = _target;
			Logger = logger;
			source.Database.SetCommandTimeout(0);
			target.Database.SetCommandTimeout(0);
		}
		public async Task CopySystemTablesAsync() {

			Logger.LogInformation("Begin copying system tables");


			//await Migrator<CardType>.MigrateByteIdentity(source.CardTypes, target);
			await target.InsertUpdateAsync(source.AddressTypes);
			await target.InsertUpdateAsync(source.ApplicationObjects);
			await target.InsertUpdateAsync(source.ApplicationObjectTypes);
			await target.InsertUpdateAsync(source.CapacityTypes);
			await target.InsertUpdateAsync(source.AppointmentTypes);
			await target.InsertUpdateAsync(source.BookingAgentTypes);
			await target.InsertUpdateAsync(source.CapacityTypes);
			//Used Bulk Insert b/c EF Core cannot handle Identity column values of 0
			//Card Types has a value of 0 for Other
			await target.BulkInsertAsync(source, source.CardTypes);
			await target.InsertUpdateAsync(source.CashDrawer_EventTypes);
			await target.InsertUpdateAsync(source.ClientLocationScheduleLimitations);
			await target.InsertUpdateAsync(source.ClientLocationScheduleWhens);
			await target.InsertUpdateAsync(source.CommissionTypes);
			await target.InsertUpdateAsync(source.Countries);
			await target.InsertUpdateAsync(source.CrossSellEventTypes);
			await target.InsertUpdateAsync(source.DaysOfTheWeeks);
			await target.InsertUpdateAsync(source.DeviceTypes);
			await target.InsertUpdateAsync(source.DirectionTypes);
			await target.InsertUpdateAsync(source.DiscountApplicationTypes);
			await target.InsertUpdateAsync(source.DiscountTypes);
			await target.InsertUpdateAsync(source.EntryModes);
			await target.InsertUpdateAsync(source.EventTypes);
			await target.InsertUpdateAsync(source.FeeTypes);
			await target.InsertUpdateAsync(source.Genders);
			await target.InsertUpdateAsync(source.Languages);
			await target.InsertUpdateAsync(source.Merchandise_RentalNoteTypes);
			await target.InsertUpdateAsync(source.MessageTemplateTypes);
			await target.InsertUpdateAsync(source.NotificationSystem_NotificationDeliveryTypes);
			await target.InsertUpdateAsync(source.NotificationSystem_NotificationTypes);
			await target.InsertUpdateAsync(source.OnlineTravelAgencies);
			await target.InsertUpdateAsync(source.OpeningTriggers);
			await target.InsertUpdateAsync(source.OrderStatuses);
			await target.InsertUpdateAsync(source.PackageTypes);
			await target.InsertUpdateAsync(source.PaymentGateways);
			//PaymentMethodTypes has a key = 0 value for Other, cannot use InsertUpdateAsync
			await target.BulkInsertAsync(source,source.PaymentMethodTypes);
			await target.InsertUpdateAsync(source.PaymentProcessors);
			await target.InsertUpdateAsync(source.PaymentTypes);
			await target.InsertUpdateAsync(source.PhoneTypes);
			await target.InsertUpdateAsync(source.PhotoPackageTypes);
			await target.InsertUpdateAsync(source.PhotoSystemTypes);
			await target.InsertUpdateAsync(source.PickupOrderEventTypes);
			await target.InsertUpdateAsync(source.PickupStatuses);
			await target.InsertUpdateAsync(source.ProcessingTypes);
			await target.InsertUpdateAsync(source.RateClasses);
			await target.InsertUpdateAsync(source.RefundMethodTypes);
			await target.InsertUpdateAsync(source.RefundTypes);
			await target.InsertUpdateAsync(source.remediate_MessageQueueFixerConfigurations);
			await target.InsertUpdateAsync(source.remediate_ProcessOrderResultTypes);
			await target.InsertUpdateAsync(source.ResponseTypes);
			await target.InsertUpdateAsync(source.ScheduleTypes);
			await target.InsertUpdateAsync(source.Services);
			await target.InsertUpdateAsync(source.Square_CardBrandEnums);
			await target.InsertUpdateAsync(source.Square_CurrencyCodes);
			await target.InsertUpdateAsync(source.Square_EntryMethodEnums);
			await target.InsertUpdateAsync(source.Square_SquareSDKTypes);
			await target.InsertUpdateAsync(source.Square_StatusEnums);
			await target.InsertUpdateAsync(source.States);
			await target.InsertUpdateAsync(source.System_ApplicationRoles);
			await target.InsertUpdateAsync(source.System_ApplicationRolesApplicationObjects);
			await target.InsertUpdateAsync(source.System_AppVersions);
			await target.InsertUpdateAsync(source.System_Colors);
			await target.InsertUpdateAsync(source.System_EmailTypes);
			await target.InsertUpdateAsync(source.System_MessageTypes);
			//await target.InsertUpdateAsync(source.System_OrderStatusEnums);
			//No PK on OrderStatusEnums, standard process won't work
			await target.BulkInsertAsync(source, source.System_OrderStatusEnums);
			await target.InsertUpdateAsync(source.System_ProcessingStatuses);
			await target.InsertUpdateAsync(source.System_TimeZones);
			await target.InsertAsync(source.System_ZCTAs);
			await target.InsertUpdateAsync(source.SystemEntityTypes);
			await target.InsertUpdateAsync(source.Ticketing_TicketTypes);
			await target.InsertUpdateAsync(source.TransactionTypes);


			Logger.LogInformation("Completed copying system tables");
		}

	}
}
