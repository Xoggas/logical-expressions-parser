namespace LogicalParser;

public sealed class PositiveFilter : IFilter
{
    public bool IsSuitable(EvaluationResult result)
    {
        return result.Result == 1;
    }
}