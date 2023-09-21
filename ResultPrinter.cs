namespace LogicalParser;

public static class ResultPrinter
{
    public static void Print(IEnumerable<EvaluationResult> results, IFilter? filter = null)
    {
        foreach (EvaluationResult result in results)
        {
            if (filter == null || filter.IsSuitable(result))
            {
                PrintResult(result);
            }
        }
    }

    private static void PrintResult(EvaluationResult result)
    {
        Console.WriteLine(result.ToValueString());
    }
}