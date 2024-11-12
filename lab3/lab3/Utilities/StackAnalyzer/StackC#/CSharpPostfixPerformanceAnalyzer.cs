using System.Diagnostics;
using System.IO;

namespace lab3.Utilities.StackAnalyzer.StackC_;

public class CSharpPostfixPerformanceAnalyzer
{
    private readonly string filePath = "inputPostfixTest.txt";

    public (double[] expressionLengths, double[] times) AnalyzePerformanceWithCSharpStack()
    {
        var expressionLengths = new List<double>();
        var times = new List<double>();

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Файл {filePath} не найден.");
        }

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            var stopwatch = new Stopwatch();

            foreach (var line in lines)
            {
                string[] tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // Измерение времени вычисления постфиксного выражения
                stopwatch.Restart();
                EvaluatePostfixExpression(tokens);
                stopwatch.Stop();

                // Сохранение длины выражения и времени выполнения
                expressionLengths.Add(tokens.Length);
                times.Add(stopwatch.Elapsed.TotalMilliseconds);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при анализе производительности: {ex.Message}");
        }

        return (expressionLengths.ToArray(), times.ToArray());
    }

    private void EvaluatePostfixExpression(string[] tokens)
    {
        var stack = new Stack<double>();

        foreach (var token in tokens)
        {
            if (double.TryParse(token, out double number))
            {
                // Если токен - число, добавляем в стек
                stack.Push(number);
            }
            else
            {
                // Если токен - оператор, выполняем операцию
                if (stack.Count < 2) continue; // Недостаточно операндов для выполнения операции

                double b = stack.Pop();
                double a = stack.Pop();

                switch (token)
                {
                    case "+":
                        stack.Push(a + b);
                        break;
                    case "-":
                        stack.Push(a - b);
                        break;
                    case "*":
                        stack.Push(a * b);
                        break;
                    case "/":
                        stack.Push(b != 0 ? a / b : 0); // Проверка на деление на ноль
                        break;
                    case "^":
                        stack.Push(Math.Pow(a, b));
                        break;
                    default:
                        // Неизвестный оператор
                        break;
                }
            }
        }
    }
}