using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ModelPartial
{
	[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ModelPartialCodeFixProvider)), Shared]
	public class ModelPartialCodeFixProvider : CodeFixProvider
	{
		private string logLocation = String.Empty;
		private string logFullName = String.Empty;
		public sealed override ImmutableArray<string> FixableDiagnosticIds
		{
			get { return ImmutableArray.Create(ModelPartialAnalyzer.DiagnosticId); }
		}

		public sealed override FixAllProvider GetFixAllProvider()
		{
			logLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ModelPartialLogs");
			logFullName = Path.Combine(logLocation, $"{DateTime.Now:yyyyMMdd}_DiagnosticAnalyzer.log");
			Directory.CreateDirectory(logLocation);//Create directory if it does not exist
			WriteFile("Fixer provider executing");
												   // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
			return WellKnownFixAllProviders.BatchFixer;
		}

		public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
		{
			WriteFile("Registering code fixes");
			var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

			// TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest
			var diagnostic = context.Diagnostics.First();
			var diagnosticSpan = diagnostic.Location.SourceSpan;

			// Find the type declaration identified by the diagnostic.
			var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<ClassDeclarationSyntax>().First();

			// Register a code action that will invoke the fix.
			context.RegisterCodeFix(
				CodeAction.Create(
					title: CodeFixResources.CodeFixTitle,
					createChangedSolution: c => CreatePartialFile(context.Document, declaration, c),
					equivalenceKey: nameof(CodeFixResources.CodeFixTitle)),
				diagnostic);
		}
		private async Task<Solution> CreatePartialFile(Document document, ClassDeclarationSyntax classDeclaration, CancellationToken cancellationToken) {

			WriteFile("Code fix partial file creation executing");
			var project = document.Project;
			var workspace = project.Solution.Workspace;

			// Get the class name
			var className = classDeclaration.Identifier.Text;
			var partialClassName = $"{className}Partial";

			// Generate the content for the new file
			var partialClassDeclaration = SyntaxFactory.ClassDeclaration(className)
				.AddModifiers(SyntaxFactory.Token(SyntaxKind.PartialKeyword))
				.NormalizeWhitespace();

			var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("Models"))
				.AddMembers(partialClassDeclaration)
				.NormalizeWhitespace();

			var compilationUnit = SyntaxFactory.CompilationUnit()
				.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")))
				.AddMembers(namespaceDeclaration)
				.NormalizeWhitespace();

			var newFileContent = compilationUnit.ToFullString();

			// Determine the target folder and file path
			var currentFilePath = document.FilePath ?? throw new InvalidOperationException("Document has no file path.");
			var projectDirectory = Path.GetDirectoryName(project.FilePath) ?? string.Empty;
			var modelPartialsPath = Path.Combine(projectDirectory, "ModelPartials");

			// Generate the new file path
			var newFileName = $"{partialClassName}.cs";
			var newFilePath = Path.Combine(modelPartialsPath, newFileName);

			// Write the new file
			File.WriteAllText(newFilePath, newFileContent);

			

			// Return the original document (no modifications made to it)
			return project.Solution;

		}
		private void WriteFile(string msg)
		{
			using (StreamWriter sw = new StreamWriter(logFullName, append: true))
			{
				sw.WriteLine(FormattedLine(msg));
				sw.Flush();
			}
		}
		private string FormattedLine(string msg)
		{
			return $"{String.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))}	{msg}";
		}
		//private async Task<Solution> MakeUppercaseAsync(Document document, TypeDeclarationSyntax typeDecl, CancellationToken cancellationToken)
		//{
		//	// Compute new uppercase name.
		//	var identifierToken = typeDecl.Identifier;
		//	var newName = identifierToken.Text.ToUpperInvariant();

		//	// Get the symbol representing the type to be renamed.
		//	var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
		//	var typeSymbol = semanticModel.GetDeclaredSymbol(typeDecl, cancellationToken);

		//	// Produce a new solution that has all references to that type renamed, including the declaration.
		//	var originalSolution = document.Project.Solution;
		//	var optionSet = originalSolution.Workspace.Options;
		//	var newSolution = await Renamer.RenameSymbolAsync(document.Project.Solution, typeSymbol, newName, optionSet, cancellationToken).ConfigureAwait(false);

		//	// Return the new solution with the now-uppercase type name.
		//	return newSolution;
		//}
	}
}
