using System.Diagnostics;
using System.IO;

namespace lab3.Utilities.StackAnalyzer.StackC_;

public class CSharpStackPerformanceAnalyzer
{
    private readonly string filePath = "inputStackTest.txt";

    public (double[] dataSizes, double[] times) AnalyzePerformanceWithCSharpStack()
    {
        var dataSizes = new List<double>();
        var times = new List<double>();

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Файл {filePath} не найден.");
        }

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            var stack = new Stack<string>();
            var stopwatch = new Stopwatch();

            foreach (var line in lines)
            {
                string[] commands = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // Измерение времени обработки команд в стеке C#
                stopwatch.Restart();
                foreach (var command in commands)
                {
                    if (command.StartsWith("1,"))
                    {
                        // Операция вставки (Push)
                        string valueToPush = command.Substring(2);
                        stack.Push(valueToPush);
                    }
                    else
                    {
                        switch (command)
                        {
                            case "2":
                                // Операция удаления (Pop), если стек не пуст
                                if (stack.Count > 0)
                                    stack.Pop();
                                break;
                            case "3":
                                // Операция просмотра верхнего элемента (Top/Peek), если стек не пуст
                                if (stack.Count > 0)
                                    _ = stack.Peek();
                                break;
                            case "4":
                                // Проверка на пустоту стека
                                _ = stack.Count == 0;
                                break;
                            case "5":
                                // Печать элементов (для измерения просто считаем элементы)
                                _ = stack.ToArray();
                                break;
                            default:
                                // Неизвестная команда, пропускаем
                                break;
                        }
                    }
                }

                stopwatch.Stop();

                // Сохранение результатов измерений
                dataSizes.Add(commands.Length);
                times.Add(stopwatch.Elapsed.TotalMilliseconds);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при анализе производительности: {ex.Message}");
        }

        return (dataSizes.ToArray(), times.ToArray());
    }
}
