// -----------------------------------------------------------------------
// <copyright file="SA1005.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.SpacingRules
{
    using Roslyn.Compilers.CSharp;

    public class SA1005 : RuleRewriter
    {
        public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
        {
            if (NeedsSpaceAdding(trivia))
            {
                var text = trivia.ToFullString();
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

            var text = trivia.ToFullString();

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

