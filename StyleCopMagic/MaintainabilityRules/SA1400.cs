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
        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (node.Modifiers.Count == 0 && !IsInterfaceMember(node))
            {
                return node.AddModifiers(GetTypeDeclarationModifier(node));
            }
            else
            {
                return base.VisitClassDeclaration(node);
            }
        }

        public override SyntaxNode VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            if (node.Modifiers.Count == 0 && !IsInterfaceMember(node))
            {
                return AddModifiers(node, GetTypeDeclarationModifier(node));
            }
            else
            {
                return base.VisitEnumDeclaration(node);
            }
        }

        public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            if (node.Modifiers.Count == 0 && !IsInterfaceMember(node))
            {
                return AddModifiers(node, Syntax.Token(SyntaxKind.PrivateKeyword));
            }
            else
            {
                return base.VisitFieldDeclaration(node);
            }
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.Modifiers.Count == 0 && !IsInterfaceMember(node) && node.ExplicitInterfaceSpecifier == null)
            {
                return AddModifiers(node, Syntax.Token(SyntaxKind.PrivateKeyword));
            }
            else
            {
                return base.VisitMethodDeclaration(node);
            }
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (node.Modifiers.Count == 0 && !IsInterfaceMember(node) && node.ExplicitInterfaceSpecifier == null)
            {
                return AddModifiers(node, Syntax.Token(SyntaxKind.PrivateKeyword));
            }
            else
            {
                return base.VisitPropertyDeclaration(node);
            }
        }

        public override SyntaxNode VisitStructDeclaration(StructDeclarationSyntax node)
        {
            if (node.Modifiers.Count == 0 && !IsInterfaceMember(node))
            {
                return AddModifiers(node, GetTypeDeclarationModifier(node));
            }
            else
            {
                return base.VisitStructDeclaration(node);
            }
        }

        public override SyntaxNode VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            if (node.Modifiers.Count == 0 && !IsInterfaceMember(node))
            {
                return AddModifiers(node, Syntax.Token(SyntaxKind.PublicKeyword));
            }
            else
            {
                return base.VisitInterfaceDeclaration(node);
            }
        }

        private bool IsInterfaceMember(SyntaxNode node)
        {
            TypeDeclarationSyntax containingType = node.Parent.FirstAncestorOrSelf<TypeDeclarationSyntax>();
            return containingType != null && containingType.Kind == SyntaxKind.InterfaceDeclaration;
        }

        private SyntaxToken GetTypeDeclarationModifier(SyntaxNode node)
        {
            bool nested = node.Parent.FirstAncestorOrSelf<TypeDeclarationSyntax>(x =>
                x.Kind == SyntaxKind.ClassDeclaration ||
                x.Kind == SyntaxKind.StructDeclaration) != null;
            SyntaxKind modifier = nested ? SyntaxKind.PrivateKeyword : SyntaxKind.InternalKeyword;
            return Syntax.Token(modifier);
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
