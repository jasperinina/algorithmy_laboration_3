using System.Diagnostics;
using System.IO;
using lab3.logic;

namespace lab3.Utilities.StackAnalyzer;

public class InfixToPostfixPerformanceAnalyzer
{
    private readonly string filePath;

    public InfixToPostfixPerformanceAnalyzer(string filePath = "inputConversionPostfixTest.txt")
    {
        this.filePath = filePath;

        // Генерация файла с 10,000 строк данных, если файл не существует
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
            var converter = new InfixToPostfixConverter();
            var stopwatch = new Stopwatch();

            foreach (var line in lines)
            {
                stopwatch.Restart();
                try
                {
                    converter.ConvertToPostfix(line); // Преобразование выражения
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
            for (int i = 1; i <= 10000; i++)
            {
                string expression = GenerateRandomInfixExpression(i);
                writer.WriteLine(expression);
            }
        }
    }

    private string GenerateRandomInfixExpression(int length)
    {
        var tokens = new List<string>();
        var random = new Random();

        string[] operators = { "+", "-", "*", "/", "^", "(", ")" };
        string[] functions = { "ln", "cos", "sin", "sqrt" };

        for (int i = 0; i < length; i++)
        {
            int choice = random.Next(0, 3);

            if (choice == 0)
            {
                // Случайное число
                tokens.Add(random.Next(1, 100).ToString());
            }
            else if (choice == 1)
            {
                // Случайный оператор
                tokens.Add(operators[random.Next(operators.Length)]);
            }
            else
            {
                // Случайная функция
                tokens.Add(functions[random.Next(functions.Length)]);
                tokens.Add("(");
                tokens.Add(random.Next(1, 100).ToString());
                tokens.Add(")");
            }
        }

        return string.Join(" ", tokens);
    }
}