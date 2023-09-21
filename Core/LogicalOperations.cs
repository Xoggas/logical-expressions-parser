namespace LogicalParser;

public static class LogicalOperations
{
    public static int And(int a, int b)
    {
        return a == 1 && b == 1 ? 1 : 0;
    }

    public static int Or(int a, int b)
    {
        return a == 1 || b == 1 ? 1 : 0;
    }

    public static int Equivalence(int a, int b)
    {
        return a == b ? 1 : 0;
    }

    public static int Implication(int a, int b)
    {
        return Or(Negate(a), b);
    }

    public static int Negate(int a)
    {
        return a == 0 ? 1 : 0;
    }
}