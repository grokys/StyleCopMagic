// -----------------------------------------------------------------------
// <copyright file="SettingsFileTest.cs" company="">
// BSD Licence
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic.UnitTests
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SettingsFileTest
    {
        [TestMethod]
        public void CompanyName_Should_Be_Empty_String_With_Default_Ctor()
        {
            SettingsFile target = new SettingsFile();
            Assert.AreEqual(string.Empty, target.CompanyName);
        }

        [TestMethod]
        public void Copyright_Should_Be_Empty_String_With_Default_Ctor()
        {
            SettingsFile target = new SettingsFile();
            Assert.AreEqual(string.Empty, target.Copyright);
        }

        [TestMethod]
        public void Test_Empty_SettingsFile()
        {
            string path = Path.Combine(TestBase.TestFilePath, "SettingsFile", "Empty.StyleCop");
            SettingsFile target = new SettingsFile(path);
        }

        [TestMethod]
        public void Test_SettingsFile_With_CompanyName()
        {
            string path = Path.Combine(TestBase.TestFilePath, "SettingsFile", "CompanyName.StyleCop");
            SettingsFile target = new SettingsFile(path);

            Assert.AreEqual("Foo", target.CompanyName);
            Assert.IsNull(target.Copyright);
        }

        [TestMethod]
        public void Test_SettingsFile_With_Copyright()
        {
            string path = Path.Combine(TestBase.TestFilePath, "SettingsFile", "Copyright.StyleCop");
            SettingsFile target = new SettingsFile(path);

            Assert.IsNull(target.CompanyName);
            Assert.AreEqual("Bar", target.Copyright);
        }

        [TestMethod]
        public void Test_SettingsFile_With_CompanyName_And_Copyright()
        {
            string path = Path.Combine(TestBase.TestFilePath, "SettingsFile", "CompanyNameCopyright.StyleCop");
            SettingsFile target = new SettingsFile(path);

            Assert.AreEqual("Foo", target.CompanyName);
            Assert.AreEqual("Bar", target.Copyright);
        }
    }
}
