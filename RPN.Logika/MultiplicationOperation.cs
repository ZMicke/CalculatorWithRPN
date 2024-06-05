namespace RPN.Logika;

public class MultiplyOperation : Operation
{
    public override int Priority => 2;

    public override double Evaluate(params double[] operands)
    {
        return operands[0] * operands[1];
    }

    public override string ToString()
    {
        return "*";
    }
}

