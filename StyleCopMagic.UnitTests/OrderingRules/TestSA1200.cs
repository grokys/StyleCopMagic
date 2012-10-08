using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleCopMagic.OrderingRules;

namespace StyleCopMagic.UnitTests.OrderingRules
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
    }
}
