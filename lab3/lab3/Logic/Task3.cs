namespace lab3.Logic;

public class Task3
{
    //Метод для нахождения наибольшей возростающей последовательности элементов в списке
    public static List<int> FindLIS(List<int> input_list)
    {
        if (input_list.Count == 0) return new List<int>();

        List<int> lengths = new List<int>(new int[input_list.Count]);
        List<int> previous = new List<int>(new int[input_list.Count]);
        int maxLength = 1;
        int bestEnd = 0;

        lengths[0] = 1;
        previous[0] = -1;

        for (int i = 1; i < input_list.Count; i++)
        {
            lengths[i] = 1;
            previous[i] = -1;

            for (int j = i - 1; j >= 0; j--)
            {
                if (input_list[j] < input_list[i] && lengths[j] + 1 > lengths[i])
                {
                    lengths[i] = lengths[j] + 1;
                    previous[i] = j;
                }
            }

            if (lengths[i] > maxLength)
            {
                maxLength = lengths[i];
                bestEnd = i;
            }
        }

        List<int> output_list = new List<int>();
        for (int i = bestEnd; i >= 0; i = previous[i])
        {
            output_list.Insert(0, input_list[i]);
        }

        return output_list;
    }
    //Метод для удаления дубликатов в строке при помощи стека
    public static string RemoveDuplicates(string input)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char c in input)
        {
            if (stack.Count == 0 || stack.Peek() != c)
            {
                stack.Push(c);
            }
        }

        char[] result = new char[stack.Count];
        for (int i = stack.Count - 1; i >= 0; i--)
        {
            result[i] = stack.Pop();
        }

        return new string(result);
    }
    //Метод для нахождения n-ого члена последовательности Фибоначчи при помощи очереди
    public static int FindFibonacci(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 1;

        Queue<int> queue = new Queue<int>();

        queue.Enqueue(0);
        queue.Enqueue(1);

        for (int i = 2; i <= n; i++)
        {
            int a = queue.Dequeue();
            int b = queue.Peek();

            queue.Enqueue(a + b);
        }

        // Извлекаем предпоследний элемент, так как последний элемент в очереди - это (n+1)-й элемент
        queue.Dequeue();

        return queue.Peek();
    }

    //Метод для проверки содержания элемента в бинарном дереве
    public static bool ContainsElement(TreeNode root, string target)
    {
        if (root == null)
        {
            return false;
        }

        // Сравниваем текущий узел с искомым значением
        if (root.Data.Equals(target))
        {
            return true;
        }

        // Рекурсивно ищем в левом поддереве
        if (ContainsElement(root.Left, target))
        {
            return true;
        }

        // Рекурсивно ищем в правом поддереве
        return ContainsElement(root.Right, target);
    }
}