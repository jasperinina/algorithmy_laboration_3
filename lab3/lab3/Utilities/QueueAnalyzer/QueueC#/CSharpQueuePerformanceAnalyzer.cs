using System.Diagnostics;
using System.IO;

namespace lab3.Utilities.QueueAnalyzer.QueueC_;

public class CSharpQueuePerformanceAnalyzer
{
    private readonly string filePath;

    public CSharpQueuePerformanceAnalyzer(string filePath = "inputQueue_Test.txt")
    {
        this.filePath = filePath;
    }

    public (double[] dataSizes, double[] times) AnalyzePerformanceWithCSharpQueue()
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
            var stopwatch = new Stopwatch();

            foreach (var line in lines)
            {
                string[] commands = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var queue = new Queue<string>(); // Используем стандартную очередь C#

                // Измерение времени обработки команд
                stopwatch.Restart();
                foreach (var command in commands)
                {
                    if (command.StartsWith("1,"))
                    {
                        // Операция вставки (Enqueue)
                        string valueToEnqueue = command.Substring(2);
                        queue.Enqueue(valueToEnqueue);
                    }
                    else
                    {
                        switch (command)
                        {
                            case "2":
                                // Удаление элемента (Dequeue), если очередь не пуста
                                if (queue.Count > 0)
                                    queue.Dequeue();
                                break;
                            case "3":
                                // Просмотр верхнего элемента (Peek), если очередь не пуста
                                if (queue.Count > 0)
                                    _ = queue.Peek();
                                break;
                            case "4":
                                // Проверка на пустоту
                                _ = queue.Count == 0;
                                break;
                            case "5":
                                // Перебор элементов (для измерения просто считаем их)
                                _ = queue.ToArray();
                                break;
                            default:
                                // Неизвестная команда, пропускаем
                                break;
                        }
                    }
                }

                stopwatch.Stop();

                // Сохранение результатов
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