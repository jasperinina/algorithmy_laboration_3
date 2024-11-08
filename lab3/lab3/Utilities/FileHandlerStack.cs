using System.IO;
using lab3.logic;

namespace lab3.Utilities;

// Чтение данных из input.txt

public class FileHandlerStack
{
    private readonly StackModel stackModel;
    private readonly string filePath = "inputStack.txt";
    private readonly Action<string> outputHandler;

    public FileHandlerStack(Action<string> outputHandler)
    {
        this.stackModel = new StackModel(outputHandler);
        this.outputHandler = outputHandler; // Инициализация делегата вывода
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
            // Автоматическое чтение файла после перезаписи
            ReadFile();
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
                // Операция вставки (Push)
                string valueToPush = command.Substring(2);
                stackModel.Push(valueToPush);
            }
            else
            {
                switch (command)
                {
                    case "2":
                        stackModel.Pop();
                        break;
                    case "3":
                        stackModel.Top();
                        break;
                    case "4":
                        bool isEmpty = stackModel.IsEmpty();
                        outputHandler(isEmpty ? "Стек пуст." : "Стек не пуст.");
                        break;
                    case "5":
                        stackModel.Print(outputHandler);
                        break;
                    default:
                        outputHandler($"Неизвестная команда: {command}");
                        break;
                }
            }
        }
    }
}