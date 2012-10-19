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

    public class SA1201_SA1202 : RuleRewriter
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

        private readonly SyntaxKind[][] AccessLevelOrder = new[]
        {
            new[] { SyntaxKind.PublicKeyword },
            new[] { SyntaxKind.InternalKeyword },
            new[] { SyntaxKind.InternalKeyword, SyntaxKind.ProtectedKeyword },
            new[] { SyntaxKind.ProtectedKeyword },
            new[] { SyntaxKind.PrivateKeyword },
        };

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            // TODO: Need to handle whitespace better here, but holding out and hoping that comes
            // with a future Roslyn. I know whitespace handling is going to change in future 
            // anyway...
            SyntaxKindComparer comparer = new SyntaxKindComparer(DeclarationOrder, AccessLevelOrder);

            var orderedMembers = node.Members.OrderBy(x => x, comparer);

            node = node.WithMembers(Syntax.List<MemberDeclarationSyntax>(orderedMembers));

            return base.VisitClassDeclaration(node);
        }

        public override SyntaxNode VisitStructDeclaration(StructDeclarationSyntax node)
        {
            // TODO: Need to handle whitespace better here, but holding out and hoping that comes
            // with a future Roslyn. I know whitespace handling is going to change in future 
            // anyway...
            SyntaxKindComparer comparer = new SyntaxKindComparer(DeclarationOrder, AccessLevelOrder);

            var orderedMembers = node.Members.OrderBy(x => x, comparer);

            node = node.WithMembers(Syntax.List<MemberDeclarationSyntax>(orderedMembers));

            return base.VisitStructDeclaration(node);
        }

        public override SyntaxNode VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            // TODO: Need to handle whitespace better here, but holding out and hoping that comes
            // with a future Roslyn. I know whitespace handling is going to change in future 
            // anyway...
            SyntaxKindComparer comparer = new SyntaxKindComparer(DeclarationOrder, AccessLevelOrder);

            var orderedMembers = node.Members.OrderBy(x => x, comparer);

            node = node.WithMembers(Syntax.List<MemberDeclarationSyntax>(orderedMembers));

            return base.VisitInterfaceDeclaration(node);
        }

        public override SyntaxNode VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            // TODO: Need to handle whitespace better here, but holding out and hoping that comes
            // with a future Roslyn. I know whitespace handling is going to change in future 
            // anyway...
            SyntaxKindComparer comparer = new SyntaxKindComparer(DeclarationOrder, AccessLevelOrder);

            var orderedMembers = node.Members.OrderBy(x => x, comparer);

            node = node.WithMembers(Syntax.List<MemberDeclarationSyntax>(orderedMembers));

            return base.VisitNamespaceDeclaration(node);
        }

        private class SyntaxKindComparer : IComparer<MemberDeclarationSyntax>
        {
            private SyntaxKind[] declarationOrder;
            private SyntaxKind[][] accessLevelOrder;

            public SyntaxKindComparer(SyntaxKind[] declarationOrder, SyntaxKind[][] accessLevelOrder)
            {
                this.declarationOrder = declarationOrder;
                this.accessLevelOrder = accessLevelOrder;
            }

            public int Compare(MemberDeclarationSyntax x, MemberDeclarationSyntax y)
            {
                int declX = Array.IndexOf(this.declarationOrder, x.Kind);
                int declY = Array.IndexOf(this.declarationOrder, y.Kind);

                if (declX == declY)
                {
                    declX = IndexOf(this.accessLevelOrder, x.GetModifiers());
                    declY = IndexOf(this.accessLevelOrder, y.GetModifiers());
                }

                return declX - declY;
            }

            private int IndexOf(SyntaxKind[][] array, SyntaxTokenList syntaxTokenList)
            {
                for (int i = 0; i < array.Length; ++i)
                {
                    if (array[i].SequenceEqual(syntaxTokenList.Select(x => x.Kind).OrderBy(x => x)))
                    {
                        return i;
                    }
                }

                return -1;
            }
        }
    }
}
