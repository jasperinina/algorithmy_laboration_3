using System.Diagnostics;
using System.IO;

namespace lab3.Utilities.QueueAnalyzer;

public class QueuePerformanceAnalyzer
{
    private readonly string filePath;
    private static readonly Random random = new Random();

    // Конструктор с параметром для пользовательского пути или использованием пути по умолчанию
    public QueuePerformanceAnalyzer(string filePath = "inputQueue_Test.txt")
    {
        this.filePath = filePath;

        // Генерация файла с 10,000 строк данных, если файл не существует
        if (!File.Exists(filePath))
        {
            GenerateTestFile(filePath);
        }
    }

    public (double[] dataSizes, double[] times) AnalyzePerformance()
    {
        var dataSizes = new List<double>();
        var times = new List<double>();

        if (!File.Exists(filePath))
        {
            return (dataSizes.ToArray(), times.ToArray()); // Возврат пустых массивов, если файл не найден
        }

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            var stopwatch = new Stopwatch();

            foreach (var line in lines)
            {
                // Создание нового экземпляра FileHandlerQueue для каждого набора данных
                var fileHandler = new FileHandlerQueue(_ => { }); // Пустой делегат для подавления вывода

                stopwatch.Restart();
                fileHandler.ProcessLine(line);
                stopwatch.Stop();

                dataSizes.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length);
                times.Add(stopwatch.Elapsed.TotalMilliseconds);
            }
        }
        catch (Exception ex)
        {
            // Логирование или обработка исключений при необходимости
            Console.WriteLine($"Ошибка при анализе производительности: {ex.Message}");
        }

        return (dataSizes.ToArray(), times.ToArray());
    }

    private void GenerateTestFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            for (int i = 1; i <= 10000; i++)
            {
                string line = GenerateRandomCommands(i);
                writer.WriteLine(line);
            }
        }
    }

    private string GenerateRandomCommands(int count)
    {
        var commands = new List<string>();
        for (int i = 0; i < count; i++)
        {
            int commandType = random.Next(1, 6); // Случайное число от 1 до 5 (включительно)
            switch (commandType)
            {
                case 1:
                    commands.Add($"1,data{random.Next(1, 10000)}"); // Случайное значение для добавления
                    break;
                case 2:
                    commands.Add("2"); // Dequeue
                    break;
                case 3:
                    commands.Add("3"); // Peek
                    break;
                case 4:
                    commands.Add("4"); // isEmpty
                    break;
                case 5:
                    commands.Add("5"); // Print
                    break;
            }
        }

        return string.Join(" ", commands);
    }
}