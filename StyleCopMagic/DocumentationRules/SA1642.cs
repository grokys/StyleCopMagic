// -----------------------------------------------------------------------
// <copyright file="SA1642.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.DocumentationRules
{
    using System.Linq;
    using Roslyn.Compilers.CSharp;

    public class SA1642 : RuleRewriter
    {
        private SemanticModel semanticModel;

        public SA1642(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
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
