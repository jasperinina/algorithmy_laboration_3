using System.IO;
using lab3.logic;

namespace lab3.Utilities;

public class FileHandlerAlgorithms
{
    private readonly string filePath = "inputAlgorithms.txt";
    private readonly Action<string> outputHandler;

    public FileHandlerAlgorithms(Action<string> outputHandler)
    {
        this.outputHandler = outputHandler;
    }
    
    public Node ReadFile()
    {
        if (!File.Exists(filePath))
        {
            outputHandler($"Файл {filePath} не найден.");
            return null;
        }

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length == 0 || (lines.Length == 1 && string.IsNullOrWhiteSpace(lines[0])))
            {
                outputHandler($"Файл {filePath} пуст.");
                return null;
            }

            // Используем первую строку для создания списка
            return CreateLinkedList(lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }
        catch (Exception ex)
        {
            outputHandler($"Ошибка при чтении файла: {ex.Message}");
            return null;
        }
    }
        
    private void ProcessLine(string line)
    {
        // Разделение строки на элементы списка
        string[] elements = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (elements.Length == 0)
        {
            outputHandler("Пустая строка или отсутствуют элементы.");
            return;
        }

        // Создание связного списка на основе элементов
        Node head = CreateLinkedList(elements);
        outputHandler("Список из файла:");
        PrintList(head);

        // Здесь можно добавить вызов алгоритмов или дополнительных операций
    }

    public Node CreateLinkedList(string[] elements)
    {
        Node head = null;
        Node tail = null;

        foreach (string element in elements)
        {
            Node newNode = new Node(element);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                tail = newNode;
            }
        }

        return head;
    }

    private void PrintList(Node head)
    {
        Node current = head;
        while (current != null)
        {
            outputHandler(current.Data + " ");
            current = current.Next;
        }
        outputHandler(""); // Переход на новую строку
    }
}