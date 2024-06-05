namespace RPN.Logika;

public class SinOperation : Operation
{
    public override int Priority => 4;

    public override double Evaluate(params double[] operands)
    {
        return Math.Sin(operands[0]);
    }

    public override string ToString()
    {
        return "sin";
    }
}

