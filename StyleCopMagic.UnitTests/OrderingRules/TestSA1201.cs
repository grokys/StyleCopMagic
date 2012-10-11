using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleCopMagic.OrderingRules;

namespace StyleCopMagic.UnitTests.SpacingRules
{
    [TestClass]
    public class TestSA1201 : TestBase
    {
        public TestSA1201()
            : base(typeof(SA1201))
        {
        }

        [TestMethod]
        public void ClassMembers()
        {
            Run("ClassMembers");
        }

        [TestMethod]
        public void StructMembers()
        {
            Run("StructMembers");
        }

        [TestMethod]
        public void InterfaceMembers()
        {
            Run("InterfaceMembers");
        }

        [TestMethod]
        public void NamespaceMembers()
        {
            Run("NamespaceMembers");
        }
    }
}
