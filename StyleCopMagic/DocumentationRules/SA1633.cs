// -----------------------------------------------------------------------
// <copyright file="SA1633.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic
{
    using System.IO;
    using Roslyn.Compilers.CSharp;

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

            // TODO: Work out how to make Roslyn match this to the file EOL format.
            comment = comment.Replace("\n", "\r\n");

            return SyntaxTree.Create(
                this.src.FilePath,
                (CompilationUnitSyntax)this.src.GetRoot().WithLeadingTrivia(Syntax.ParseLeadingTrivia(comment)));
        }
    }
}
