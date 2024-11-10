namespace lab3.logic;

public class LinkedListOperations
{
    private readonly Action<string> outputHandler;

    public LinkedListOperations(Action<string> outputHandler)
    {
        this.outputHandler = outputHandler;
    }

    // Задание 1: Перевернуть список
    public void ReverseList1(ref Node head)
    {
        if (head == null)
        {
            outputHandler?.Invoke("Ошибка: список пуст. Нечего переворачивать.");
            return;
        }

        Node prev = null;
        Node current = head;
        Node next = null;

        while (current != null)
        {
            next = current.Next;
            current.Next = prev;
            prev = current;
            current = next;
        }
        head = prev;
    }

    // Задание 2: Перенести последний элемент в начало
    public void MoveLastToFirst2(ref Node head)
    {
        if (head == null)
        {
            outputHandler?.Invoke("Ошибка: список пуст. Операция невозможна.");
            return;
        }

        if (head.Next == null)
        {
            outputHandler?.Invoke("Предупреждение: в списке только один элемент. Операция не изменила список.");
            return;
        }

        Node secondLast = null;
        Node last = head;

        while (last.Next != null)
        {
            secondLast = last;
            last = last.Next;
        }

        secondLast.Next = null;
        last.Next = head;
        head = last;
    }

    // Задание 2 (альтернативное): Перенести первый элемент в конец
    public void MoveFirstToLast2(ref Node head)
    {
        if (head == null)
        {
            outputHandler?.Invoke("Ошибка: список пуст. Операция невозможна.");
            return;
        }

        if (head.Next == null)
        {
            outputHandler?.Invoke("Предупреждение: в списке только один элемент. Операция не изменила список.");
            return;
        }

        Node first = head;
        Node current = head;

        while (current.Next != null)
        {
            current = current.Next;
        }

        head = head.Next;
        current.Next = first;
        first.Next = null;
    }

    // Задание 3: Подсчитать количество различных элементов
    public int CountDistinctElements3(Node head)
    {
        if (head == null)
        {
            outputHandler?.Invoke("Ошибка: список пуст.");
            return 0;
        }

        HashSet<string> elements = new HashSet<string>();
        Node current = head;

        while (current != null)
        {
            elements.Add(current.Data);
            current = current.Next;
        }
        return elements.Count;
    }

    // Задание 4: Удалить неуникальные элементы
    public void RemoveNonUniqueElements4(ref Node head)
    {
        if (head == null)
        {
            outputHandler?.Invoke("Ошибка: список пуст. Операция невозможна.");
            return;
        }

        Dictionary<string, int> counts = new Dictionary<string, int>();
        Node current = head;

        while (current != null)
        {
            if (counts.ContainsKey(current.Data))
                counts[current.Data]++;
            else
                counts[current.Data] = 1;

            current = current.Next;
        }

        Node dummy = new Node("");
        dummy.Next = head;
        Node prev = dummy;
        current = head;

        bool removed = false;

        while (current != null)
        {
            if (counts[current.Data] > 1)
            {
                prev.Next = current.Next;
                removed = true;
            }
            else
            {
                prev = current;
            }
            current = current.Next;
        }

        head = dummy.Next;

        if (!removed)
        {
            outputHandler?.Invoke("Не найдено неуникальных элементов для удаления.");
        }
    }

    // Задание 5: Вставить список самого в себя после первого вхождения X
    public void InsertSelfAfterX5(ref Node head, string x)
    {
        if (head == null)
        {
            outputHandler?.Invoke("Ошибка: список пуст. Операция невозможна.");
            return;
        }

        if (string.IsNullOrEmpty(x))
        {
            outputHandler?.Invoke("Ошибка: значение X не задано.");
            return;
        }

        Node current = head;

        while (current != null)
        {
            if (current.Data == x)
            {
                Node selfCopy = CopyList(head);
                Node temp = current.Next;
                current.Next = selfCopy;

                // Переходим к концу скопированного списка
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = temp;
                return;
            }
            current = current.Next;
        }

        outputHandler?.Invoke($"Элемент '{x}' не найден в списке.");
    }

    // Вспомогательная функция для копирования списка
    private Node CopyList(Node head)
    {
        if (head == null)
            return null;

        Node newHead = new Node(head.Data);
        Node currentOld = head.Next;
        Node currentNew = newHead;

        while (currentOld != null)
        {
            currentNew.Next = new Node(currentOld.Data);
            currentOld = currentOld.Next;
            currentNew = currentNew.Next;
        }
        return newHead;
    }

    // Задание 6: Вставить элемент E в упорядоченный список
    public void InsertInOrder6(ref Node head, string E)
    {
        if (string.IsNullOrEmpty(E))
        {
            outputHandler?.Invoke("Ошибка: значение E не задано.");
            return;
        }

        if (!int.TryParse(E, out int eValue))
        {
            outputHandler?.Invoke($"Ошибка: не удалось преобразовать '{E}' в число.");
            return;
        }

        Node newNode = new Node(E);

        if (head == null)
        {
            head = newNode;
            return;
        }

        if (!int.TryParse(head.Data, out int headValue))
        {
            outputHandler?.Invoke($"Ошибка: не удалось преобразовать '{head.Data}' в число.");
            return;
        }

        if (eValue <= headValue)
        {
            newNode.Next = head;
            head = newNode;
            return;
        }

        Node current = head;

        while (current.Next != null)
        {
            if (!int.TryParse(current.Next.Data, out int nextValue))
            {
                outputHandler?.Invoke($"Ошибка: не удалось преобразовать '{current.Next.Data}' в число.");
                return;
            }

            if (eValue <= nextValue)
                break;

            current = current.Next;
        }

        newNode.Next = current.Next;
        current.Next = newNode;
    }

