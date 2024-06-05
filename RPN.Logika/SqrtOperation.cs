namespace RPN.Logika;

public class SqrtOperation : Operation
{
    public override int Priority => 4;

    public override double Evaluate(params double[] operands)
    {
        return Math.Sqrt(operands[0]);
    }

    public override string ToString()
    {
        return "sqrt";
    }
}

