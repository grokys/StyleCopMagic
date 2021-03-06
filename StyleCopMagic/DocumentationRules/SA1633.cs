﻿// -----------------------------------------------------------------------
// <copyright file="SA1633.cs" company="">
// BSD Licence
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.DocumentationRules
{
    using System.IO;
    using System.Linq;
    using Roslyn.Compilers.CSharp;
    using System;

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
                this.settings.Copyright.Replace("\n", "\n// "));

            // TODO: figure out how to do this properly.
            comment = comment.Replace("\n", Environment.NewLine);

            SyntaxTriviaList commentTrivia = Syntax.ParseLeadingTrivia(comment);
            var preservedTrivia = node.GetLeadingTrivia().SkipWhile(x => 
                x.Kind == SyntaxKind.SingleLineCommentTrivia ||
                x.Kind == SyntaxKind.EndOfLineTrivia);

            return node.WithLeadingTrivia(commentTrivia.Concat(preservedTrivia));
        }
    }
}
