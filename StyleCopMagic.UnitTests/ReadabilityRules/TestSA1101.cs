using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleCopMagic.ReadabilityRules;

namespace StyleCopMagic.UnitTests.ReadabilityRules
{
    [TestClass]
    public class TestSA1101 : TestBase
    {
        public TestSA1101()
            : base(typeof(SA1101))
        {
        }

        [TestMethod]
        public void FieldWrite()
        {
            Run("FieldWrite");
        }

        [TestMethod]
        public void FieldRead()
        {
            Run("FieldRead");
        }

        [TestMethod]
        public void FieldMemberAccess()
        {
            Run("FieldMemberAccess");
        }

        [TestMethod]
        public void FieldStaticRead()
        {
            Run("FieldStaticRead");
        }

        [TestMethod]
        public void PropertyWrite()
        {
            Run("PropertyWrite");
        }

        [TestMethod]
        public void PropertyRead()
        {
            Run("PropertyRead");
        }

        [TestMethod]
        public void MethodCall()
        {
            Run("MethodCall");
        }

        [TestMethod]
        public void EventRead()
        {
            Run("EventRead");
        }

        [TestMethod]
        public void EventCall()
        {
            Run("EventCall");
        }

        [TestMethod]
        public void AmbiguousOverloadedMethodCall()
        {
            Run("AmbiguousOverloadedMethodCall");
        }

        [TestMethod]
        public void UsingCrash()
        {
            Run("UsingCrash");
        }

        [TestMethod]
        public void ThrowException()
        {
            Run("ThrowException");
        }

        [TestMethod]
        public void CreateSelf()
        {
            Run("CreateSelf");
        }

        [TestMethod]
        public void InnerStructFieldWrite()
        {
            Run("InnerStructFieldWrite");
        }

        [TestMethod]
        public void ObjectInitializer()
        {
            Run("ObjectInitializer");
        }

        [TestMethod]
        public void AnonymousObjectInitializer()
        {
            Run("AnonymousObjectInitializer");
        }
    }
}
