namespace RPN.Logika;

public class CosOperation : Operation
{
    public override int Priority => 4;

    public override double Evaluate(params double[] operands)
    {
        return Math.Cos(operands[0]);
    }

    public override string ToString()
    {
        return "cos";
    }
}

