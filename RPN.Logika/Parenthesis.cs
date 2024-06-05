namespace RPN.Logika;

public class Parenthesis : Token
{
    public char Symbol { get; }

    public Parenthesis(char symbol)
    {
        Symbol = symbol;
    }

    public override string ToString()
    {
        return Symbol.ToString();
    }
}
