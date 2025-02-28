using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace ModelPartial
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class ModelPartialAnalyzer : DiagnosticAnalyzer
	{
		public const string DiagnosticId = "MP0001";

		// You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
		// See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Localizing%20Analyzers.md for more on localization
		private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
		private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
		private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
		private const string Category = "Usage";

		private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description
			, customTags: new[] { WellKnownDiagnosticTags.NotConfigurable, WellKnownDiagnosticTags.Telemetry, WellKnownDiagnosticTags.Compiler });

		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

//		private string logLocation = String.Empty;
//		private string logFullName = String.Empty;
		
		public override void Initialize(AnalysisContext context)
		{
			context.EnableConcurrentExecution();

			// TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
			// See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
			context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.ClassDeclaration);
			//Force code to analyze generated code
			context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);

			//logLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ModelPartialLogs");
			//logFullName = Path.Combine(logLocation, $"{DateTime.Now:yyyyMMdd}_DiagnosticAnalyzer.log");
//			Directory.CreateDirectory(logLocation);//Create directory if it does not exist
		
		}

		private void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
		{
			try
			{


				//Get the namespace
				var classNode = context.Node;
				var classSymbol = context.SemanticModel.GetDeclaredSymbol(classNode);
				var nmspc = classSymbol.ContainingNamespace.MetadataName;

				//Only test objects in the Models namespace
				if (nmspc != "Models")
				{
					return;
				}

				//Get the class name
				var classDeclaration = (ClassDeclarationSyntax)context.Node;
				var className = classDeclaration.Identifier.Text;
				
				//context.GetLocation().SourceTree?.FilePath

				//WriteFile($"Namespace : {nmspc} Class :{className}");

				// Get the file path of the current syntax node
				var syntaxTree = classNode.SyntaxTree;
				string filePath = String.Empty;
				if (syntaxTree != null)
				{
					filePath = syntaxTree.FilePath;
				}

				//Get base file path of project (this is known and specific to this project structure
				var directory = new System.IO.DirectoryInfo(filePath).Parent.Parent;
				
				//Target directory
				var partialClassName = className + "Partial";

				//string directory = Path.GetDirectoryName(filePath);
				var partialDir = Path.Combine(directory.FullName, "ModelPartials");
				var partialClassPath = Path.Combine(partialDir, $"{partialClassName}.cs");

				//WriteFile($"PartialModel Class {partialClassName} Filepath : {partialClassPath}");

				//if (!classDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword))
				if (!File.Exists(partialClassPath))
				{
					//WriteFile($"Warning : PartialModel Class {partialClassName} does not exist @ Filepath : {partialClassPath}");

					
					var diagnostic = Diagnostic.Create(
						Rule,
						classDeclaration.Identifier.GetLocation(),
						classDeclaration.Identifier.Text
						);

					//WriteFile($"Id: {diagnostic.Id} Loc: {diagnostic.Location} Desc : {diagnostic.Descriptor} Rule : {Rule.Description} RuleIsEnabled : {Rule.IsEnabledByDefault}");
					//WriteFile($"Class : {classDeclaration.Identifier.Text} Location : {classDeclaration.Identifier.GetLocation()}");
					//WriteFile($"DiagnosticDescriptor Rule = new DiagnosticDescriptor(\"{diagnostic.Id}\", \"{Title}\", \"{MessageFormat}\",\"{Category}\", DiagnosticSeverity.Warning, isEnabledByDefault: true, description: {Description});");
					
					context.ReportDiagnostic(diagnostic);

				}
				//else
				//{
				//	WriteFile($"OK : PartialModel Class {partialClassName} exists @ Filepath : {partialClassPath}");
				//}
			}
			catch  {
				//WriteFile(ex.Message);
				throw;
			}
		}

		//private void WriteFile(string msg) {
		//	using (StreamWriter sw = new StreamWriter(logFullName,append:true)) { 
		//		sw.WriteLine(FormattedLine(msg));
		//		sw.Flush();
		//	}
		//}
		//private string FormattedLine(string msg) {
		//	return $"{String.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))}	{msg}";
		//}
	}
}
