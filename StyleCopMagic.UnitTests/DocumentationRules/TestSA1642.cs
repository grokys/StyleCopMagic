using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleCopMagic.DocumentationRules;

namespace StyleCopMagic.UnitTests.DocumentationRules
{
    [TestClass]
    public class TestSA1642 : TestBase
    {
        public TestSA1642()
            : base(typeof(SA1642))
        {
        }

        [TestMethod]
        public void DefaultConstructor()
        {
            Run("DefaultConstructor");
        }

        [TestMethod]
        public void DocumentationCommentAlreadyExists()
        {
            Run("DocumentationCommentAlreadyExists");
        }

        [TestMethod]
        public void CommentAlreadyExists()
        {
            Run("CommentAlreadyExists");
        }

        [TestMethod]
        public void StructConstructor()
        {
            Run("StructConstructor");
        }
    }
}
