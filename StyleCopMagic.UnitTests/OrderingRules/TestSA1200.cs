using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleCopMagic.SpacingRules;

namespace StyleCopMagic.UnitTests.SpacingRules
{
    [TestClass]
    public class TestSA1005 : TestBase
    {
        public TestSA1005()
            : base(typeof(SA1005))
        {
        }

        [TestMethod]
        public void SingleLineComment()
        {
            Run("SingleLineComment");
        }
    }
}
