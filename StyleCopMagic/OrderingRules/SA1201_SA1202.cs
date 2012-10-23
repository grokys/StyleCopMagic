// -----------------------------------------------------------------------
// <copyright file="SA1201.cs" company="">
// BSD Licence
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

        public override SyntaxNode Visit(SyntaxNode node)
        {
            // TODO: Why does a null sometimes get passed here?
            if (node != null)
            {
                switch (node.Kind)
                {
                    case SyntaxKind.ClassDeclaration:
                    case SyntaxKind.StructDeclaration:
                    case SyntaxKind.InterfaceDeclaration:
                    case SyntaxKind.NamespaceDeclaration:
                        MemberComparer comparer = new MemberComparer(DeclarationOrder, AccessLevelOrder);
                        var orderedMembers = node.GetMembers().OrderBy(x => x, comparer);
                        node = node.WithMembers(Syntax.List<MemberDeclarationSyntax>(orderedMembers));
                        break;
                }
            }

            return base.Visit(node);
        }

        private class MemberComparer : IComparer<MemberDeclarationSyntax>
        {
            private SyntaxKind[] declarationOrder;
            private SyntaxKind[][] accessLevelOrder;

            public MemberComparer(SyntaxKind[] declarationOrder, SyntaxKind[][] accessLevelOrder)
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
                    if (array[i].Intersect(syntaxTokenList.Select(x => x.Kind).OrderBy(x => x)).Count() == array[i].Length)
                    {
                        return i;
                    }
                }

                return -1;
            }
        }
    }
}
