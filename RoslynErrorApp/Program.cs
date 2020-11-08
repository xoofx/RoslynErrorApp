using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace RoslynErrorApp
{    
    class Program
    {
        static async Task Main(string[] args)
        {
            Microsoft.Build.Locator.MSBuildLocator.RegisterDefaults();
            var workspace = MSBuildWorkspace.Create(new Dictionary<string, string>()
            {
                // Doesn't work
                // {"TargetFramework", "netcoreapp3.1"},
            });
            workspace.WorkspaceFailed += (sender, eventArgs) => Console.WriteLine(eventArgs.Diagnostic.Message);

            var solutionPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "RoslynErrorApp.sln"));
            var solution = await workspace.OpenSolutionAsync(solutionPath, new ConsoleProgressReporter());
            var project = solution.Projects.First(x => x.Name == "RoslynErrorApp");

            var compilation = await project.GetCompilationAsync();

            var errors = compilation.GetDiagnostics().Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error).ToList();

            if (errors.Count > 0)
            {
                Console.WriteLine("Compilation errors:");
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }

                Console.WriteLine("Error, Exiting.");
                return;
            }
            Console.WriteLine("No errors");
        }

        private class ConsoleProgressReporter : IProgress<ProjectLoadProgress>
        {
            public void Report(ProjectLoadProgress loadProgress)
            {
                var projectDisplay = Path.GetFileName(loadProgress.FilePath);
                if (loadProgress.TargetFramework != null)
                {
                    projectDisplay += $" ({loadProgress.TargetFramework})";
                }

                Console.WriteLine($"{loadProgress.Operation,-15} {loadProgress.ElapsedTime,-15:m\\:ss\\.fffffff} {projectDisplay}");
            }
        }
    }
}
