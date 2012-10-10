// -----------------------------------------------------------------------
// <copyright file="SA1503.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.LayoutRules
{
    using Roslyn.Compilers.CSharp;
    using Roslyn.Services;

    public class SA1503 : RuleRewriter
    {
        private SemanticModel semanticModel;

        public SA1503(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public override SyntaxNode Visit(SyntaxNode node)
        {
            return base.Visit(node);
        }

        private StatementSyntax WrapStatementWithBlock(StatementSyntax statement)
        {
            return Syntax.Block(
                statements: Syntax.List(statement));
        }

        public override SyntaxNode VisitIfStatement(IfStatementSyntax node)
        {
            node = (IfStatementSyntax)base.VisitIfStatement(node);

            if (!node.CloseParenToken.IsMissing && node.Statement.Kind != SyntaxKind.Block)
            {
                return CodeAnnotations.Formatting.AddAnnotationTo(
                    Syntax.IfStatement(
                        node.IfKeyword,
                        node.OpenParenToken,
                        node.Condition,
                        node.CloseParenToken,
                        WrapStatementWithBlock(node.Statement),
                        node.Else));
            }
            else
            {
                return node;
            }
        }

        public override SyntaxNode VisitWhileStatement(WhileStatementSyntax node)
        {
            node = (WhileStatementSyntax)base.VisitWhileStatement(node);

            if (!node.CloseParenToken.IsMissing && node.Statement.Kind != SyntaxKind.Block)
            {
                return CodeAnnotations.Formatting.AddAnnotationTo(
                    Syntax.WhileStatement(
                        node.WhileKeyword,
                        node.OpenParenToken,
                        node.Condition,
                        node.CloseParenToken,
                        WrapStatementWithBlock(node.Statement)));
            }
            else
            {
                return node;
            }
        }

        public override SyntaxNode VisitDoStatement(DoStatementSyntax node)
        {
            node = (DoStatementSyntax)base.VisitDoStatement(node);

            if (!node.CloseParenToken.IsMissing && node.Statement.Kind != SyntaxKind.Block)
            {
                return CodeAnnotations.Formatting.AddAnnotationTo(
                    Syntax.DoStatement(
                        node.DoKeyword,
                        WrapStatementWithBlock(node.Statement),
                        node.WhileKeyword,
                        node.OpenParenToken,
                        node.Condition,
                        node.CloseParenToken,
                        node.SemicolonToken));
            }
            else
            {
                return node;
            }
        }

        public override SyntaxNode VisitForStatement(ForStatementSyntax node)
        {
            node = (ForStatementSyntax)base.VisitForStatement(node);

            if (!node.CloseParenToken.IsMissing && node.Statement.Kind != SyntaxKind.Block)
            {
                return CodeAnnotations.Formatting.AddAnnotationTo(
                    Syntax.ForStatement(
                        node.ForKeyword,
                        node.OpenParenToken,
                        node.Declaration,
                        node.Initializers,
                        node.FirstSemicolonToken,
                        node.Condition,
                        node.SecondSemicolonToken,
                        node.Incrementors,
                        node.CloseParenToken,
                        WrapStatementWithBlock(node.Statement)));
            }
            else
            {
                return node;
            }
        }

        public override SyntaxNode VisitForEachStatement(ForEachStatementSyntax node)
        {
            node = (ForEachStatementSyntax)base.VisitForEachStatement(node);

            if (!node.CloseParenToken.IsMissing && node.Statement.Kind != SyntaxKind.Block)
            {
                return CodeAnnotations.Formatting.AddAnnotationTo(
                    Syntax.ForEachStatement(
                        node.ForEachKeyword,
                        node.OpenParenToken,
                        node.Type,
                        node.Identifier,
                        node.InKeyword,
                        node.Expression,
                        node.CloseParenToken,
                        WrapStatementWithBlock(node.Statement)));
            }
            else
            {
                return node;
            }
        }

        public override SyntaxNode VisitFixedStatement(FixedStatementSyntax node)
        {
            node = (FixedStatementSyntax)base.VisitFixedStatement(node);

            if (!node.CloseParenToken.IsMissing && node.Statement.Kind != SyntaxKind.Block)
            {
                return CodeAnnotations.Formatting.AddAnnotationTo(
                    Syntax.FixedStatement(
                        node.FixedKeyword,
                        node.OpenParenToken,
                        node.Declaration,
                        node.CloseParenToken,
                        WrapStatementWithBlock(node.Statement)));
            }
            else
            {
                return node;
            }
        }

        public override SyntaxNode VisitUsingStatement(UsingStatementSyntax node)
        {
            node = (UsingStatementSyntax)base.VisitUsingStatement(node);

            if (!node.CloseParenToken.IsMissing && node.Statement.Kind != SyntaxKind.Block)
            {
                return CodeAnnotations.Formatting.AddAnnotationTo(
                    Syntax.UsingStatement(
                        node.UsingKeyword,
                        node.OpenParenToken,
                        node.Declaration,
                        node.Expression,
                        node.CloseParenToken,
                        WrapStatementWithBlock(node.Statement)));
            }
            else
            {
                return node;
            }
        }

        public override SyntaxNode VisitLockStatement(LockStatementSyntax node)
        {
            node = (LockStatementSyntax)base.VisitLockStatement(node);

            if (!node.CloseParenToken.IsMissing && node.Statement.Kind != SyntaxKind.Block)
            {
                return CodeAnnotations.Formatting.AddAnnotationTo(
                    Syntax.LockStatement(
                        node.LockKeyword,
                        node.OpenParenToken,
                        node.Expression,
                        node.CloseParenToken,
                        WrapStatementWithBlock(node.Statement)));
            }
            else
            {
                return node;
            }
        }

        public override SyntaxNode VisitElseClause(ElseClauseSyntax node)
        {
            node = (ElseClauseSyntax)base.VisitElseClause(node);

            if (node.Statement.Kind != SyntaxKind.Block)
            {
                return CodeAnnotations.Formatting.AddAnnotationTo(
                    Syntax.ElseClause(
                        node.ElseKeyword,
                        WrapStatementWithBlock(node.Statement)));
            }
            else
            {
                return node;
            }
        }
    }
}

