namespace LogicalParser;

public sealed class Token
{
    public readonly TokenType Type;
    public readonly char? Value;

    public Token(TokenType type, char value)
    {
        Type = type;
        Value = char.ToUpper(value);
    }

    public Token(TokenType type)
    {
        Type = type;
    }

    public static readonly Token Eof = new(TokenType.Eof);
    public static readonly Token Negation = new(TokenType.Negation);
    public static readonly Token And = new(TokenType.And);
    public static readonly Token Or = new(TokenType.Or);
    public static readonly Token Implication = new(TokenType.Implication);
    public static readonly Token Equivalence = new(TokenType.Equivalence);
    public static readonly Token LeftParen = new(TokenType.LeftParen);
    public static readonly Token RightParent = new(TokenType.RightParen);

    public override string ToString()
    {
        return Value == null ? $"{Type}" : $"{Type} : {Value}";
    }
}