using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additive_DB_Refresh.Contexts
{
	public partial class RefreshProcessContext
	{
		protected RefreshProcessContext(DbContextOptions options) : base(options) { }
	}
}
