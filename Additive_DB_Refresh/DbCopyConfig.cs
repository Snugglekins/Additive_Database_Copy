using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additive_DB_Refresh
{
	public class DbCopyConfig
	{
		public string DestinationDatabase { get; set; } = String.Empty;
		public List<int> ClientLocationKeys { get; set; } = new();
		public bool? CopyPartnerLocations { get; set; };
		public string? UsersList { get; set; }
		public bool? CopyLinkedOrders { get; set; }

	}
}
