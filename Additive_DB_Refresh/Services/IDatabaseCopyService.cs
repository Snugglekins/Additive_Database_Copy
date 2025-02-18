using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Additive_DB_Refresh.Services
{
	public interface IDatabaseCopyService : IHostedService, IDisposable
	{
		public Task RunAsync(CancellationToken cancellationToken);
	}
}
