using System.Diagnostics;
using System.IO;
using lab3.logic;

namespace lab3.Utilities.StackAnalyzer;

public class PostfixPerformanceAnalyzer
{
    private readonly string filePath;

    public PostfixPerformanceAnalyzer(string filePath = "inputPostfixTest.txt")
    {
        this.filePath = filePath;

        // Генерация файла с примерами выражений, если файл не существует
        if (!File.Exists(filePath))
        {
            GenerateTestFile(filePath);
        }
    }

    public (double[] expressionLengths, double[] times) AnalyzePerformance()
    {
        var expressionLengths = new List<double>();
        var times = new List<double>();

        if (!File.Exists(filePath))
        {
            return (expressionLengths.ToArray(), times.ToArray()); // Возврат пустых массивов, если файл не найден
        }

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            var evaluator = new PostfixEvaluator();
            var stopwatch = new Stopwatch();

            foreach (var line in lines)
            {
                stopwatch.Restart();
                try
                {
                    evaluator.Evaluate(line); // Вычисление выражения
                }
                catch
                {
                    // Игнорируем ошибки для простоты анализа производительности
                }
                stopwatch.Stop();

                expressionLengths.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length);
                times.Add(stopwatch.Elapsed.TotalMilliseconds);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при анализе производительности: {ex.Message}");
        }

        return (expressionLengths.ToArray(), times.ToArray());
    }

    private void GenerateTestFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            for (int i = 1; i <= 5000; i++)
            {
                string expression = GenerateRandomPostfixExpression(i);
                writer.WriteLine(expression);
            }
        }
    }

    private string GenerateRandomPostfixExpression(int length)
    {
        var tokens = new List<string>();
        var random = new Random();

        for (int i = 0; i < length; i++)
        {
            if (random.Next(0, 2) == 0)
            {
                // Случайное число
                tokens.Add(random.Next(1, 100).ToString());
            }
            else
            {
                // Случайная операция
                string[] operators = { "+", "-", "*", "/", "^", "ln", "cos", "sin", "sqrt" };
                tokens.Add(operators[random.Next(operators.Length)]);
            }
        }

        return string.Join(" ", tokens);
    }
}