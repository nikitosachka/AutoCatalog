using AutoCatalog.Tools;
using NUnit.Framework;
using System;
using System.IO;

namespace Tests.Tools
{
    public class CyclomaticComplexityRunner
    {
        [Test]
        public void RunCyclomaticComplexityAnalysis()
        {
            // Шлях до проєкту
            var projectPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../AutoCatalog");

            // Розрахунок цикломатичної складності
            int complexity = CyclomaticComplexity.AnalyzeProject(projectPath);

            // Виведення в консоль
            Console.WriteLine($"Cyclomatic Complexity of project: {complexity}");
        }
    }
}
