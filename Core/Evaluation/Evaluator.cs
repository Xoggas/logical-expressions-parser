namespace LogicalParser;

public sealed class Evaluator
{
    private readonly Node _root;
    private readonly SortedDictionary<char, int> _variables;

    public Evaluator(Node root)
    {
        _root = root;
        _variables = new SortedDictionary<char, int>();
    }

    public IEnumerable<EvaluationResult> GetResults()
    {
        FillVariablesDictionary(_root);

        var length = 2 << (_variables.Keys.Count - 1);

        for (var i = 0; i < length; i++)
        {
            UnpackNumberBitsToVariables(i);
            yield return new EvaluationResult(_variables, GetResult(_root));
        }
    }

    private void UnpackNumberBitsToVariables(int number)
    {
        for (var i = _variables.Keys.Count - 1; i >= 0; i--)
        {
            var key = _variables.Keys.ElementAt(i);
            _variables[key] = number & 1;
            number >>= 1;
        }
    }

    private void FillVariablesDictionary(Node node)
    {
        if (node is BinaryNode binaryNode)
        {
            FillVariablesDictionary(binaryNode.Left);
            FillVariablesDictionary(binaryNode.Right);
        }
        else if (node is UnaryNode unaryNode)
        {
            FillVariablesDictionary(unaryNode.Node);
        }
        else if (node is VariableNode variableNode)
        {
            _variables.TryAdd(variableNode.VariableName, 0);
        }
    }

    private int GetResult(Node root)
    {
        if (root is VariableNode variableNode)
        {
            return _variables[variableNode.VariableName];
        }

        if (root is BinaryNode binaryNode)
        {
            return binaryNode.Operation switch
            {
                TokenType.And => LogicalOperations.And(GetResult(binaryNode.Left), GetResult(binaryNode.Right)),
                TokenType.Or => LogicalOperations.Or(GetResult(binaryNode.Left), GetResult(binaryNode.Right)),
                TokenType.Implication => LogicalOperations.Implication(GetResult(binaryNode.Left),
                    GetResult(binaryNode.Right)),
                TokenType.Equivalence => LogicalOperations.Equivalence(GetResult(binaryNode.Left),
                    GetResult(binaryNode.Right)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        if (root is UnaryNode unaryNode && unaryNode.Operation.Type is TokenType.Negation)
        {
            return LogicalOperations.Negate(GetResult(unaryNode.Node));
        }

        throw new Exception("Parsing error!");
    }
}