public class CyclomaticComplexity
{
    public static int CalculateCyclomaticComplexity(string code)
    {
        int complexity = 1;  // Мінімальна складність = 1 (для одного шляху)

        string[] decisionPoints = { "if", "for", "while", "foreach", "case", "catch", "try", "else" };

        foreach (var decision in decisionPoints)
        {
            complexity += CountOccurrences(code, decision);
        }

        return complexity;
    }

    private static int CountOccurrences(string code, string decisionPoint)
    {
        int count = 0;
        int index = 0;

        while ((index = code.IndexOf(decisionPoint, index)) != -1)
        {
            count++;
            index += decisionPoint.Length;
        }

        return count;
    }
}
