using Additive_DB_Refresh.Contexts;
using Additive_DB_Refresh.Extensions;
using Additive_DB_Refresh.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additive_DB_Refresh.Repositories
{
	public class SettingsRepository
	{
		private readonly SourceContext source;
		private readonly TargetContext target;
		public SettingsRepository(SourceContext _source, TargetContext _target) {
			source = _source;
			target = _target;
		}
		public async Task CopySettingsAsync() {
			List<AppointmentType> appType = await source.AppointmentTypes.ToListAsync();
			await target.SaveRangeAsync(appType);
			
			List<CapacityType> capacityTypes = await source.CapacityTypes.ToListAsync();
			await target.SaveRangeAsync(capacityTypes);

			List<Ticketing_TicketType> ticketTypes = await source.Ticketing_TicketTypes.ToListAsync();
			await target.SaveRangeAsync(ticketTypes);
			
		}
	}
}
