// -----------------------------------------------------------------------
// <copyright file="SA1633.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic
{
    using System.IO;
    using System.Linq;
    using Roslyn.Compilers.CSharp;
    using Roslyn.Services;

    public class SA1633 : SyntaxRewriter, IFixer
    {
        private SyntaxTree src;
        private ISettings settings;

        public SA1633(SyntaxTree src, ISettings settings)
        {
            this.src = src;
            this.settings = settings;
        }

        public SyntaxTree Repair()
        {
            SyntaxNode result = Visit(src.GetRoot());
            return SyntaxTree.Create(src.FilePath, (CompilationUnitSyntax)result.Format().GetFormattedRoot());
        }

        public override SyntaxNode Visit(SyntaxNode node)
        {
            if (node == src.GetRoot().ChildNodes().First())
            {
                string comment = string.Format(
                    "//-----------------------------------------------------------------------\n" +
                    "// <copyright file=\"{0}\" company=\"{1}\">\n" +
                    "// {2}\n" +
                    "// </copyright>\n" +
                    "//-----------------------------------------------------------------------\n" +
                    "\n",
                    Path.GetFileName(this.src.FilePath),
                    this.settings.CompanyName,
                    this.settings.Copyright);

                // TODO: figure out how to do this properly.
                comment = comment.Replace("\n", "\r\n");

                return node.WithLeadingTrivia(Syntax.ParseLeadingTrivia(comment));
            }
            else
            {
                return base.Visit(node);
            }
        }
    }
}
