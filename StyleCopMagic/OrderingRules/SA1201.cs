// -----------------------------------------------------------------------
// <copyright file="SA1201.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.OrderingRules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Roslyn.Compilers.CSharp;

    public class SA1201 : RuleRewriter
    {
        private readonly SyntaxKind[] DeclarationOrder = new[]
        {
            SyntaxKind.ExternAliasDirective,
            SyntaxKind.UsingDirective,
            SyntaxKind.NamespaceDeclaration,
            SyntaxKind.FieldDeclaration,
            SyntaxKind.ConstructorDeclaration,
            SyntaxKind.DestructorDeclaration,
            SyntaxKind.DelegateDeclaration,
            SyntaxKind.EventFieldDeclaration,
            SyntaxKind.EnumDeclaration,
            SyntaxKind.InterfaceDeclaration,
            SyntaxKind.PropertyDeclaration,
            SyntaxKind.IndexerDeclaration,
            SyntaxKind.MethodDeclaration,
            SyntaxKind.StructDeclaration,
            SyntaxKind.ClassDeclaration,
        };

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            // TODO: Need to handle whitespace better here, but holding out and hoping that comes
            // with a future Roslyn. I know whitespace handling is going to change in future 
            // anyway...
            SyntaxKindComparer comparer = new SyntaxKindComparer(DeclarationOrder);

            var orderedMembers = node.Members
                .OrderBy(x => x.Kind, comparer);

            node = node.WithMembers(Syntax.List<MemberDeclarationSyntax>(orderedMembers));

            return base.VisitClassDeclaration(node);
        }

        public override SyntaxNode VisitStructDeclaration(StructDeclarationSyntax node)
        {
            // TODO: Need to handle whitespace better here, but holding out and hoping that comes
            // with a future Roslyn. I know whitespace handling is going to change in future 
            // anyway...
            SyntaxKindComparer comparer = new SyntaxKindComparer(DeclarationOrder);

            var orderedMembers = node.Members
                .OrderBy(x => x.Kind, comparer);

            node = node.WithMembers(Syntax.List<MemberDeclarationSyntax>(orderedMembers));

            return base.VisitStructDeclaration(node);
        }

        public override SyntaxNode VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            // TODO: Need to handle whitespace better here, but holding out and hoping that comes
            // with a future Roslyn. I know whitespace handling is going to change in future 
            // anyway...
            SyntaxKindComparer comparer = new SyntaxKindComparer(DeclarationOrder);

            var orderedMembers = node.Members
                .OrderBy(x => x.Kind, comparer);

            node = node.WithMembers(Syntax.List<MemberDeclarationSyntax>(orderedMembers));

            return base.VisitInterfaceDeclaration(node);
        }

        public override SyntaxNode VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            // TODO: Need to handle whitespace better here, but holding out and hoping that comes
            // with a future Roslyn. I know whitespace handling is going to change in future 
            // anyway...
            SyntaxKindComparer comparer = new SyntaxKindComparer(DeclarationOrder);

            var orderedMembers = node.Members
                .OrderBy(x => x.Kind, comparer);

            node = node.WithMembers(Syntax.List<MemberDeclarationSyntax>(orderedMembers));

            return base.VisitNamespaceDeclaration(node);
        }

        private class SyntaxKindComparer : IComparer<SyntaxKind>
        {
            private SyntaxKind[] order;

            public SyntaxKindComparer(SyntaxKind[] order)
            {
                this.order = order;
            }

            public int Compare(SyntaxKind x, SyntaxKind y)
            {
                return Array.IndexOf(this.order, x) - Array.IndexOf(this.order, y);
            }
        }
    }
}
