using AnalyzerTest.Models;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzerTest.ModelPartials
{
	// TODO : Implement me
	public class ClientPartial : Client
	{
		public override bool Equals(object? obj)
		{
			if (obj is Client other)
			{
				return ClientKey == other.ClientKey;
			}
			else
			{
				return false;
			}
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(ClientKey);
			
		}
	}
}