    // Задание 7: Удалить все элементы E
    public void RemoveAll7(ref Node head, string E)
    {
        if (head == null)
        {
            outputHandler?.Invoke("Ошибка: список пуст. Операция невозможна.");
            return;
        }

        if (string.IsNullOrEmpty(E))
        {
            outputHandler?.Invoke("Ошибка: значение E не задано.");
            return;
        }

        Node dummy = new Node("");
        dummy.Next = head;
        Node current = dummy;
        bool removed = false;

        while (current.Next != null)
        {
            if (current.Next.Data == E)
            {
                current.Next = current.Next.Next;
                removed = true;
            }
            else
            {
                current = current.Next;
            }
        }
        head = dummy.Next;

        if (!removed)
        {
            outputHandler?.Invoke($"Элемент '{E}' не найден в списке.");
        }
    }

    // Задание 8: Вставить элемент F перед первым вхождением E
    public void InsertBefore8(ref Node head, string F, string E)
    {
        if (string.IsNullOrEmpty(F) || string.IsNullOrEmpty(E))
        {
            outputHandler?.Invoke("Ошибка: значения F и/или E не заданы.");
            return;
        }

        Node newNode = new Node(F);

        if (head == null)
        {
            outputHandler?.Invoke("Ошибка: список пуст. Операция невозможна.");
            return;
        }

        if (head.Data == E)
        {
            newNode.Next = head;
            head = newNode;
            return;
        }

        Node current = head;

        while (current.Next != null && current.Next.Data != E)
        {
            current = current.Next;
        }

        if (current.Next != null)
        {
            newNode.Next = current.Next;
            current.Next = newNode;
        }
        else
        {
            outputHandler?.Invoke($"Элемент '{E}' не найден в списке.");
        }
    }

    // Задание 9: Дописать к списку L список E
    public void AppendList9(ref Node L, Node E)
    {
        if (L == null && E == null)
        {
            outputHandler?.Invoke("Ошибка: оба списка пусты. Операция невозможна.");
            return;
        }

        if (L == null)
        {
            L = E;
            return;
        }

        Node current = L;

        while (current.Next != null)
        {
            current = current.Next;
        }

        current.Next = E;
    }

    // Задание 10: Разбить список по первому вхождению X
    public (Node firstPart, Node secondPart) SplitByX10(Node head, string X)
    {
        if (head == null)
        {
            outputHandler?.Invoke("Ошибка: список пуст. Операция невозможна.");
            return (null, null);
        }

        if (string.IsNullOrEmpty(X))
        {
            outputHandler?.Invoke("Ошибка: значение X не задано.");
            return (head, null);
        }

        Node current = head;
        Node prev = null;

        while (current != null)
        {
            if (current.Data == X)
            {
                if (prev != null)
                {
                    prev.Next = null;
                }
                else
                {
                    head = null;
                }
                return (head, current);
            }
            prev = current;
            current = current.Next;
        }

        outputHandler?.Invoke($"Элемент '{X}' не найден в списке. Второй список будет пустым.");
        return (head, null);
    }

    // Задание 11: Удвоить список
    public void DuplicateList11(ref Node head)
    {
        if (head == null)
        {
            outputHandler?.Invoke("Ошибка: список пуст. Операция невозможна.");
            return;
        }

        Node current = head;

        while (current.Next != null)
        {
            current = current.Next;
        }

        Node copy = CopyList(head);
        current.Next = copy;
    }

    // Задание 12: Поменять местами два элемента списка
    public void SwapNodes12(ref Node head, string data1, string data2)
    {
        if (head == null)
        {
            outputHandler?.Invoke("Ошибка: список пуст. Операция невозможна.");
            return;
        }

        if (string.IsNullOrEmpty(data1) || string.IsNullOrEmpty(data2))
        {
            outputHandler?.Invoke("Ошибка: значения для обмена не заданы.");
            return;
        }

        if (data1 == data2)
        {
            outputHandler?.Invoke("Оба элемента имеют одинаковые значения. Обмен не требуется.");
            return;
        }

        Node prevNode1 = null, currNode1 = head;
        while (currNode1 != null && currNode1.Data != data1)
        {
            prevNode1 = currNode1;
            currNode1 = currNode1.Next;
        }

        Node prevNode2 = null, currNode2 = head;
        while (currNode2 != null && currNode2.Data != data2)
        {
            prevNode2 = currNode2;
            currNode2 = currNode2.Next;
        }

        if (currNode1 == null || currNode2 == null)
        {
            outputHandler?.Invoke("Один или оба элемента не найдены в списке.");
            return;
        }

        // Если currNode1 или currNode2 являются головой списка
        if (prevNode1 != null)
            prevNode1.Next = currNode2;
        else
            head = currNode2;

        if (prevNode2 != null)
            prevNode2.Next = currNode1;
        else
            head = currNode1;

        // Меняем next указатели
        (currNode1.Next, currNode2.Next) = (currNode2.Next, currNode1.Next);
    }
}