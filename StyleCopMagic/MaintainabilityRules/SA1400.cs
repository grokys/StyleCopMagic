// -----------------------------------------------------------------------
// <copyright file="SA1400.cs" company="">
// BSD Licence
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.MaintainabilityRules
{
    using System.Linq;
    using Roslyn.Compilers.CSharp;

    public class SA1400 : RuleRewriter
    {
        public override SyntaxNode Visit(SyntaxNode node)
        {
            if (node != null)
            {
                switch (node.Kind)
                {
                    case SyntaxKind.ClassDeclaration:
                    case SyntaxKind.EnumDeclaration:
                    case SyntaxKind.FieldDeclaration:
                    case SyntaxKind.MethodDeclaration:
                    case SyntaxKind.PropertyDeclaration:
                    case SyntaxKind.StructDeclaration:
                    case SyntaxKind.InterfaceDeclaration:
                        if (!HasAccessModifiers(node) && 
                            !IsInterfaceMember(node) && 
                            node.GetExplicitInterfaceSpecifier() == null)
                        {
                            node = AddModifiers(node, GetTypeDeclarationModifier(node));
                        }
                        break;
                }
            }

            return base.Visit(node);
        }

        private bool HasAccessModifiers(SyntaxNode node)
        {
            return node.GetModifiers().Any(x =>
                x.Kind == SyntaxKind.PublicKeyword ||
                x.Kind == SyntaxKind.ProtectedKeyword ||
                x.Kind == SyntaxKind.InternalKeyword ||
                x.Kind == SyntaxKind.PrivateKeyword);
        }

        private bool IsInterfaceMember(SyntaxNode node)
        {
            TypeDeclarationSyntax containingType = node.Parent.FirstAncestorOrSelf<TypeDeclarationSyntax>();
            return containingType != null && containingType.Kind == SyntaxKind.InterfaceDeclaration;
        }

        private SyntaxToken GetTypeDeclarationModifier(SyntaxNode node)
        {
            if (node is InterfaceDeclarationSyntax)
            {
                return Syntax.Token(SyntaxKind.PublicKeyword);
            }
            else if (node is ClassDeclarationSyntax ||
                     node is EnumDeclarationSyntax ||
                     node is StructDeclarationSyntax)
            {
                bool nested = node.Parent.FirstAncestorOrSelf<TypeDeclarationSyntax>(x =>
                    x.Kind == SyntaxKind.ClassDeclaration ||
                    x.Kind == SyntaxKind.StructDeclaration) != null;
                SyntaxKind modifier = nested ? SyntaxKind.PrivateKeyword : SyntaxKind.InternalKeyword;
                return Syntax.Token(modifier);
            }
            else
            {
                return Syntax.Token(SyntaxKind.PrivateKeyword);
            }
        }

        private SyntaxNode AddModifiers(SyntaxNode node, SyntaxToken syntaxToken)
        {
            // Get the first child node.
            SyntaxNode firstNode = node.ChildNodes().FirstOrDefault();

            if (firstNode != null)
            {
                // The leading trivia will be the method comments, if any.
                SyntaxTriviaList leadingTrivia = firstNode.GetLeadingTrivia();

                // Replace it with one without leading trivia.
                node = node.ReplaceNodes(
                    new[] { firstNode },
                    (a, b) => firstNode.WithLeadingTrivia());

                // Add the leading trivia to the new modifier.
                syntaxToken = syntaxToken.WithLeadingTrivia(leadingTrivia);
            }

            return node.AddModifiers(syntaxToken);
        }
    }
}
