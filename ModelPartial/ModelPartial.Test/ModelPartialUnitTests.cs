using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using VerifyCS = ModelPartial.Test.CSharpCodeFixVerifier<
	ModelPartial.ModelPartialAnalyzer,
	ModelPartial.ModelPartialCodeFixProvider>;

namespace ModelPartial.Test
{
	[TestClass]
	public class ModelPartialUnitTest
	{
		//No diagnostics expected to show up
		[TestMethod]
		public async Task TestMethod1()
		{
			var test = @"";

			await VerifyCS.VerifyAnalyzerAsync(test);
		}
		[TestMethod]
		public async Task Test_Models_Namespace_HasPartial() {
			var test = @"
			  using System;
			  using System.Collections.Generic;
			  using System.Linq;
			  using System.Text;
			  using System.Threading.Tasks;
			  using System.Diagnostics;

			  namespace ConsoleApplication1.Models
			  {
			      class {|#0:HasFile|}
			      {   
			      }
			  }";
			await VerifyCS.VerifyAnalyzerAsync(test);
		}
		[TestMethod]
		public async Task Test_Models_Namespace_NoPartial()
		{
			var test = @"
			  using System;
			  using System.Collections.Generic;
			  using System.Linq;
			  using System.Text;
			  using System.Threading.Tasks;
			  using System.Diagnostics;

			  namespace ConsoleApplication1.Models
			  {
			      class {|#0:NoFile|}
			      {   
			      }
			  }";


			await VerifyCS.VerifyAnalyzerAsync(test);
		}
		[TestMethod]
		public async Task Test_NotModels_Namespace() {
			var test = @"
			  using System;
			  using System.Collections.Generic;
			  using System.Linq;
			  using System.Text;
			  using System.Threading.Tasks;
			  using System.Diagnostics;

			  namespace ConsoleApplication1.NotModels
			  {
			      class {|#0:TypeName|}
			      {   
			      }
			  }";

			await VerifyCS.VerifyAnalyzerAsync(test);
		}
		//Diagnostic and CodeFix both triggered and checked for
		[TestMethod]
		public async Task TestMethod2()
		{
			var test = @"
		  using System;
		  using System.Collections.Generic;
		  using System.Linq;
		  using System.Text;
		  using System.Threading.Tasks;
		  using System.Diagnostics;

		  namespace ConsoleApplication1
		  {
		      class {|#0:TypeName|}
		      {   
		      }
		  }";

			var fixtest = @"
		  using System;
		  using System.Collections.Generic;
		  using System.Linq;
		  using System.Text;
		  using System.Threading.Tasks;
		  using System.Diagnostics;

		  namespace ConsoleApplication1
		  {
		      class TYPENAME
		      {   
		      }
		  }";

			DiagnosticResult expected = new DiagnosticResult(); // VerifyCS.Diagnostic("ModelPartial").WithLocation(0).WithArguments("TypeName");
			await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
		}
	}
}
