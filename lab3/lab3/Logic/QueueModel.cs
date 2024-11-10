using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Реализация операций с очередью (на основе списка и стандартного Queue<T>)

namespace lab3.logic
{
    class QueueModel
    {
        private Node head;
        private Node tail;
        private int count = 0;
        private readonly Action<string> outputHandler;

        public QueueModel(Action<string> outputHandler)
        {
            this.head = null;
            this.outputHandler = outputHandler;
        }

        // Операция Enqueue - добавляет элемент в конец очереди
        public void Enqueue(string elem)
        {
            Node newNode = new Node(elem) { };
            Node tempNode = tail;
            tail = newNode;
            if (count == 0)
            {
                head = newNode;
            }
            else
            {
                tempNode.Next = tail;
            }

            count++;
            outputHandler?.Invoke($"Элемент '{elem}' добавлен в очередь");
        }

        // Операция Dequeue - удаляет и возвращает элемент с начала очереди
        public string Dequeue()
        {
            if (IsEmpty())
            {
                outputHandler?.Invoke("Очередь пуста. Операция Dequeue невозможна.");
                return null;
            }

            string outputData = head.Data;
            head = head.Next;
            count--;
            outputHandler?.Invoke($"Элемент '{outputData}' удален из очереди");
            return outputData;
        }

        // Операция Peek - возвращает первый элемент очереди без удаления
        public string Peek()
        {
            if (IsEmpty())
            {
                return null;
            }

            outputHandler?.Invoke($"Первый элемент очереди: '{head.Data}'");
            return head.Data;
        }

        // Операция isEmpty - проверяет, пуста ли очередь
        public bool IsEmpty()
        {
            return count == 0;
        }

        // Операция Print - выводит элементы очереди
        public void Print(Action<string> outputHandler)
        {
            if (IsEmpty())
            {
                return;
            }

            Node current = head;
            outputHandler?.Invoke("Элементы очереди:");
            while (current != null)
            {
                outputHandler?.Invoke(current.Data);
                current = current.Next;
            }
        }

        //switch case для обработки команд
        //public void ExecuteOperation(string command)
        //{
        //    switch (command)
        //    {
        //        case "2":
        //            Dequeue();
        //            break;
        //        case "3":
        //            Peek();
        //            break;
        //        case "4":
        //            IsEmpty();
        //            break;
        //        case "5":
        //            Print(outputHandler);
        //            break;
        //        default:
        //            string insert = command[2..];
        //            Enqueue(insert);
        //            break;
        //    }
        //}
    }

    //class StandardQueue <T>
    //{
    //    private Queue<T> queue;

    //    public StandardQueue()
    //    {
    //        queue = new Queue<T>();
    //    }

    //    public string Print()
    //    {
    //        T[] arr = queue.ToArray();
    //        string result = string.Empty;
    //        foreach (T el in arr)
    //        {
    //            result += el + " ";
    //        }
    //        return result;
    //    }

    //    public void ExecuteOperation(string command)
    //    {
    //        switch (command)
    //        {
    //            case "2":
    //                queue.Dequeue();
    //                break;
    //            case "3":
    //                if (queue.TryPeek(out T a))
    //                {
    //                    Console.WriteLine(a);
    //                }
    //                break;
    //            case "4":
    //                queue.TryPeek(out T b);
    //                break;
    //            case "5":
    //                Console.WriteLine(Print());
    //                break;
    //            default:
    //                string insert = command[2..];
    //                queue.Enqueue((T)Convert.ChangeType(insert, typeof(T)));
    //                break;
    //        }
    //    }
    //}
}