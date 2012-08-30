// -----------------------------------------------------------------------
// <copyright file="ReadabilityRules.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Roslyn.Compilers.CSharp;

    public class SA1101 : SyntaxRewriter
    {
        private SyntaxTree src;
        private Compilation compilation;
        private SemanticModel semanticModel;

        public SA1101(SyntaxTree src)
        {
            this.src = src;
            this.compilation = Compilation.Create(
                "src", 
                syntaxTrees: new[] { this.src });
            this.semanticModel = this.compilation.GetSemanticModel(src);
        }

        /// <summary>
        /// Repairs SA1101 (PrefixLocalCallsWithThis) warnings.
        /// </summary>
        public SyntaxTree Repair()
        {
            SyntaxNode result = Visit(src.GetRoot());
            return SyntaxTree.Create(src.FilePath, (CompilationUnitSyntax)result);
        }

        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            SymbolInfo symbolInfo = this.semanticModel.GetSymbolInfo(node);
            
            if (symbolInfo.Symbol != null)
            {
                FieldSymbol field = symbolInfo.Symbol.OriginalDefinition as FieldSymbol;
                
                if (field != null && !field.IsStatic)
                {
                    MemberAccessExpressionSyntax parent = node.Parent as MemberAccessExpressionSyntax;
                    
                    // If the parent expression isn't a member access expression then we need to
                    // add a 'this.'.
                    bool rewrite = parent == null;

                    // If the parent expression is a member access expression, but the identifier is
                    // on the left, then we need to add a 'this.'.
                    if (!rewrite)
                    {
                        rewrite = parent.ChildNodes().First() == node;
                    }

                    if (rewrite)
                    {
                        return Syntax.MemberAccessExpression(
                            SyntaxKind.MemberAccessExpression,
                            Syntax.ThisExpression(),
                            Syntax.IdentifierName(node.Identifier.ValueText))
                                .WithLeadingTrivia(node.GetLeadingTrivia())
                                .WithTrailingTrivia(node.GetTrailingTrivia());
                    }
                }
            }

            return base.VisitIdentifierName(node);
        }
    }
}
