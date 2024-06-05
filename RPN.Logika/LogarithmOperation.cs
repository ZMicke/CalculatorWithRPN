namespace RPN.Logika;

public class LogOperation : Operation
{
    public override int Priority => 4;

    public override double Evaluate(params double[] operands)
    {
        return Math.Log(operands[1], operands[0]);
    }

    public override string ToString()
    {
        return "log";
    }
}

