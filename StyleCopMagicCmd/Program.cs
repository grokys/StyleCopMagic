namespace StyleCopMagicCmd
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Roslyn.Compilers;
    using Roslyn.Compilers.CSharp;
    using Roslyn.Services;
    using StyleCopMagic;

    class Program
    {
        static int Main(string[] args)
        {
            IWorkspace workspace = null;
            ISettings settings;

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: StyleCopMagicCmd ProjectFile");
                return 1;
            }
            if (Path.GetExtension(args[0]) == ".sln")
            {
                workspace = Workspace.LoadSolution(args[0]);
            }
            else if (Path.GetExtension(args[0]) == ".csproj")
            {
                workspace = Workspace.LoadStandAloneProject(args[0]);
            }
            else
            {
                Console.WriteLine("Unexpected project file extension. Expected .sln or .csproj.");
                return 1;
            }

            string settingsFilePath = Path.Combine(Path.GetDirectoryName(args[0]), "Settings.StyleCop");
            settings = (File.Exists(settingsFilePath)) ? new SettingsFile(settingsFilePath) : new SettingsFile();

            if (args.Length > 1)
            {
                includeFiles = new List<string>(args.Skip(1));
            }

            Process(workspace, settings);

            return 0;
        }

        private static void Process(IWorkspace workspace, ISettings settings)
        {
            ISolution solution = workspace.CurrentSolution;
            ISolution newSolution = solution;
            IEnumerable<IProject> csharpProjects = solution.Projects.Where(x => x.LanguageServices.Language == LanguageNames.CSharp);

            foreach (IProject project in csharpProjects)
            {
                Compilation compilation = (Compilation)project.GetCompilation();

                foreach (DocumentId documentId in project.DocumentIds)
                {
                    IDocument document = newSolution.GetDocument(documentId);

                    if (includeFiles == null || includeFiles.Contains(document.Name))
                    {
                        if (document.LanguageServices.Language == LanguageNames.CSharp)
                        {
                            Console.WriteLine(document.Name);

                            foreach (Type type in fixers)
                            {
                                try
                                {
                                    SyntaxTree tree = (SyntaxTree)document.GetSyntaxTree();
                                    IFixer fixer = FixerFactory.Create(type, tree, compilation, settings);
                                    SyntaxTree newTree = fixer.Repair();
                                    document = document.UpdateSyntaxRoot(newTree.GetRoot());
                                    newSolution = document.Project.Solution;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                        }
                    }
                }
            }

            workspace.ApplyChanges(solution, newSolution);
        }

        static List<string> includeFiles;
        static List<Type> fixers = new List<Type>(FixerFactory.EnumerateFixers());
    }
}
