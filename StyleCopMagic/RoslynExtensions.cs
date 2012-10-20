// -----------------------------------------------------------------------
// <copyright file="RoslynExtensions.cs" company="">
// BSD Licence
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic
{
    using System.Reflection;
    using Roslyn.Compilers.CSharp;

    // TODO: Is there a better way to do this than using reflection?
    public static class RoslynExtensions
    {
        public static SyntaxList<MemberDeclarationSyntax> GetMembers(this SyntaxNode node)
        {
            PropertyInfo modifiers = node.GetType().GetProperty("Members");
            return (SyntaxList<MemberDeclarationSyntax>)modifiers.GetValue(node, null);
        }

        public static SyntaxNode WithMembers(this SyntaxNode node, SyntaxList<MemberDeclarationSyntax> members)
        {
            MethodInfo method = node.GetType().GetMethod("WithMembers");
            return (SyntaxNode)method.Invoke(node, new object[] { members });
        }

        public static SyntaxTokenList GetModifiers(this SyntaxNode node)
        {
            PropertyInfo property = node.GetType().GetProperty("Modifiers");
            
            if (property != null)
            {
                return (SyntaxTokenList)property.GetValue(node, null);
            }
            else
            {
                return new SyntaxTokenList();
            }
        }

        public static SyntaxNode AddModifiers(this SyntaxNode node, params SyntaxToken[] syntaxTokens)
        {
            MethodInfo member = node.GetType().GetMethod("AddModifiers");
            return (SyntaxNode)member.Invoke(node, new[] { syntaxTokens });
        }

        public static ExplicitInterfaceSpecifierSyntax GetExplicitInterfaceSpecifier(this SyntaxNode node)
        {
            PropertyInfo property = node.GetType().GetProperty("ExplicitInterfaceSpecifier");

            if (property != null)
            {
                return (ExplicitInterfaceSpecifierSyntax)property.GetValue(node, null);
            }
            else
            {
                return null;
            }
        }
    }
}
