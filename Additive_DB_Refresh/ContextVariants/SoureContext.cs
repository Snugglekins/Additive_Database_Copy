using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additive_DB_Refresh.Contexts
{
	public class SourceContext(DbContextOptions<SourceContext> options) : RefreshProcessContext(options)
	{
	}
}
