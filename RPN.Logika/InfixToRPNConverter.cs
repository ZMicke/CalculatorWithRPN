using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RPN.Logika;


public class InfixToRPNConverter
{
    public static List<Token> Convert(string expression)
    {
        List<Token> output = new List<Token>();
        Stack<Token> operators = new Stack<Token>();

        for (int i = 0; i < expression.Length; i++)
        {
            if (char.IsDigit(expression[i]) || expression[i] == '.')
            {
                StringBuilder number = new StringBuilder();
                while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'))
                {
                    number.Append(expression[i]);
                    i++;
                }
                i--;
                output.Add(new Number(double.Parse(number.ToString(), CultureInfo.InvariantCulture)));
            }
            else if ("+-*/^".Contains(expression[i]))
            {
                Operation op = expression[i] switch
                {
                    '+' => new AddOperation(),
                    '-' => new SubtractOperation(),
                    '*' => new MultiplyOperation(),
                    '/' => new DivideOperation(),
                    '^' => new PowerOperation(),
                    _ => throw new InvalidOperationException("Invalid operator.")
                };

                while (operators.Count > 0 && operators.Peek() is Operation operation && operation.Priority >= op.Priority)
                {
                    output.Add(operators.Pop());
                }
                operators.Push(op);
            }
            else if (expression.Substring(i).StartsWith("log"))
            {
                operators.Push(new LogOperation());
                i += 2;
            }
            else if (expression.Substring(i).StartsWith("sqrt"))
            {
                operators.Push(new SqrtOperation());
                i += 3;
            }
            else if (expression.Substring(i).StartsWith("rt"))
            {
                operators.Push(new RtOperation());
                i += 1;
            }
            else if (expression.Substring(i).StartsWith("sin"))
            {
                operators.Push(new SinOperation());
                i += 2;
            }
            else if (expression.Substring(i).StartsWith("cos"))
            {
                operators.Push(new CosOperation());
                i += 2;
            }
            else if (expression.Substring(i).StartsWith("tg"))
            {
                operators.Push(new TgOperation());
                i += 1;
            }
            else if (expression.Substring(i).StartsWith("ctg"))
            {
                operators.Push(new CtgOperation());
                i += 2;
            }
            else if (expression[i] == '(')
            {
                operators.Push(new Parenthesis('('));
            }
            else if (expression[i] == ')')
            {
                while (operators.Count > 0 && operators.Peek() is not Parenthesis)
                {
                    output.Add(operators.Pop());
                }
                if (operators.Count == 0 || operators.Pop() is not Parenthesis)
                {
                    throw new MathExpressionException("Mismatched parentheses.");
                }
            }
            else if (expression[i] == 'x')
            {
                output.Add(new Variable("x"));
            }
            else if (expression[i] == ',')
            {
                while (operators.Count > 0 && operators.Peek() is not Parenthesis)
                {
                    output.Add(operators.Pop());
                }
            }
            else if (!char.IsWhiteSpace(expression[i]))
            {
                throw new MathExpressionException($"Invalid character in expression: {expression[i]}");
            }
        }

        while (operators.Count > 0)
        {
            Token op = operators.Pop();
            if (op is Parenthesis)
            {
                throw new MathExpressionException("Mismatched parentheses.");
            }
            output.Add(op);
        }

        return output;
    }
}

