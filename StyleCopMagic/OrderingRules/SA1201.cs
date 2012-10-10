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
        private readonly SyntaxKind[] MemberDeclarationOrder = new[]
        {
            SyntaxKind.FieldDeclaration,
            SyntaxKind.ConstructorDeclaration,
            SyntaxKind.DestructorDeclaration,
            SyntaxKind.DelegateDeclaration,
            SyntaxKind.EventFieldDeclaration,
            SyntaxKind.EnumDeclaration,
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
            SyntaxKindComparer comparer = new SyntaxKindComparer(MemberDeclarationOrder);

            var orderedMembers = node.Members
                .OrderBy(x => x.Kind, comparer);

            return node.WithMembers(Syntax.List<MemberDeclarationSyntax>(orderedMembers));
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
