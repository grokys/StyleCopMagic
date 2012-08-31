namespace StyleCopMagicCmd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using Roslyn.Compilers.CSharp;
    using Roslyn.Services;
    using StyleCopMagic;
    using Roslyn.Compilers;

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
                Process(workspace);
            }
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

                    if (document.LanguageServices.Language == LanguageNames.CSharp)
                    {
                        Console.WriteLine(document.Name);

                        try
                        {
                            SyntaxTree tree = (SyntaxTree)document.GetSyntaxTree();
                            SA1101 rule = new SA1101(tree);
                            SyntaxTree newTree = rule.Repair();
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

            workspace.ApplyChanges(solution, newSolution);
        }
    }
}
