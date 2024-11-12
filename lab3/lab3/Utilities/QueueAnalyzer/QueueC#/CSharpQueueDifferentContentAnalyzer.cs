using System.Diagnostics;
using System.IO;

namespace lab3.Utilities.QueueAnalyzer.QueueC_;

public class CSharpQueueDifferentContentAnalyzer
{
    private readonly string filePath;

    public CSharpQueueDifferentContentAnalyzer(string filePath = "inputQueueTest_DifferentContent.txt")
    {
        this.filePath = filePath;
    }

    public (double[] dataSizes, double[] times, string[] labels) AnalyzePerformanceWithCSharpQueue()
    {
        var dataSizes = new List<double>();
        var times = new List<double>();
        var labels = new List<string>();

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Файл {filePath} не найден.");
        }

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            var stopwatch = new Stopwatch();

            int counter = 1;
            foreach (var line in lines)
            {
                string[] commands = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var queue = new Queue<string>(); // Используем стандартную очередь C#

                // Измерение времени выполнения команд
                stopwatch.Restart();
                foreach (var command in commands)
                {
                    if (command.StartsWith("1,"))
                    {
                        string valueToEnqueue = command.Substring(2);
                        queue.Enqueue(valueToEnqueue); // Вставка элемента
                    }
                    else
                    {
                        switch (command)
                        {
                            case "2":
                                if (queue.Count > 0)
                                    queue.Dequeue(); // Удаление элемента
                                break;
                            case "3":
                                if (queue.Count > 0)
                                    _ = queue.Peek(); // Просмотр верхнего элемента
                                break;
                            case "4":
                                _ = queue.Count == 0; // Проверка на пустоту
                                break;
                            case "5":
                                _ = queue.ToArray(); // Симуляция печати элементов
                                break;
                            default:
                                break;
                        }
                    }
                }

                stopwatch.Stop();

                // Сохранение результатов
                dataSizes.Add(commands.Length);
                times.Add(stopwatch.Elapsed.TotalMilliseconds);

                // Добавляем метки: чередуем "Тяжелые операции" и "Легкие операции"
                labels.Add(counter % 2 == 1 ? "Тяжелые операции" : "Легкие операции");
                counter++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при анализе производительности: {ex.Message}");
        }

        return (dataSizes.ToArray(), times.ToArray(), labels.ToArray());
    }
}