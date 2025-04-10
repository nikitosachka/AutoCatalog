using AutoCatalog.Tools;
using NUnit.Framework;

namespace Tests.Tools
{
    public class CyclomaticComplexityRunner
    {
        [Test]
        public void RunCyclomaticComplexityAnalysis()
        {
            CyclomaticComplexity.AnalyzeProject("../../../../AutoCatalog");
        }
    }
}
