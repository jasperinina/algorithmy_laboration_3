using System.IO;
using lab3.logic;

namespace lab3.Utilities;

// Чтение данных из inputPostfix.txt
public class FileHandlerPostfix
{
    private readonly PostfixEvaluator evaluator;
    private readonly string filePath = "inputPostfix.txt";
    private readonly Action<string> outputHandler;

    public FileHandlerPostfix(Action<string> outputHandler)
    {
        this.evaluator = new PostfixEvaluator();
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
        
    private void ProcessLine(string line)
    {
        try
        {
            double result = evaluator.Evaluate(line);
            outputHandler($"Результат выражения '{line}' = {result}");
        }
        catch (Exception ex)
        {
            outputHandler($"Ошибка при вычислении выражения '{line}': {ex.Message}");
        }
    }
}