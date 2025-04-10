using AutoCatalog.Tools;
using NUnit.Framework;
using System.IO;

namespace Tests.Tools
{
    public class CyclomaticComplexityRunner
    {
        [Test]
        public void RunCyclomaticComplexityAnalysis()
        {
            var projectPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../AutoCatalog");
            CyclomaticComplexity.AnalyzeProject(projectPath);
        }
    }
}
