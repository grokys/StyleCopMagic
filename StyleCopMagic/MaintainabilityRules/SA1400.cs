// -----------------------------------------------------------------------
// <copyright file="SA1400.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.MaintainabilityRules
{
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
                return node.AddModifiers(GetTypeDeclarationModifier(node));
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
                return node.AddModifiers(Syntax.Token(SyntaxKind.PrivateKeyword));
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
                return node.AddModifiers(Syntax.Token(SyntaxKind.PrivateKeyword));
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
                return node.AddModifiers(Syntax.Token(SyntaxKind.PrivateKeyword));
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
                return node.AddModifiers(GetTypeDeclarationModifier(node));
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
                return node.AddModifiers(Syntax.Token(SyntaxKind.PublicKeyword));
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
    }
}
