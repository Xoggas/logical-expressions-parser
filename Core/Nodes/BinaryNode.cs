namespace LogicalParser;

public sealed class BinaryNode : Node
{
    public readonly Node Left;
    public readonly TokenType Operation;
    public readonly Node Right;

    public BinaryNode(Node left, TokenType operation, Node right)
    {
        Left = left;
        Operation = operation;
        Right = right;
    }

    public override string ToString()
    {
        return $"({Left} {Operation} {Right})";
    }
}