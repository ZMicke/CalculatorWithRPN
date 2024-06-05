namespace RPN.Logika;

public class TgOperation : Operation
{
    public override int Priority => 4;

    public override double Evaluate(params double[] operands)
    {
        return Math.Tan(operands[0]);
    }

    public override string ToString()
    {
        return "tg";
    }
}
