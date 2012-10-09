// -----------------------------------------------------------------------
// <copyright file="SA1005.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.SpacingRules
{
    using Roslyn.Compilers.CSharp;

    public class SA1005 : SyntaxRewriter, IFixer
    {
        private SyntaxTree src;

        public SA1005(SyntaxTree src, Compilation compilation, ISettings settings)
        {
            this.src = src;
        }

        public SyntaxTree Repair()
        {
            SyntaxNode result = Visit(src.GetRoot());
            return SyntaxTree.Create(src.FilePath, (CompilationUnitSyntax)result);
        }

        public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
        {
            if (NeedsSpaceAdding(trivia))
            {
                var text = trivia.GetText();
                text = text.Substring(0, 2) + " " + text.Substring(2).TrimStart();
                var triviaList = Syntax.ParseLeadingTrivia(text);

                if (triviaList.Count == 1)
                {
                    return triviaList[0];
                }
            }

            return trivia;
        }

        private bool NeedsSpaceAdding(SyntaxTrivia trivia)
        {
            if (trivia.Kind != SyntaxKind.SingleLineCommentTrivia)
            {
                return false;
            }

            var text = trivia.GetText();

            if (!text.StartsWith("//"))
            {
                return false;
            }

            if (text.StartsWith("///") || text.StartsWith("////"))
            {
                return false;
            }

            if (text.Length >= 3 && text[2] != ' ')
            {
                return true;
            }

            if (text.Length >= 4 && text[2] == ' ' && text[3] == ' ')
            {
                return true;
            }

            return false;
        }
    }
}

