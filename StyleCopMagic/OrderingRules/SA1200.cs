// -----------------------------------------------------------------------
// <copyright file="SA1200.cs" company="">
// BSD Licence
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.OrderingRules
{
    using System.Linq;
    using Roslyn.Compilers.CSharp;

    public class SA1200 : RuleRewriter
    {
        public override SyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
        {
            UsingDirectiveSyntax[] usingNodes = node.ChildNodes().Where(x => x.Kind == SyntaxKind.UsingDirective).Cast<UsingDirectiveSyntax>().ToArray();
            NamespaceDeclarationSyntax namespaceNode = (NamespaceDeclarationSyntax)node.ChildNodes().FirstOrDefault(x => x.Kind == SyntaxKind.NamespaceDeclaration);

            if (usingNodes.Length > 0 && namespaceNode != null)
            {
                SyntaxTriviaList leadingTrivia = usingNodes[0].GetLeadingTrivia();

                // Remove the existing using statements.
                node = node.ReplaceNodes(usingNodes, (a, b) => null);

                // Leading comment trivia will be added to the namespace. This allows file comments
                // to remain at the top of the file.
                var namespaceTrivia = leadingTrivia.TakeWhile(x =>
                    x.Kind == SyntaxKind.SingleLineCommentTrivia ||
                    x.Kind == SyntaxKind.EndOfLineTrivia);

                // All other trivia will be preserved on the first using directive.
                var preservedTrivia = leadingTrivia.Skip(namespaceTrivia.Count());
                usingNodes[0] = usingNodes[0].WithLeadingTrivia(preservedTrivia);

                // Leading trivia from the namespace needs to be added as trailing trivia to the 
                // last using. This allows for #regions around the using statements (as of the 
                // Roslyn Sept 2012 CTP, the #endregion appears as leading trivia on the namespace)
                // TODO: See if this is still necessary with the next CTP.
                var trailingTrivia = usingNodes.Last().GetTrailingTrivia()
                    .Concat(namespaceNode.GetLeadingTrivia());

                usingNodes[usingNodes.Length - 1] = usingNodes.Last().WithTrailingTrivia(trailingTrivia);

                // Add any using directives already on the namespace.
                var namespaceUsings = usingNodes.Concat(namespaceNode.Usings);

                // Create a new namespace statment with the usings and the leading trivia we removed earlier.
                SyntaxNode newNamespaceNode = namespaceNode
                    .WithUsings(Syntax.List<UsingDirectiveSyntax>(namespaceUsings))
                    .WithLeadingTrivia(namespaceTrivia);

                // Replace the namespace with the one with usings.
                node = node.ReplaceNodes(
                    node.ChildNodes().Take(1),
                    (a, b) => newNamespaceNode);

            }

            return node;
        }
    }
}
