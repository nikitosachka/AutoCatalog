using Xunit;

public class CyclomaticComplexityTests
{
    [Fact]
    public void TestCyclomaticComplexity()
    {
        string sampleCode = @"
            public void Example()
            {
                if (x > 10)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine(i);
                    }
                }
                else
                {
                    Console.WriteLine(""x is less than 10"");
                }
            }";

        int complexity = CyclomaticComplexity.CalculateCyclomaticComplexity(sampleCode);
        Assert.Equal(4, complexity);  // Очікується складність 4 (1 + 1 if + 1 for + 1 else)
    }
}
