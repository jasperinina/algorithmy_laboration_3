using System.IO;

namespace lab3.Utilities.QueueAnalyzer;

public class QueueDifferentContentAnalyzer
{
    private readonly string filePath;
    private static readonly Random random = new Random();

    public QueueDifferentContentAnalyzer(string filePath = "inputQueueDifferentContent_Test.txt")
    {
        this.filePath = filePath;
        GenerateTestFile(filePath);
    }

    private void GenerateTestFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            // Генерация двух различных наборов одинаковой длины, но различного состава
            for (int i = 1; i <= 100; i++) // Генерация 100 строк данных
            {
                string line1 = GenerateHeavyCommands(100); // "Тяжелые" команды
                string line2 = GenerateLightCommands(100); // "Легкие" команды
                writer.WriteLine(line1);
                writer.WriteLine(line2);
            }
        }
    }

    private string GenerateHeavyCommands(int count)
    {
        var commands = new List<string>();
        for (int i = 0; i < count; i++)
        {
            // Генерация "тяжелых" команд (больше Enqueue и Dequeue)
            int commandType = random.Next(1, 3); // Команды 1 (Enqueue) и 2 (Dequeue)
            switch (commandType)
            {
                case 1:
                    commands.Add($"1,data{random.Next(1, 10000)}"); // Enqueue с случайными данными
                    break;
                case 2:
                    commands.Add("2"); // Dequeue
                    break;
            }
        }

        return string.Join(" ", commands);
    }

    private string GenerateLightCommands(int count)
    {
        var commands = new List<string>();
        for (int i = 0; i < count; i++)
        {
            // Генерация "легких" команд (Peek, isEmpty, Print)
            int commandType = random.Next(3, 6); // Команды 3, 4 и 5
            switch (commandType)
            {
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