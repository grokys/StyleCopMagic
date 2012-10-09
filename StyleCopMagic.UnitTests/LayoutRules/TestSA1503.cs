using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleCopMagic.LayoutRules;

namespace StyleCopMagic.UnitTests.DocumentationRules
{
    [TestClass]
    public class TestSA1503 : TestBase
    {
        public TestSA1503()
            : base(typeof(SA1503))
        {
        }

        [TestMethod]
        public void IfStatementMissingBraces()
        {
            Run("IfStatementMissingBraces");
        }

        [TestMethod]
        public void IfStatementSameLine()
        {
            Run("IfStatementSameLine");
        }

        [TestMethod]
        public void WhileStatementMissingBraces()
        {
            Run("WhileStatementMissingBraces");
        }

        [TestMethod]
        public void DoStatementMissingBraces()
        {
            Run("DoStatementMissingBraces");
        }

        [TestMethod]
        public void ForStatementMissingBraces()
        {
            Run("ForStatementMissingBraces");
        }

        [TestMethod]
        public void ForEachStatementMissingBraces()
        {
            Run("ForEachStatementMissingBraces");
        }

        [TestMethod]
        public void FixedStatementMissingBraces()
        {
            Run("FixedStatementMissingBraces");
        }

        [TestMethod]
        public void UsingStatementMissingBraces()
        {
            Run("UsingStatementMissingBraces");
        }

        [TestMethod]
        public void LockStatementMissingBraces()
        {
            Run("LockStatementMissingBraces");
        }

        [TestMethod]
        public void ElseStatementMissingBraces()
        {
            Run("ElseStatementMissingBraces");
        }
    }
}
