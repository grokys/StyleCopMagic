// -----------------------------------------------------------------------
// <copyright file="SA1633.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.DocumentationRules
{
    using System.IO;
    using Roslyn.Compilers.CSharp;

    public class SA1633 : RuleRewriter
    {
        private ISettings settings;

        public SA1633(ISettings settings)
        {
            this.settings = settings;
        }

        public override SyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
        {
            string comment = string.Format(
                "//-----------------------------------------------------------------------\n" +
                "// <copyright file=\"{0}\" company=\"{1}\">\n" +
                "// {2}\n" +
                "// </copyright>\n" +
                "//-----------------------------------------------------------------------\n" +
                "\n",
                Path.GetFileName(node.SyntaxTree.FilePath),
                this.settings.CompanyName,
                this.settings.Copyright);

            // TODO: figure out how to do this properly.
            comment = comment.Replace("\n", "\r\n");

            return node.WithLeadingTrivia(Syntax.ParseLeadingTrivia(comment));
        }
    }
}
