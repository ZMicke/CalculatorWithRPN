namespace RPN.Logika;

public class SubtractOperation : Operation
{
    public override int Priority => 1;

    public override double Evaluate(params double[] operands)
    {
        return operands[0] - operands[1];
    }

    public override string ToString()
    {
        return "-";
    }
}

