namespace LogicalParser;

public sealed class Lexer
{
    private readonly string _input;
    private int _currentSymbolIndex;

    public Lexer(string input)
    {
        _input = input;
        _currentSymbolIndex = 0;
    }

    private bool IsEof => _currentSymbolIndex >= _input.Length;
    private char CurrentSymbol => _input[_currentSymbolIndex];

    public Token GetNextToken()
    {
        if (IsEof)
        {
            return Token.Eof;
        }

        SkipWhiteSpaces();

        Token token = CurrentSymbol switch
        {
            '+' or '|' => Token.Or,
            '&' or '*' => Token.And,
            '~' or '!' => Token.Negation,
            '(' => Token.LeftParen,
            ')' => Token.RightParent,
            '>' => Token.Implication,
            '=' => Token.Equivalence,
            _ => char.IsLetter(CurrentSymbol)
                ? new Token(TokenType.Variable, CurrentSymbol)
                : throw new ArgumentOutOfRangeException()
        };

        Advance();

        return token;
    }

    private void SkipWhiteSpaces()
    {
        while (char.IsWhiteSpace(CurrentSymbol) && !IsEof)
        {
            Advance();
        }
    }

    private void Advance()
    {
        _currentSymbolIndex++;
    }
}