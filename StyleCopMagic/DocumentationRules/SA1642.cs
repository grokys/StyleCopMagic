// -----------------------------------------------------------------------
// <copyright file="SA1642.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.DocumentationRules
{
    using System.Linq;
    using Roslyn.Compilers.CSharp;
    using Roslyn.Services;

    public class SA1642 : SyntaxRewriter, IFixer
    {
        private SyntaxTree src;
        private Compilation compilation;
        private SemanticModel semanticModel;

        public SA1642(SyntaxTree src, Compilation compilation, ISettings settings)
        {
            this.src = src;
            this.compilation = Compilation.Create(
                "src",
                syntaxTrees: new[] { this.src });
            this.semanticModel = this.compilation.GetSemanticModel(src);
        }

        /// <summary>
        /// Repairs SA1642 (ConstructorSummaryDocumentationMustBeginWithStandardText) warnings <see cref=""/>.
        /// </summary>
        public SyntaxTree Repair()
        {
            SyntaxNode result = Visit(src.GetRoot());
            return SyntaxTree.Create(src.FilePath, (CompilationUnitSyntax)result.Format().GetFormattedRoot());
        }

        public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            SyntaxTriviaList existingTrivia = node.GetLeadingTrivia();

            // TODO: just existingTrivia.Any(SyntaxKind.DocumentationComment) should work here
            // but sometimes doc comments aren't picked up by Roslyn June 2012.
            if (!HasDocumentationComment(existingTrivia))
            {
                MethodSymbol symbol = this.semanticModel.GetDeclaredSymbol(node);
                NamedTypeSymbol containingType = symbol.ContainingType;
                string containingTypeKind = symbol.ContainingType.TypeKind.ToString().ToLower();

                string summary = string.Format(
                    "/// <summary>\n" +
                    "/// Initializes a new instance of the <see cref=\"{0}\"/> {1}.\n" +
                    "/// </summary>\n",
                    node.Identifier,
                    containingTypeKind);
                var trivia = existingTrivia.Concat(Syntax.ParseLeadingTrivia(summary));
                return node.WithLeadingTrivia(trivia);
            }

            return base.VisitConstructorDeclaration(node);
        }

        private bool HasDocumentationComment(SyntaxTriviaList existingTrivia)
        {
            return existingTrivia.Any(SyntaxKind.DocumentationComment) ||
                   existingTrivia.Any(x => x.GetText().IndexOf("<summary>") != -1);
        }
    }
}
