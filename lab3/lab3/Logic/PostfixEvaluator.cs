namespace lab3.logic;

public class PostfixEvaluator
{
    private readonly StackModel stack;

    public PostfixEvaluator()
    {
        stack = new StackModel(elem => { }); // Использование нашего существующего стека
    }

    public double Evaluate(string expression)
    {
        string[] tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in tokens)
        {
            if (double.TryParse(token, out double number))
            {
                stack.Push(number.ToString()); // Преобразование числа в строку для вашего стека
            }
            else
            {
                // Выполнение операций с использованием существующего стека
                if (stack.IsEmpty())
                {
                    throw new InvalidOperationException("Недостаточно операндов для операции.");
                }

                double result;
                switch (token)
                {
                    case "+":
                        double b = Convert.ToDouble(stack.Pop());
                        double a = Convert.ToDouble(stack.Pop());
                        result = a + b;
                        break;
                    case "-":
                        b = Convert.ToDouble(stack.Pop());
                        a = Convert.ToDouble(stack.Pop());
                        result = a - b;
                        break;
                    case "*":
                        b = Convert.ToDouble(stack.Pop());
                        a = Convert.ToDouble(stack.Pop());
                        result = a * b;
                        break;
                    case "/":
                        b = Convert.ToDouble(stack.Pop());
                        if (b == 0)
                            throw new DivideByZeroException("Деление на ноль.");
                        a = Convert.ToDouble(stack.Pop());
                        result = a / b;
                        break;
                    case "^":
                        b = Convert.ToDouble(stack.Pop());
                        a = Convert.ToDouble(stack.Pop());
                        result = Math.Pow(a, b);
                        break;
                    case "ln":
                        a = Convert.ToDouble(stack.Pop());
                        result = Math.Log(a);
                        break;
                    case "cos":
                        a = Convert.ToDouble(stack.Pop());
                        result = Math.Cos(a);
                        break;
                    case "sin":
                        a = Convert.ToDouble(stack.Pop());
                        result = Math.Sin(a);
                        break;
                    case "sqrt":
                        a = Convert.ToDouble(stack.Pop());
                        result = Math.Sqrt(a);
                        break;
                    default:
                        throw new InvalidOperationException($"Неизвестная операция: {token}");
                }

                stack.Push(result.ToString()); // Результат операции добавляется обратно в стек
            }
        }

        if (stack.IsEmpty() || stack.Top() == null)
        {
            throw new InvalidOperationException("Неверное выражение.");
        }

        return Convert.ToDouble(stack.Pop()); 
    }
}