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

            if (args.Length > 1)
            {
                includeFiles = new List<string>(args.Skip(1));
            }

            Process(workspace);

            return 0;
        }

        private static void Process(IWorkspace workspace)
        {
            ISolution solution = workspace.CurrentSolution;
            ISolution newSolution = solution;
            IEnumerable<IProject> csharpProjects = solution.Projects.Where(x => x.LanguageServices.Language == LanguageNames.CSharp);

            foreach (IProject project in csharpProjects)
            {
                IProject newProject = project;
                string settingsFilePath = Path.Combine(Path.GetDirectoryName(project.FilePath), "Settings.StyleCop");
                ISettings settings = (File.Exists(settingsFilePath)) ? new SettingsFile(settingsFilePath) : new SettingsFile();

                foreach (DocumentId documentId in project.DocumentIds)
                {
                    IDocument document = newSolution.GetDocument(documentId);

                    if (includeFiles == null || includeFiles.Contains(document.Name))
                    {
                        if (document.LanguageServices.Language == LanguageNames.CSharp)
                        {
                            Console.WriteLine(document.Name);

                            foreach (Type type in rewriters)
                            {
                                SyntaxTree tree = (SyntaxTree)document.GetSyntaxTree();
                                SyntaxNode contents = tree.GetRoot();

                                try
                                {
                                    RuleRewriter fixer = RuleRewriterFactory.Create(type, settings, () =>
                                        (SemanticModel)newProject.GetCompilation().GetSemanticModel(tree));
                                    contents = fixer.Visit(contents);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }

                                document = document.UpdateSyntaxRoot(contents.Format().GetFormattedRoot());
                                newProject = document.Project;
                                newSolution = newProject.Solution;
                            }
                        }
                    }
                }
            }

            workspace.ApplyChanges(solution, newSolution);
        }

        static List<string> includeFiles;
        static List<Type> rewriters = new List<Type>(RuleRewriterFactory.EnumerateRewriters());
    }
}
