namespace lab3.logic;

public class InfixToPostfixConverter
{
    public string ConvertToPostfix(string infix)
    {
        var output = new List<string>();
        var operators = new Stack<string>();
        var tokens = infix.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in tokens)
        {
            if (double.TryParse(token, out _))
            {
                // Числа сразу добавляются в выходную строку
                output.Add(token);
            }
            else if (IsFunction(token))
            {
                operators.Push(token);
            }
            else if (IsOperator(token))
            {
                while (operators.Count > 0 && Precedence(operators.Peek()) >= Precedence(token))
                {
                    output.Add(operators.Pop());
                }
                operators.Push(token);
            }
            else if (token == "(")
            {
                operators.Push(token);
            }
            else if (token == ")")
            {
                while (operators.Count > 0 && operators.Peek() != "(")
                {
                    output.Add(operators.Pop());
                }
                if (operators.Count == 0 || operators.Pop() != "(")
                {
                    throw new InvalidOperationException("Несоответствующие скобки.");
                }
                // Если верх стека - функция, перемещаем её в выходную строку
                if (operators.Count > 0 && IsFunction(operators.Peek()))
                {
                    output.Add(operators.Pop());
                }
            }
            else
            {
                throw new InvalidOperationException($"Неизвестный токен: {token}");
            }
        }

        // Выгружаем оставшиеся операторы в выходную строку
        while (operators.Count > 0)
        {
            var op = operators.Pop();
            if (op == "(" || op == ")")
            {
                throw new InvalidOperationException("Несоответствующие скобки.");
            }
            output.Add(op);
        }

        return string.Join(" ", output);
    }

    private bool IsFunction(string token)
    {
        return token == "ln" || token == "cos" || token == "sin" || token == "sqrt";
    }

    private bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
    }

    private int Precedence(string op)
    {
        return op switch
        {
            "+" or "-" => 1,
            "*" or "/" => 2,
            "^" => 3,
            "ln" or "cos" or "sin" or "sqrt" => 4, // Высокий приоритет для функций
            _ => 0
        };
    }

}