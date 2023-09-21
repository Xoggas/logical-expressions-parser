using System.Text;

namespace LogicalParser;

public sealed class EvaluationResult
{
    public readonly IDictionary<char, int> Variables;
    public readonly int Result;

    public EvaluationResult(IDictionary<char, int> variables, int result)
    {
        Variables = new Dictionary<char, int>(variables);
        Result = result;
    }

    public string ToKeyString()
    {
        return ToStringFromCollection(Variables.Keys) + "F";
    }

    public string ToValueString()
    {
        return ToStringFromCollection(Variables.Values) + Result;
    }

    private static string ToStringFromCollection<T>(IEnumerable<T> collection)
    {
        StringBuilder stringBuilder = new();

        foreach (T value in collection)
        {
            stringBuilder.Append(value);
            stringBuilder.Append(' ');
        }

        return stringBuilder.ToString();
    }
}