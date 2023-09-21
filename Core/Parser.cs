namespace LogicalParser;

public sealed class Parser
{
    private readonly Lexer _lexer;
    private Token _currentToken;

    public Parser(string input)
    {
        _lexer = new Lexer(input);
        _currentToken = _lexer.GetNextToken();
    }

    private static Exception GetException()
    {
        return new Exception("Parsing Error!");
    }

    public Node GetAstRootNode()
    {
        return GetEquivalenceNode();
    }

    private Node GetEquivalenceNode()
    {
        return GetNextNode(TokenType.Equivalence, GetImplicationNode);
    }

    private Node GetImplicationNode()
    {
        return GetNextNode(TokenType.Implication, GetOrNode);
    }

    private Node GetOrNode()
    {
        return GetNextNode(TokenType.Or, GetAndNode);
    }

    private Node GetAndNode()
    {
        return GetNextNode(TokenType.And, GetFactorNode);
    }

    private Node GetNextNode(TokenType operatorType, Func<Node> operandGetter)
    {
        Node node = operandGetter();

        while (_currentToken.Type == operatorType)
        {
            EatAndGetNextToken(operatorType);
            node = new BinaryNode(node, operatorType, operandGetter());
        }

        return node;
    }

    private Node GetFactorNode()
    {
        Token currentToken = _currentToken;

        switch (currentToken.Type)
        {
            case TokenType.Negation:
                EatAndGetNextToken(TokenType.Negation);
                return new UnaryNode(Token.Negation, GetFactorNode());
            case TokenType.Variable:
                EatAndGetNextToken(TokenType.Variable);
                return new VariableNode(currentToken.Value.GetValueOrDefault());
            case TokenType.LeftParen:
                EatAndGetNextToken(TokenType.LeftParen);
                Node expression = GetEquivalenceNode();
                EatAndGetNextToken(TokenType.RightParen);
                return expression;
            default:
                throw GetException();
        }
    }

    private void EatAndGetNextToken(TokenType tokenType)
    {
        if (_currentToken.Type == tokenType)
        {
            _currentToken = _lexer.GetNextToken();
        }
        else
        {
            throw new Exception($"Expected {tokenType} but got {_currentToken.Type}");
        }
    }
}