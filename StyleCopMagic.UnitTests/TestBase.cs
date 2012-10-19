// -----------------------------------------------------------------------
// <copyright file="TestBase.cs" company="">
// BSD Licence
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.UnitTests
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Roslyn.Compilers.CSharp;
    using Roslyn.Services;
    using Roslyn.Services.Formatting;

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
            ISettings settings = new MockSettings();
            Compilation compilation = Compilation.Create("test", syntaxTrees: new[] { src });
            var target = RuleRewriterFactory.Create(this.rule, settings, () => compilation.GetSemanticModel(src));
            var result = target.Visit(src.GetRoot());
            var formattedResult = SyntaxTree.Create((CompilationUnitSyntax)result.Format(FormattingOptions.GetDefaultOptions()).GetFormattedRoot(), src.FilePath);
            Compare(formattedResult, this.rule.Name, test);
        }

        private SyntaxTree Load(string rule, string test)
        {
            string path = Path.Combine(TestFilePath, rule, test + ".src.cs");
            return SyntaxTree.ParseFile(path);
        }

        private void Compare(SyntaxTree src, string rule, string test)
        {
            string expectedPath = Path.Combine(TestFilePath, rule, test + ".expected.cs");
            string actualPath = Path.Combine(TestFilePath, rule, test + ".actual.cs");
            string expected = File.ReadAllText(expectedPath);
            string actual = src.GetText().ToString();

            File.WriteAllText(actualPath, actual);

            Assert.AreEqual(expected, actual);
        }

        class MockSettings : ISettings
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
