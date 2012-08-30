using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roslyn.Compilers.CSharp;
using System.IO;

namespace StyleCopMagic.UnitTests
{
    [TestClass]
    public class TestSA1101
    {
        const string TestFilePath = @"..\..\..\TestFiles";

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

        //[TestMethod]
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

        private void Run(string test)
        {
            var src = Load("SA1101", test);
            var target = new SA1101(src);
            var result = target.Repair();
            Compare(result, "SA1101", test);
        }
        
        private SyntaxTree Load(string rule, string test)
        {
            string path = Path.Combine(TestFilePath, rule, test + ".src.cs");
            string file = File.ReadAllText(path);
            return SyntaxTree.ParseCompilationUnit(file);
        }

        private void Compare(SyntaxTree src, string rule, string test)
        {
            string expectedPath = Path.Combine(TestFilePath, rule, test + ".expected.cs");
            string actualPath = Path.Combine(TestFilePath, rule, test + ".actual.cs");
            string expected = File.ReadAllText(expectedPath);
            string actual = src.GetText().GetText();

            File.WriteAllText(actualPath, actual);

            Assert.AreEqual(expected, actual);
        }
    }
}
