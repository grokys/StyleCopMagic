// -----------------------------------------------------------------------
// <copyright file="SettingsFile.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StyleCopMagic
{
    using System.Xml.Linq;
    using System.Xml.XPath;

    /// <summary>
    /// Reads a Settings.StyleCop file.
    /// </summary>
    public class SettingsFile : ISettings
    {
        public SettingsFile()
        {
            this.CompanyName = this.Copyright = string.Empty;
        }

        public SettingsFile(string fileName)
        {
            XDocument document = XDocument.Load(fileName);
            XElement documentationRulesSettings = document.XPathSelectElement(
                "/StyleCopSettings/Analyzers/Analyzer[@AnalyzerId='StyleCop.CSharp.DocumentationRules']/AnalyzerSettings");

            if (documentationRulesSettings != null)
            {
                XElement companyName = documentationRulesSettings.XPathSelectElement("StringProperty[@Name='CompanyName']");
                XElement copyright = documentationRulesSettings.XPathSelectElement("StringProperty[@Name='Copyright']");

                if (companyName != null)
                {
                    this.CompanyName = companyName.Value;
                }

                if (copyright != null)
                {
                    this.Copyright = copyright.Value;
                }
            }
        }

        public string CompanyName { get; private set; }
        public string Copyright { get; private set; }
    }
}
