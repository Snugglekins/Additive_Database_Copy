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
		private readonly SourceContext Source;
		private readonly TargetContext Target;
		private readonly ILogger<SystemTablesStream> Logger;
		public SystemTablesStream(SourceContext source, TargetContext target, ILogger<SystemTablesStream> logger) {
			Source = source;
			Target = target;
			Logger = logger;
			Source.Database.SetCommandTimeout(0);
			Target.Database.SetCommandTimeout(0);
		}
		public async Task CopySystemTablesAsync() {

			Logger.LogInformation("Begin copying system tables");

			//await Migrator<CardType>.MigrateByteIdentity(Source.CardTypes, target);
			await Target.InsertUpdateAsync(Source.AddressTypes);
			await Target.InsertUpdateAsync(Source.ApplicationObjects);
			await Target.InsertUpdateAsync(Source.ApplicationObjectTypes);
			await Target.InsertUpdateAsync(Source.CapacityTypes);
			await Target.InsertUpdateAsync(Source.AppointmentTypes);
			await Target.InsertUpdateAsync(Source.BookingAgentTypes);
			await Target.InsertUpdateAsync(Source.CapacityTypes);
			//Used Bulk Insert b/c EF Core cannot handle Identity column values of 0
			//Card Types has a value of 0 for Other
			await Target.BulkInsertAsync(Source, Source.CardTypes);
			await Target.InsertUpdateAsync(Source.CashDrawer_EventTypes);
			await Target.InsertUpdateAsync(Source.ClientLocationScheduleLimitations);
			await Target.InsertUpdateAsync(Source.ClientLocationScheduleWhens);
			await Target.InsertUpdateAsync(Source.CommissionTypes);
			await Target.InsertUpdateAsync(Source.Countries);
			await Target.InsertUpdateAsync(Source.CrossSellEventTypes);
			await Target.InsertUpdateAsync(Source.DaysOfTheWeeks);
			await Target.InsertUpdateAsync(Source.DeviceTypes);
			await Target.InsertUpdateAsync(Source.DirectionTypes);
			await Target.InsertUpdateAsync(Source.DiscountApplicationTypes);
			await Target.InsertUpdateAsync(Source.DiscountTypes);
			await Target.InsertUpdateAsync(Source.EntryModes);
			await Target.InsertUpdateAsync(Source.EventTypes);
			await Target.InsertUpdateAsync(Source.FeeTypes);
			await Target.InsertUpdateAsync(Source.Genders);
			await Target.InsertUpdateAsync(Source.Languages);
			await Target.InsertUpdateAsync(Source.Merchandise_RentalNoteTypes);
			await Target.InsertUpdateAsync(Source.MessageTemplateTypes);
			await Target.InsertUpdateAsync(Source.NotificationSystem_NotificationDeliveryTypes);
			await Target.InsertUpdateAsync(Source.NotificationSystem_NotificationTypes);
			await Target.InsertUpdateAsync(Source.OnlineTravelAgencies);
			await Target.InsertUpdateAsync(Source.OpeningTriggers);
			await Target.InsertUpdateAsync(Source.OrderStatuses);
			await Target.InsertUpdateAsync(Source.PackageTypes);
			await Target.InsertUpdateAsync(Source.PaymentGateways);
			//PaymentMethodTypes has a key = 0 value for Other, cannot use InsertUpdateAsync
			await Target.BulkInsertAsync(Source,Source.PaymentMethodTypes);
			await Target.InsertUpdateAsync(Source.PaymentProcessors);
			await Target.InsertUpdateAsync(Source.PaymentTypes);
			await Target.InsertUpdateAsync(Source.PhoneTypes);
			await Target.InsertUpdateAsync(Source.PhotoPackageTypes);
			await Target.InsertUpdateAsync(Source.PhotoSystemTypes);
			await Target.InsertUpdateAsync(Source.PickupOrderEventTypes);
			await Target.InsertUpdateAsync(Source.PickupStatuses);
			await Target.InsertUpdateAsync(Source.ProcessingTypes);
			await Target.InsertUpdateAsync(Source.RateClasses);
			await Target.InsertUpdateAsync(Source.RefundMethodTypes);
			await Target.InsertUpdateAsync(Source.RefundTypes);
			await Target.InsertUpdateAsync(Source.remediate_MessageQueueFixerConfigurations);
			await Target.InsertUpdateAsync(Source.remediate_ProcessOrderResultTypes);
			await Target.InsertUpdateAsync(Source.ResponseTypes);
			await Target.InsertUpdateAsync(Source.ScheduleTypes);
			await Target.InsertUpdateAsync(Source.Services);
			await Target.InsertUpdateAsync(Source.Square_CardBrandEnums);
			await Target.InsertUpdateAsync(Source.Square_CurrencyCodes);
			await Target.InsertUpdateAsync(Source.Square_EntryMethodEnums);
			await Target.InsertUpdateAsync(Source.Square_SquareSDKTypes);
			await Target.InsertUpdateAsync(Source.Square_StatusEnums);
			await Target.InsertUpdateAsync(Source.States);
			await Target.InsertUpdateAsync(Source.System_ApplicationRoles);
			await Target.InsertUpdateAsync(Source.System_ApplicationRolesApplicationObjects);
			await Target.InsertUpdateAsync(Source.System_AppVersions);
			await Target.InsertUpdateAsync(Source.System_Colors);
			await Target.InsertUpdateAsync(Source.System_EmailTypes);
			await Target.InsertUpdateAsync(Source.System_MessageTypes);
			//await Target.InsertUpdateAsync(Source.System_OrderStatusEnums);
			//No PK on OrderStatusEnums, standard process won't work
			await Target.BulkInsertAsync(Source, Source.System_OrderStatusEnums);
			await Target.InsertUpdateAsync(Source.System_ProcessingStatuses);
			await Target.InsertUpdateAsync(Source.System_TimeZones);
			await Target.InsertAsync(Source.System_ZCTAs);
			await Target.InsertUpdateAsync(Source.SystemEntityTypes);
			await Target.InsertUpdateAsync(Source.Ticketing_TicketTypes);
			await Target.InsertUpdateAsync(Source.TransactionTypes);

			Logger.LogInformation("Completed copying system tables");
		}

	}
}
