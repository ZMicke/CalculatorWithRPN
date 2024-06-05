using System.Globalization;

namespace RPN.Logika;


public class Number : Token
{
    public double Value { get; }

    public Number(double value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}

