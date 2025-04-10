using System;
using System.IO;
using System.Linq;

namespace AutoCatalog.Tools
{
    public class CyclomaticComplexity
    {
        public static int Calculate(string code)
        {
            int conditions = code.Split(new[] { "if", "for", "while", "case", "&&", "||" }, StringSplitOptions.None).Length - 1;
            return conditions + 1;
        }

        public static void AnalyzeProject(string rootPath)
        {
            var csFiles = Directory.GetFiles(rootPath, "*.cs", SearchOption.AllDirectories)
                                   .Where(path => !path.Contains("obj") && !path.Contains("bin"))
                                   .ToList();

            Console.WriteLine($"Found {csFiles.Count} .cs files\n");

            foreach (var file in csFiles)
            {
                var code = File.ReadAllText(file);
                var complexity = Calculate(code);
                Console.WriteLine($"{file} - Cyclomatic Complexity: {complexity}");
            }
        }
    }
}
