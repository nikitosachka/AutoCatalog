using AutoCatalog.Tools;
using NUnit.Framework;

namespace Tests.Tools
{
    public class CyclomaticComplexityRunner
    {
        [Test]
        public void RunCyclomaticComplexityAnalysis()
        {
            CyclomaticComplexity.AnalyzeProject("https://github.com/nikitosachka/AutoCatalog");
        }
    }
}
