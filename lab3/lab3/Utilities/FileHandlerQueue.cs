using System.IO;
using lab3.logic;

namespace lab3.Utilities;

public class FileHandlerQueue
{
    private readonly string filePath = "inputQueue.txt";
    private readonly Action<string> outputHandler;
    private readonly QueueModel queueModel;

    public FileHandlerQueue(Action<string> outputHandler)
    {
        this.outputHandler = outputHandler;
        this.queueModel = new QueueModel(outputHandler);
    }

    public void ReadFile()
    {
        if (!File.Exists(filePath))
        {
            outputHandler($"Файл {filePath} не найден.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length == 0 || (lines.Length == 1 && string.IsNullOrWhiteSpace(lines[0])))
            {
                outputHandler($"Файл {filePath} пуст.");
                return;
            }

            foreach (var line in lines)
            {
                ProcessLine(line);
            }
        }
        catch (Exception ex)
        {
            outputHandler($"Ошибка при чтении файла: {ex.Message}");
        }
    }

    public void OverwriteFile(string newContent)
    {
        try
        {
            File.WriteAllText(filePath, newContent);
            outputHandler($"Файл {filePath} был успешно перезаписан.");
        }
        catch (Exception ex)
        {
            outputHandler($"Ошибка при перезаписи файла: {ex.Message}");
        }
    }

    public void ProcessLine(string line)
    {
        string[] commands = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var command in commands)
        {
            if (command.StartsWith("1,"))
            {
                // Операция вставки (Enqueue)
                string valueToEnqueue = command.Substring(2);
                queueModel.Enqueue(valueToEnqueue);
            }
            else
            {
                switch (command)
                {
                    case "2":
                        queueModel.Dequeue();
                        break;
                    case "3":
                        queueModel.Peek();
                        break;
                    case "4":
                        bool isEmpty = queueModel.IsEmpty();
                        outputHandler(isEmpty ? "Очередь пуста." : "Очередь не пуста.");
                        break;
                    case "5":
                        queueModel.Print(outputHandler);
                        break;
                    default:
                        outputHandler($"Неизвестная команда: {command}");
                        break;
                }
            }
        }
    }
}