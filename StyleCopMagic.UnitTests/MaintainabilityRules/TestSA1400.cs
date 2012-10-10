using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleCopMagic.MaintainabilityRules;

namespace StyleCopMagic.UnitTests.MaintainabilityRules
{
    [TestClass]
    public class TestSA1400 : TestBase
    {
        public TestSA1400()
            : base(typeof(SA1400))
        {
        }

        [TestMethod]
        public void ClassWithoutInternal()
        {
            Run("ClassWithoutInternal");
        }

        [TestMethod]
        public void EnumWithoutInternal()
        {
            Run("EnumWithoutInternal");
        }

        [TestMethod]
        public void FieldWithoutPrivate()
        {
            Run("FieldWithoutPrivate");
        }

        [TestMethod]
        public void MethodWithoutPrivate()
        {
            Run("MethodWithoutPrivate");
        }

        [TestMethod]
        public void PropertyWithoutPrivate()
        {
            Run("PropertyWithoutPrivate");
        }

        [TestMethod]
        public void StructWithoutInternal()
        {
            Run("StructWithoutInternal");
        }

        [TestMethod]
        public void InterfaceWithoutPublic()
        {
            Run("InterfaceWithoutPublic");
        }

        [TestMethod]
        public void NestedClassWithoutPrivate()
        {
            Run("NestedClassWithoutPrivate");
        }

        [TestMethod]
        public void NestedEnumWithoutPrivate()
        {
            Run("NestedEnumWithoutPrivate");
        }

        [TestMethod]
        public void NestedStructWithoutPrivate()
        {
            Run("NestedStructWithoutPrivate");
        }

        [TestMethod]
        public void MethodInInterface()
        {
            Run("MethodInInterface");
        }

        [TestMethod]
        public void PropertyInInterface()
        {
            Run("PropertyInInterface");
        }

        [TestMethod]
        public void ExplicitInterfaceMethod()
        {
            Run("ExplicitInterfaceMethod");
        }

        [TestMethod]
        public void ExplicitInterfaceProperty()
        {
            Run("ExplicitInterfaceProperty");
        }

        [TestMethod]
        public void MethodWithComments()
        {
            Run("MethodWithComments");
        }
    }
}
