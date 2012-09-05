namespace StyleCopMagicCmd
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Roslyn.Compilers;
    using Roslyn.Compilers.CSharp;
    using Roslyn.Services;
    using StyleCopMagic;

    class Program
    {
        static void Main(string[] args)
        {
            IWorkspace workspace = null;

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: StyleCopMagicCmd ProjectFile");
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
            }

            if (workspace != null)
            {
                if (args.Length > 1)
                {
                    includeFiles = new List<string>(args.Skip(1));
                }

                LoadFixers();
                Process(workspace);
            }
        }

        private static void LoadFixers()
        {
            var types = from type in Assembly.GetAssembly(typeof(IFixer)).GetTypes()
                        where !type.IsInterface && typeof(IFixer).IsAssignableFrom(type)
                        select type;
            fixers = new List<Type>(types);
        }

        private static void Process(IWorkspace workspace)
        {
            ISolution solution = workspace.CurrentSolution;
            ISolution newSolution = solution;

            foreach (IProject project in solution.Projects)
            {
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
                                    IFixer fixer = CreateFixer(type, tree);
                                    SyntaxTree newTree = fixer.Repair();
                                    IDocument newDocument = document.UpdateSyntaxRoot(newTree.GetRoot());
                                    newSolution = newDocument.Project.Solution;
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

        private static IFixer CreateFixer(Type type, SyntaxTree tree)
        {
            ConstructorInfo constructor = type.GetConstructor(new[] { typeof(SyntaxTree) });
            return (IFixer)constructor.Invoke(new[] { tree });
        }

        static List<string> includeFiles;
        static List<Type> fixers;
    }
}
