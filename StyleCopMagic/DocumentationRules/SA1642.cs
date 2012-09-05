// -----------------------------------------------------------------------
// <copyright file="ReadabilityRules.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic
{
    using Roslyn.Compilers.CSharp;
    using Roslyn.Services;

    public class SA1642 : SyntaxRewriter, IFixer
    {
        private SyntaxTree src;
        private Compilation compilation;
        private SemanticModel semanticModel;

        public SA1642(SyntaxTree src)
        {
            this.src = src;
            this.compilation = Compilation.Create(
                "src",
                syntaxTrees: new[] { this.src });
            this.semanticModel = this.compilation.GetSemanticModel(src);
        }

        /// <summary>
        /// Repairs SA1642 (ConstructorSummaryDocumentationMustBeginWithStandardText) warnings <see cref=""/>.
        /// </summary>
        public SyntaxTree Repair()
        {
            SyntaxNode result = Visit(src.GetRoot());
            return SyntaxTree.Create(src.FilePath, (CompilationUnitSyntax)result.Format().GetFormattedRoot());
        }

        public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            string summary = string.Format(
                "/// <summary>\n" +
                "/// Initializes a new instance of the <see cref=\"{0}\"/> class.\n" +
                "/// </summary>\n",
                node.Identifier);
            return node.WithLeadingTrivia(Syntax.ParseLeadingTrivia(summary));
        }
    }
}
