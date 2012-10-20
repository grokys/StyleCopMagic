using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleCopMagic.DocumentationRules;

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

        [TestMethod]
        public void ExistingFileComment()
        {
            Run("ExistingFileComment");
        }

        [TestMethod]
        public void FileWithInitialRegion()
        {
            Run("FileWithInitialRegion");
        }
    }
}
