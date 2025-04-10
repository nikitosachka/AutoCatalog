using System;
using System.IO;

namespace AutoCatalog.Tools
{
    public class CyclomaticComplexity
    {
        public static void AnalyzeProject(string path)
        {
            int complexity = 0;

            foreach (string file in Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories))
            {
                string code = File.ReadAllText(file);
                complexity += CalculateComplexity(code);
            }

            Console.WriteLine($"Cyclomatic Complexity of project: {complexity}");
        }

        private static int CalculateComplexity(string code)
        {
            int conditions = CountOccurrences(code, "if")
                           + CountOccurrences(code, "while")
                           + CountOccurrences(code, "for")
                           + CountOccurrences(code, "case")
                           + CountOccurrences(code, "catch");

            return conditions + 1; // +1 за кожен метод
        }

        private static int CountOccurrences(string code, string word)
        {
            return code.Split(new[] { word }, StringSplitOptions.None).Length - 1;
        }
    }
}
