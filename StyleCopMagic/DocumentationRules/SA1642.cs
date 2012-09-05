// -----------------------------------------------------------------------
// <copyright file="ReadabilityRules.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic
{
    using System.Linq;
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
                "Initializes a new instance of the <see cref=\"{0}\"/> class.",
                node.Identifier);

            var documentationTrivia = Syntax.Trivia(
               Syntax.DocumentationComment(
                    Syntax.XmlElement(
                        Syntax.XmlElementStartTag(Syntax.XmlName("summary")),
                        Syntax.List<XmlNodeSyntax>(
                            Syntax.XmlText(
                                Syntax.TokenList())),
                        Syntax.XmlElementEndTag(Syntax.XmlName("summary"))))
                .WithLeadingTrivia(
                    Syntax.ElasticMarker,
                    Syntax.DocumentationCommentExteriorTrivia("/// ")));

            var result = node.WithLeadingTrivia(
                new[] 
                { 
                    documentationTrivia, 
                    Syntax.ElasticCarriageReturnLineFeed,
                }.Concat(node.GetLeadingTrivia()));

            return result;
        }
    }
}
