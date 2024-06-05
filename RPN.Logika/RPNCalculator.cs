using System;
using System.Collections.Generic;


namespace RPN.Logika;
public class RPNCalculator
{
    public static double Calculate(List<Token> rpn, double variableValue)
    {
        Stack<double> stack = new Stack<double>();

        foreach (var token in rpn)
        {
            switch (token)
            {
                case Number number:
                    stack.Push(number.Value);
                    break;
                case Variable:
                    stack.Push(variableValue);
                    break;
                case Operation operation:
                    if (operation is LogOperation or RtOperation)
                    {
                        if (stack.Count < 2)
                            throw new MathExpressionException("Invalid RPN expression.");

                        double b = stack.Pop();
                        double a = stack.Pop();

                        stack.Push(operation.Evaluate(a, b));
                    }
                    else if (operation is SqrtOperation or SinOperation or CosOperation or TgOperation or CtgOperation)
                    {
                        if (stack.Count < 1)
                            throw new MathExpressionException("Invalid RPN expression.");

                        double a = stack.Pop();

                        stack.Push(operation.Evaluate(a));
                    }
                    else
                    {
                        if (stack.Count < 2)
                            throw new MathExpressionException("Invalid RPN expression.");

                        double b = stack.Pop();
                        double a = stack.Pop();

                        stack.Push(operation.Evaluate(a, b));
                    }
                    break;
                default:
                    throw new InvalidOperationException("Invalid token.");
            }
        }

        if (stack.Count != 1)
            throw new MathExpressionException("Invalid RPN expression.");

        return stack.Pop();
    }
}
