using System.IO;
using lab3.logic;

namespace lab3.Utilities;

public class FileHandlerInfixToPostfix
{
    private readonly InfixToPostfixConverter converter;
    private readonly string filePath = "inputConversionPostfix.txt";
    private readonly Action<string> outputHandler;

    public FileHandlerInfixToPostfix(Action<string> outputHandler)
    {
        this.converter = new InfixToPostfixConverter();
        this.outputHandler = outputHandler;
    }

    public void ReadFile()
    {
        if (!File.Exists(filePath))
        {
            outputHandler($"Файл {filePath} не найден.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        
        if (lines.Length == 0 || (lines.Length == 1 && string.IsNullOrWhiteSpace(lines[0])))
        {
            outputHandler($"Файл {filePath} пуст.");
            return;
        }
        
        foreach (var line in lines)
        {
            try
            {
                string postfixExpression = converter.ConvertToPostfix(line);
                outputHandler($"Инфиксное выражение: '{line}' -> Постфиксное выражение: '{postfixExpression}'");
            }
            catch (Exception ex)
            {
                outputHandler($"Ошибка при конверсии выражения '{line}': {ex.Message}");
            }
        }
    }
}