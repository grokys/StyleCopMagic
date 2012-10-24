using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleCopMagic.OrderingRules;

namespace StyleCopMagic.UnitTests.OrderingRules
{
    [TestClass]
    public class TestSA1201_SA1202 : TestBase
    {
        public TestSA1201_SA1202()
            : base(typeof(SA1201_SA1202))
        {
        }

        [TestMethod]
        public void SA1201_ClassMembers()
        {
            Run("SA1201_ClassMembers");
        }

        [TestMethod]
        public void SA1201_StructMembers()
        {
            Run("SA1201_StructMembers");
        }

        [TestMethod]
        public void SA1201_InterfaceMembers()
        {
            Run("SA1201_InterfaceMembers");
        }

        [TestMethod]
        public void SA1201_NamespaceMembers()
        {
            Run("SA1201_NamespaceMembers");
        }

        [TestMethod]
        public void SA1201_StaticMember()
        {
            Run("SA1201_StaticMember");
        }

        [TestMethod]
        public void SA1201_OperatorEquals()
        {
            Run("SA1201_OperatorEquals");
        }

        [TestMethod]
        public void SA1201_ConversionOperator()
        {
            Run("SA1201_ConversionOperator");
        }

        [TestMethod]
        public void SA1202_ClassMethods()
        {
            Run("SA1202_ClassMethods");
        }

        [TestMethod]
        public void SA1201_SA1202_ClassMembersInsideIf()
        {
            Run("SA1201_SA1202_ClassMembersInsideIf");
        }
    }
}
