namespace RPN.Logika;

public class PowerOperation : Operation
{
    public override int Priority => 3;

    public override double Evaluate(params double[] operands)
    {
        return Math.Pow(operands[0], operands[1]);
    }

    public override string ToString()
    {
        return "^";
    }
}

