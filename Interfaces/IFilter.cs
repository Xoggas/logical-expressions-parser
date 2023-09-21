namespace LogicalParser;

public interface IFilter
{
    public bool IsSuitable(EvaluationResult result);
}