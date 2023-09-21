namespace LogicalParser;

public sealed class UnaryNode : Node
{
    public readonly Token Operation;
    public readonly Node Node;

    public UnaryNode(Token operation, Node node)
    {
        Operation = operation;
        Node = node;
    }

    public override string ToString()
    {
        return $"({Operation}({Node}))";
    }
}