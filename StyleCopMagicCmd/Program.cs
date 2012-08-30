using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Roslyn.Compilers.CSharp;
using StyleCopMagic;

namespace StyleCopMagicCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                string path = Path.GetDirectoryName(arg);
                string searchPattern = Path.GetFileName(arg);
                bool recursive = false;

                if (string.IsNullOrWhiteSpace(path))
                {
                    path = Directory.GetCurrentDirectory();
                }

                if (Directory.Exists(Path.Combine(path, searchPattern)))
                {
                    path = Path.Combine(path, searchPattern);
                    searchPattern = "*.cs";
                    recursive = true;
                }

                ProcessDirectory(path, searchPattern, recursive);
            }
        }

        static void ProcessDirectory(string path, string searchPattern, bool recursive)
        {
            foreach (string fileName in Directory.EnumerateFiles(path, searchPattern))
            {
                Console.WriteLine(fileName);
                ProcessFile(fileName);
            }

            if (recursive)
            {
                foreach (string childPath in Directory.EnumerateDirectories(path))
                {
                    string directoryName = Path.GetFileName(childPath);

                    if (directoryName != "obj" && directoryName != "bin")
                    {
                        ProcessDirectory(childPath, searchPattern, true);
                    }
                }
            }
        }

        private static void ProcessFile(string fileName)
        {
            try
            {
                SyntaxTree src = SyntaxTree.ParseCompilationUnit(File.ReadAllText(fileName));
                SA1101 s = new SA1101(src);
                src = s.Repair();
                File.WriteAllText(fileName, src.GetText().GetText());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
