namespace LogicalParser;

public sealed class VariableNode : Node
{
    public readonly char VariableName;

    public VariableNode(char variableName)
    {
        VariableName = variableName;
    }

    public override string ToString()
    {
        return $"{VariableName}";
    }
}