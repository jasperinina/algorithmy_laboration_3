namespace lab3.logic;

// Реализация операций со связным списком

public class LinkedListOperations
{
    // 1. Переворачивает список L
    public void ReverseList1(ref Node head)
    {
        Node prev = null, current = head, next = null;
        while (current != null)
        {
            next = current.Next;
            current.Next = prev;
            prev = current;
            current = next;
        }
        head = prev;
    }

    // 2. Переносит последний элемент в начало
    public void MoveLastToFront2(ref Node head)
    {
        if (head == null || head.Next == null)
            return;

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

    // 2. Переносит первый элемент в конец
    public void MoveFirstToEnd2(ref Node head)
    {
        if (head == null || head.Next == null)
            return;

        Node first = head;
        head = head.Next;
        first.Next = null;

        Node current = head;
        while (current.Next != null)
        {
            current = current.Next;
        }
        current.Next = first;
    }

    // 3. Определяет количество различных элементов в списке
    public int CountUniqueElements3(Node head)
    {
        var set = new HashSet<string>();
        Node current = head;
        while (current != null)
        {
            set.Add(current.Data);
            current = current.Next;
        }
        return set.Count;
    }

    // 4. Удаляет неуникальные элементы
    public void RemoveNonUnique4(ref Node head)
    {
        var countMap = new Dictionary<string, int>();
        Node current = head;
        while (current != null)
        {
            if (!countMap.ContainsKey(current.Data))
                countMap[current.Data] = 0;
            countMap[current.Data]++;
            current = current.Next;
        }

        Node dummy = new Node("");
        dummy.Next = head;
        Node prev = dummy;
        current = head;

        while (current != null)
        {
            if (countMap[current.Data] > 1)
            {
                prev.Next = current.Next;
            }
            else
            {
                prev = current;
            }
            current = current.Next;
        }

        head = dummy.Next;
    }

    // 5. Вставляет список самого в себя после первого вхождения элемента x
    public void InsertSelfAfterX5(ref Node head, string x)
    {
        Node current = head;
        while (current != null && current.Data != x)
        {
            current = current.Next;
        }

        if (current == null)
            return; // x не найден

        Node copy = CloneList(head);
        Node temp = current.Next;
        current.Next = copy;

        while (copy.Next != null)
        {
            copy = copy.Next;
        }
        copy.Next = temp;
    }

    // 6. Вставляет новый элемент E в упорядоченный по не убыванию список
    public void InsertInOrder6(ref Node head, string data)
    {
        Node newNode = new Node(data);
        if (head == null || string.Compare(head.Data, data) >= 0)
        {
            newNode.Next = head;
            head = newNode;
            return;
        }

        Node current = head;
        while (current.Next != null && string.Compare(current.Next.Data, data) < 0)
        {
            current = current.Next;
        }

        newNode.Next = current.Next;
        current.Next = newNode;
    }

    // 7. Удаляет все элементы E из списка
    public void RemoveAll7(ref Node head, string data)
    {
        Node dummy = new Node("");
        dummy.Next = head;
        Node prev = dummy, current = head;

        while (current != null)
        {
            if (current.Data == data)
            {
                prev.Next = current.Next;
            }
            else
            {
                prev = current;
            }
            current = current.Next;
        }

        head = dummy.Next;
    }

    // 8. Вставляет новый элемент F перед первым вхождением E
    public void InsertBefore8(ref Node head, string f, string e)
    {
        Node newNode = new Node(f);
        if (head == null)
            return;

        if (head.Data == e)
        {
            newNode.Next = head;
            head = newNode;
            return;
        }

        Node current = head;
        while (current.Next != null && current.Next.Data != e)
        {
            current = current.Next;
        }

        if (current.Next != null)
        {
            newNode.Next = current.Next;
            current.Next = newNode;
        }
    }

    // 9. Дописывает к списку L список E
    public void AppendList9(ref Node l, Node e)
    {
        if (l == null)
        {
            l = CloneList(e);
            return;
        }

        Node current = l;
        while (current.Next != null)
        {
            current = current.Next;
        }

        current.Next = CloneList(e);
    }

    // 10. Разбивает список по первому вхождению числа x
    public (Node, Node) SplitByX10(Node head, string x)
    {
        Node dummy = new Node("");
        dummy.Next = head;
        Node prev = dummy, current = head;

        while (current != null && current.Data != x)
        {
            prev = current;
            current = current.Next;
        }

        if (current == null)
        {
            return (head, null);
        }

        prev.Next = null;
        return (head, current);
    }

    // 11. Удваивает список
    public void DoubleList11(ref Node head)
    {
        if (head == null)
            return;

        Node copy = CloneList(head);
        Node current = head;
        while (current.Next != null)
        {
            current = current.Next;
        }
        current.Next = copy;
    }

    // 12. Меняет местами два элемента списка
    public void SwapNodes12(ref Node head, string data1, string data2)
    {
        if (data1 == data2)
            return;

        Node prev1 = null, curr1 = head;
        while (curr1 != null && curr1.Data != data1)
        {
            prev1 = curr1;
            curr1 = curr1.Next;
        }

        Node prev2 = null, curr2 = head;
        while (curr2 != null && curr2.Data != data2)
        {
            prev2 = curr2;
            curr2 = curr2.Next;
        }

        if (curr1 == null || curr2 == null)
            return;

        if (prev1 != null)
        {
            prev1.Next = curr2;
        }
        else
        {
            head = curr2;
        }

        if (prev2 != null)
        {
            prev2.Next = curr1;
        }
        else
        {
            head = curr1;
        }

        (curr1.Next, curr2.Next) = (curr2.Next, curr1.Next);
    }

    // Клонирует список
    private Node CloneList(Node head)
    {
        if (head == null)
            return null;

        Node newHead = new Node(head.Data);
        Node current = newHead;
        Node original = head.Next;

        while (original != null)
        {
            current.Next = new Node(original.Data);
            current = current.Next;
            original = original.Next;
        }

        return newHead;
    }
}
