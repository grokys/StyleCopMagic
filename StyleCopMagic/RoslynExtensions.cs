// -----------------------------------------------------------------------
// <copyright file="RoslynExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic
{
    using System.Reflection;
    using Roslyn.Compilers.CSharp;

    public static class RoslynExtensions
    {
        public static SyntaxTokenList GetModifiers(this SyntaxNode node)
        {
            PropertyInfo modifiers = node.GetType().GetProperty("Modifiers");

            if (modifiers != null)
            {
                return (SyntaxTokenList)modifiers.GetValue(node, null);
            }
            else
            {
                return new SyntaxTokenList();
            }
        }

        public static SyntaxNode AddModifiers(this SyntaxNode node, params SyntaxToken[] syntaxTokens)
        {
            MethodInfo addModifiers = node.GetType().GetMethod("AddModifiers");
            return (SyntaxNode)addModifiers.Invoke(node, new[] { syntaxTokens });
        }
    }
}
