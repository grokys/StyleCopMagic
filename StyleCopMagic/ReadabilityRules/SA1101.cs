// -----------------------------------------------------------------------
// <copyright file="SA1101.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.ReadabilityRules
{
    using System.Collections.Generic;
    using System.Linq;
    using Roslyn.Compilers;
    using Roslyn.Compilers.CSharp;

    public class SA1101 : RuleRewriter
    {
        private SemanticModel semanticModel;

        public SA1101(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (IsMember(node))
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

            return base.VisitIdentifierName(node);
        }

        bool IsMember(IdentifierNameSyntax identifier)
        {
            SymbolInfo identifierSymbol;
            bool result = false;

            // If we're in an object initializer, don't add a 'this.'.
            if (identifier.FirstAncestorOrSelf<InitializerExpressionSyntax>() != null ||
                identifier.FirstAncestorOrSelf<AnonymousObjectCreationExpressionSyntax>() != null)
            {
                return false;
            }

            try
            {
                // GetSymbolInfo sometimes throws a NullReferenceException in the June 2012 
                // Roslyn CTP with a symbol in a using statement can't be resolved. 
                // This occurance is tested by the UsingCrash test.
                identifierSymbol = this.semanticModel.GetSymbolInfo(identifier);
            }
            catch
            {
                return false;
            }

            IEnumerable<Symbol> symbols;

            if (identifierSymbol.Symbol != null)
            {
                symbols = new[] { identifierSymbol.Symbol };
            }
            else if (identifierSymbol.CandidateReason == CandidateReason.OverloadResolutionFailure)
            {
                symbols = identifierSymbol.CandidateSymbols.ToArray();
            }
            else
            {
                return false;
            }

            result = true;

            foreach (Symbol symbol in symbols)
            {
                FieldSymbol field = symbol.OriginalDefinition as FieldSymbol;
                MethodSymbol method = symbol.OriginalDefinition as MethodSymbol;
                PropertySymbol property = symbol.OriginalDefinition as PropertySymbol;
                EventSymbol @event = symbol.OriginalDefinition as EventSymbol;

                result &= ((field != null && !field.IsStatic) ||
                            (method != null && !method.IsStatic && method.MethodKind != MethodKind.Constructor) ||
                            (property != null && !property.IsStatic) ||
                            (@event != null && !@event.IsStatic));
            }

            return result;
        }
    }
}

