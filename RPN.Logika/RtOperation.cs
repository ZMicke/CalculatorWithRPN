namespace RPN.Logika;

public class RtOperation : Operation
{
    public override int Priority => 4;

    public override double Evaluate(params double[] operands)
    {
        return Math.Pow(operands[1], 1 / operands[0]);
    }

    public override string ToString()
    {
        return "rt";
    }
}

