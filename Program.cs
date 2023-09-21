namespace LogicalParser;

public static class Program
{
    public static void Main()
    {
        var input = Console.ReadLine();

        while (string.IsNullOrEmpty(input) == false)
        {
            try
            {
                PrintResultsForInput(input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            input = Console.ReadLine();
        }
    }

    private static void PrintResultsForInput(string input)
    {
        Parser parser = new(input);
        Node astRootNode = parser.GetAstRootNode();
        Evaluator evaluator = new(astRootNode);

        var results = evaluator.GetResults().ToArray();

        Console.WriteLine(results[0].ToKeyString());
        ResultPrinter.Print(results);
    }
}