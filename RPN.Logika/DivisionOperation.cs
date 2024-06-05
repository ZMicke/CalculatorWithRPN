namespace RPN.Logika;

public class DivideOperation : Operation
{
    public override int Priority => 2;

    public override double Evaluate(params double[] operands)
    {
        if (operands[1] == 0)
            throw new DivideByZeroException("Division by zero.");
        return operands[0] / operands[1];
    }

    public override string ToString()
    {
        return "/";
    }
}

