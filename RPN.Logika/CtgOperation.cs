namespace RPN.Logika;

public class CtgOperation : Operation
{
    public override int Priority => 4;

    public override double Evaluate(params double[] operands)
    {
        return 1 / Math.Tan(operands[0]);
    }

    public override string ToString()
    {
        return "ctg";
    }
}

