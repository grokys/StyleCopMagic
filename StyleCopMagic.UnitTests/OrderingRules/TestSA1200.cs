using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleCopMagic.OrderingRules;

namespace StyleCopMagic.UnitTests.SpacingRules
{
    [TestClass]
    public class TestSA1200 : TestBase
    {
        public TestSA1200()
            : base(typeof(SA1200))
        {
        }

        [TestMethod]
        public void UsingsOutsideNamespace()
        {
            Run("UsingsOutsideNamespace");
        }

        [TestMethod]
        public void WithClassComment()
        {
            Run("WithClassComment");
        }

        [TestMethod]
        public void WithFileComment()
        {
            Run("WithFileComment");
        }

        [TestMethod]
        public void RegionAroundUsings()
        {
            Run("RegionAroundUsings");
        }
    }
}
