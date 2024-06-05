namespace RPN.Logika;

public class Variable : Token
{
    public string Name { get; }

    public Variable(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}
