namespace LogicalParser;

public sealed class NegativeFilter : IFilter
{
    public bool IsSuitable(EvaluationResult result)
    {
        return result.Result == 0;
    }
}