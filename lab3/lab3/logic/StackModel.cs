namespace lab3.logic;

// Реализация операций со стеком с использованием связного списка

// Узел связного списка
public class Node
{
    public string Data { get; set; }
    public Node Next { get; set; }

    public Node(string data)
    {
        Data = data;
        Next = null;
    }
}

public class StackModel
{
    private Node top;
    private readonly Action<string> outputHandler;

    // Конструктор принимает делегат для вывода
    public StackModel(Action<string> outputHandler)
    {
        this.top = null;
        this.outputHandler = outputHandler;
    }

    // Операция Push - добавляет элемент на вершину стека
    public void Push(string elem)
    {
        Node newNode = new Node(elem)
        { 
            Next = top
        };
        top = newNode;
        outputHandler?.Invoke($"Элемент '{elem}' добавлен в стек.");
    }

    // Операция Pop - удаляет и возвращает элемент с вершины стека
    public string Pop()
    { 
        if (IsEmpty())
        {
            outputHandler?.Invoke("Стек пуст. Операция Pop невозможна.");
            return null;
        }

        string poppedData = top.Data;
        top = top.Next;
        outputHandler?.Invoke($"Элемент '{poppedData}' удален из стека.");
        return poppedData;
    }

    // Операция Top - возвращает элемент с вершины стека без удаления
    public string Top()
    {
        if (IsEmpty())
        {
            return null;
        }

        outputHandler?.Invoke($"Вершина стека: '{top.Data}'.");
        return top.Data;
    }

    // Операция isEmpty - проверяет, пуст ли стек
    public bool IsEmpty()
    {
        return top == null;
    }

    // Операция Print - выводит элементы стека
    public void Print(Action<string> outputHandler)
    {
        if (IsEmpty())
        {
            return;
        }

        Node current = top;
        outputHandler?.Invoke("Элементы стека:");
        while (current != null)
        {
            outputHandler?.Invoke(current.Data);
            current = current.Next;
        }
    }
}