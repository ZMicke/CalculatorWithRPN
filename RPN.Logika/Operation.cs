namespace RPN.Logika;

public abstract class Operation : Token
{
    public abstract int Priority { get; }
    public abstract double Evaluate(params double[] operands);
}


