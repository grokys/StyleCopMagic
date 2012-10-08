using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StyleCopMagic.UnitTests.DocumentationRules
{
    [TestClass]
    public class TestSA1633 : TestBase
    {
        public TestSA1633()
            : base(typeof(SA1633))
        {
        }

        [TestMethod]
        public void NoFileComment()
        {
            Run("NoFileComment");
        }
    }
}
