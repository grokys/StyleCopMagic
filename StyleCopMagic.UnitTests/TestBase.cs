// -----------------------------------------------------------------------
// <copyright file="TestBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.UnitTests
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Roslyn.Compilers.CSharp;

    public class TestBase
    {
        public const string TestFilePath = @"..\..\..\TestFiles";

        private Type rule;

        public TestBase(Type rule)
        {
            this.rule = rule;
        }

        protected void Run(string test)
        {
            var src = Load(this.rule.Name, test);
            var target = Create(src);
            var result = target.Repair();
            Compare(result, this.rule.Name, test);
        }

        private IFixer Create(SyntaxTree src)
        {
            Type[] ctorArgs = new[] { typeof(SyntaxTree), typeof(ISettings) };
            ConstructorInfo constructor = this.rule.GetConstructor(ctorArgs);
            return (IFixer)constructor.Invoke(new object[] { src, new Settings() });
        }

        private SyntaxTree Load(string rule, string test)
        {
            string path = Path.Combine(TestFilePath, rule, test + ".src.cs");
            string file = File.ReadAllText(path);
            return SyntaxTree.ParseCompilationUnit(file, path);
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

        class Settings : ISettings
        {
            public string CompanyName
            {
                get { return "Foo"; }
            }

            public string Copyright
            {
                get { return "Copyright Bar Inc."; }
            }
        }
    }
}
