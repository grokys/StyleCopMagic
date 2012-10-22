using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleCopMagic.OrderingRules;

namespace StyleCopMagic.UnitTests.SpacingRules
{
    [TestClass]
    public class TestSA1203 : TestBase
    {
        public TestSA1203()
            : base(typeof(SA1203))
        {
        }

        [TestMethod]
        public void ClassMembers()
        {
            Run("ClassMembers");
        }
    }
}
