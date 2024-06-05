using System;
using RPN.Logika;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите математическое выражение:");
        string input = Console.ReadLine();

        Console.WriteLine("Введите значение переменной x:");
        if (!double.TryParse(Console.ReadLine(), out double variableValue))
        {
            Console.WriteLine("Ошибка: неверное значение переменной x.");
            return;
        }

        try
        {
            var rpn = InfixToRPNConverter.Convert(input);
            Console.WriteLine("\nОбратная польская запись (ОПЗ):");
            foreach (var token in rpn)
            {
                Console.Write(token + " ");
            }
            Console.WriteLine();

            double resultRPN = RPNCalculator.Calculate(rpn, variableValue);
            Console.WriteLine("\nРезультат вычисления ОПЗ:");
            Console.WriteLine(resultRPN);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}