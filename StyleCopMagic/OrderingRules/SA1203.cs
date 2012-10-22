// -----------------------------------------------------------------------
// <copyright file="SA1203.cs" company="">
// BSD Licence
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.OrderingRules
{
    using System.Linq;
    using Roslyn.Compilers.CSharp;
    using System.Collections.Generic;

    public class SA1203 : RuleRewriter
    {
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
                        MemberComparer comparer = new MemberComparer();
                        var orderedMembers = node.GetMembers().OrderBy(x => x, comparer);
                        node = node.WithMembers(Syntax.List<MemberDeclarationSyntax>(orderedMembers));
                        break;
                }
            }

            return base.Visit(node);
        }

        private class MemberComparer : IComparer<MemberDeclarationSyntax>
        {
            public int Compare(MemberDeclarationSyntax x, MemberDeclarationSyntax y)
            {
                int xConst = x.GetModifiers().Count(i => i.Kind == SyntaxKind.ConstKeyword);
                int yConst = y.GetModifiers().Count(i => i.Kind == SyntaxKind.ConstKeyword);
                return yConst - xConst;
            }
        }
    }
}
