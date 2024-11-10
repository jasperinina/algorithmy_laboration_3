using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using lab3.logic;
using lab3.Utilities;

namespace lab3.Pages;

public partial class AlgorithmsPage : Page
{
    private MainWindow _mainWindow;
    private StackPanel dynamicPanel;
    // Поля для хранения ссылок на TextBox
    private TextBox inputBox1;
    private TextBox inputBox2;
    private TextBox inputBox;


    public AlgorithmsPage(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        AddAlgorithmsControls();
    }

    private void AddAlgorithmsControls()
    {
        StackPanel mainPanel = new StackPanel
        {
            Margin = new Thickness(0, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left
        };

        // Базовые элементы
        StackPanel basePanel = new StackPanel();
        TextBlock header1TextBlock = new TextBlock
        {
            Text = "Выберите операцию",
            HorizontalAlignment = HorizontalAlignment.Left,
            Style = (Style)_mainWindow.FindResource("HeaderTextBlockStyle")
        };

        ComboBox operationsComboBox = new ComboBox
        {
            Width = 360,
            Margin = new Thickness(0, 0, 0, 30),
            HorizontalAlignment = HorizontalAlignment.Left,
            Style = (Style)_mainWindow.FindResource("PopUp")
        };

        // Добавление элементов в ComboBox
        operationsComboBox.Items.Add("1 - Перевернуть список");
        operationsComboBox.Items.Add("2 - Перенести последний элемент в начало");
        operationsComboBox.Items.Add("2 - Перенести первый элемент в конец");
        operationsComboBox.Items.Add("3 - Подсчитать количество различных элементов");
        operationsComboBox.Items.Add("4 - Удалить неуникальные элементы");
        operationsComboBox.Items.Add("5 - Вставить список после первого вхождения X");
        operationsComboBox.Items.Add("6 - Вставить элемент в упорядоченный список");
        operationsComboBox.Items.Add("7 - Удалить все элементы E");
        operationsComboBox.Items.Add("8 - Вставить элемент F перед первым вхождением E");
        operationsComboBox.Items.Add("9 - Дописать список");
        operationsComboBox.Items.Add("10 - Разбить список по первому вхождению X");
        operationsComboBox.Items.Add("11 - Удвоить список");
        operationsComboBox.Items.Add("12 - Поменять местами два элемента списка");

        operationsComboBox.SelectionChanged += (s, e) => HandleOperationSelection(dynamicPanel, operationsComboBox.SelectedIndex);

        TextBlock headerTextBlock = new TextBlock
        {
            Text = "Введите параметры тестирования",
            HorizontalAlignment = HorizontalAlignment.Left,
            Style = (Style)_mainWindow.FindResource("HeaderTextBlockStyle")
        };

        TextBlock depthTextBlock = new TextBlock
        {
            Text = "Строка файла",
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 20, 0, 8),
            Style = (Style)_mainWindow.FindResource("TextBlockStyle")
        };

        TextBox fileContentTextBox = new TextBox
        {
            Name = "FileContent",
            Width = 360,
            Margin = new Thickness(0, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left,
            Style = (Style)_mainWindow.FindResource("RoundedTextBoxStyle")
        };

        Button overwriteFileButton = new Button
        {
            Content = "Перезаписать файл",
            Width = 360,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 20, 0, 0),
            Style = (Style)_mainWindow.FindResource("OverwriteFileButtonStyle")
        };
        overwriteFileButton.Click += (s, e) => OverwriteFileButtonAlgorithms_Click(this, new RoutedEventArgs());

        // Добавление базовых элементов
        basePanel.Children.Add(header1TextBlock);
        basePanel.Children.Add(operationsComboBox);
        basePanel.Children.Add(headerTextBlock);
        basePanel.Children.Add(depthTextBlock);
        basePanel.Children.Add(fileContentTextBox);
        basePanel.Children.Add(overwriteFileButton);

        // Панель для динамических элементов
        dynamicPanel = new StackPanel();

        // Добавление базовой и динамической панелей в основной
        mainPanel.Children.Add(basePanel);
        mainPanel.Children.Add(dynamicPanel);

        _mainWindow.PageContentControl.Content = mainPanel;
    }
    
    private void AddTextInputHorizontal(StackPanel panel, string labelText1, string labelText2)
    {
        StackPanel horizontalPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Margin = new Thickness(0, 20, 0, 0)
        };

        // Первая пара (TextBlock и TextBox)
        StackPanel verticalPanel1 = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(0, 0, 8, 0)
        };

        TextBlock label1 = new TextBlock
        {
            Text = labelText1,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 0, 0, 4),
            Style = (Style)_mainWindow.FindResource("TextBlockStyle")
        };

        // Используем поле inputBox1
        inputBox1 = new TextBox
        {
            Width = 170,
            Margin = new Thickness(0, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left,
            Style = (Style)_mainWindow.FindResource("RoundedTextBoxStyle")
        };

        verticalPanel1.Children.Add(label1);
        verticalPanel1.Children.Add(inputBox1);

        // Вторая пара (TextBlock и TextBox)
        StackPanel verticalPanel2 = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(0, 0, 0, 0)
        };

        TextBlock label2 = new TextBlock
        {
            Text = labelText2,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(10, 0, 0, 4),
            Style = (Style)_mainWindow.FindResource("TextBlockStyle")
        };

        // Используем поле inputBox2
        inputBox2 = new TextBox
        {
            Width = 170,
            Margin = new Thickness(10, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left,
            Style = (Style)_mainWindow.FindResource("RoundedTextBoxStyle")
        };

        verticalPanel2.Children.Add(label2);
        verticalPanel2.Children.Add(inputBox2);

        // Добавление вертикальных панелей в горизонтальную панель
        horizontalPanel.Children.Add(verticalPanel1);
        horizontalPanel.Children.Add(verticalPanel2);

        // Добавление горизонтальной панели в основную панель
        panel.Children.Add(horizontalPanel);
    }

    private void HandleOperationSelection(StackPanel panel, int selectedIndex)
    {
        // Очистка динамической панели
        panel.Children.Clear();

        // Обнуляем поля ввода
        inputBox = null;
        inputBox1 = null;
        inputBox2 = null;

        switch (selectedIndex)
        {
            case 5: // Вставляет список самого в себя после первого вхождения элемента x
                AddTextInput(panel, "Введите элемент X");
                break;
            case 6: // Вставляет новый элемент E в упорядоченный по не убыванию список
                AddTextInput(panel, "Введите элемент для вставки");
                break;
            case 7: // Удаляет все элементы E из списка
                AddTextInput(panel, "Введите элемент для удаления");            
                break;
            case 8: // Вставляет новый элемент F перед первым вхождением E
                AddTextInputHorizontal(panel, "Введите элемент F", "Введите элемент E");
                break;
            case 9: // Дописывает к списку L список E
                AddTextInput(panel, "Введите список через пробел");
                break;
            case 10: // Разбить список по первому вхождению X
                AddTextInput(panel, "Введите значение X");
                break;
            case 12: // Поменять местами два элемента списка
                AddTextInputHorizontal(panel, "Введите первый элемент", "Введите второй элемент");
                break;
            default:
                // Операции, которые не требуют ввода
                break;
        }
        
        // Добавление кнопки "Выполнить" после динамически добавленных элементов
        Button executeButton = new Button
        {
            Content = "Выполнить",
            Width = 360,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 20, 0, 0),
            Style = (Style)_mainWindow.FindResource("RoundedButtonStyle")
        };
        executeButton.Click += (s, e) => ExecuteAlgorithm(selectedIndex);
        panel.Children.Add(executeButton);
    }
    
    private void AddTextInput(StackPanel panel, string labelText)
    {
        TextBlock label = new TextBlock
        {
            Text = labelText,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 20, 0, 8),
            Style = (Style)_mainWindow.FindResource("TextBlockStyle")
        };

        // Используем поле inputBox
        inputBox = new TextBox
        {
            Width = 360,
            Margin = new Thickness(0, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left,
            Style = (Style)_mainWindow.FindResource("RoundedTextBoxStyle")
        };

        panel.Children.Add(label);
        panel.Children.Add(inputBox);
    }


    private void OverwriteFileButtonAlgorithms_Click(object sender, RoutedEventArgs e)
    {
        // Извлекаем текстовое поле из панели
        StackPanel panel = (StackPanel)_mainWindow.PageContentControl.Content;
        TextBox fileContentTextBox = panel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "FileContent");
        

        if (fileContentTextBox == null)
        {
            AppendTextToOutput("Не удалось найти текстовое поле для перезаписи файла.");
            return;
        }

        string newContent = fileContentTextBox.Text; // Получаем текст из TextBox
        if (string.IsNullOrWhiteSpace(newContent))
        {
            AppendTextToOutput("Нельзя перезаписать файл пустым содержимым.");
            return;
        }

        // Перезапись файла
        try
        {
            File.WriteAllText("inputAlgorithms.txt", newContent);
            AppendTextToOutput("Файл 'inputAlgorithms.txt' был успешно перезаписан.");
        }
        catch (Exception ex)
        {
            AppendTextToOutput($"Ошибка при перезаписи файла: {ex.Message}");
        }
    }
    
    private void ExecuteAlgorithm(int selectedIndex)
    {
        var fileHandler = new FileHandlerAlgorithms(AppendTextToOutput);
        Node head = fileHandler.ReadFile(); // Получение начального списка из файла

        if (head == null)
        {
            AppendTextToOutput("Ошибка: невозможно получить начальный список.");
            return;
        }

        var operations = new LinkedListOperations(AppendTextToOutput);

        // Извлечение данных из текстовых полей в панели
        var inputValues = GetInputValuesFromPanel();

        switch (selectedIndex)
        {
            case 0: // Перевернуть список
                AppendTextToOutput("1. Начальный список:");
                PrintList(head);
                operations.ReverseList1(ref head);
                AppendTextToOutput("1. Список был перевернут.");
                AppendTextToOutput("Обновленный список:");
                break;
            case 1: // Перенести последний элемент в начало
                AppendTextToOutput("2. Начальный список:");
                PrintList(head);
                operations.MoveLastToFirst2(ref head);
                AppendTextToOutput("2. Последний элемент перенесен в начало.");
                AppendTextToOutput("Обновленный список:");
                break;
            case 2: // Перенести первый элемент в конец
                AppendTextToOutput("2. Начальный список:");
                PrintList(head);
                operations.MoveFirstToLast2(ref head);
                AppendTextToOutput("2. Первый элемент перенесен в конец.");
                AppendTextToOutput("Обновленный список:");
                break;
            case 3: // Подсчитать количество различных элементов
                AppendTextToOutput("3. Начальный список:");
                PrintList(head);
                int count = operations.CountDistinctElements3(head);
                AppendTextToOutput($"3. Количество различных элементов: {count}");
                break;
            case 4: // Удалить неуникальные элементы
                AppendTextToOutput("4. Начальный список:");
                PrintList(head);
                operations.RemoveNonUniqueElements4(ref head);
                AppendTextToOutput("4. Неуникальные элементы удалены.");
                AppendTextToOutput("Обновленный список:");
                break;
            case 5: // Вставить список после первого вхождения X
                AppendTextToOutput("5. Начальный список:");
                PrintList(head);
                if (inputValues.Length > 0)
                {
                    operations.InsertSelfAfterX5(ref head, inputValues[0]);
                    AppendTextToOutput($"5. Список вставлен после первого вхождения элемента '{inputValues[0]}'.");
                    AppendTextToOutput("Обновленный список:");
                }
                break;
            case 6: // Вставить элемент в упорядоченный список
                AppendTextToOutput("6. Начальный список:");
                PrintList(head);
                if (inputValues.Length > 0)
                {
                    operations.InsertInOrder6(ref head, inputValues[0]);
                    AppendTextToOutput($"6. Элемент '{inputValues[0]}' вставлен в упорядоченный список.");
                    AppendTextToOutput("Обновленный список:");
                }
                break;
            case 7: // Удалить все элементы E
                AppendTextToOutput("7. Начальный список:");
                PrintList(head);
                if (inputValues.Length > 0)
                {
                    operations.RemoveAll7(ref head, inputValues[0]);
                    AppendTextToOutput($"7. Все элементы '{inputValues[0]}' удалены.");
                    AppendTextToOutput("Обновленный список:");
                }
                break;
            case 8: // Вставить элемент F перед первым вхождением E
                AppendTextToOutput("8. Начальный список:");
                PrintList(head);
                if (inputValues.Length > 1)
                {
                    operations.InsertBefore8(ref head, inputValues[0], inputValues[1]);
                    AppendTextToOutput($"8. Элемент '{inputValues[0]}' вставлен перед первым вхождением элемента '{inputValues[1]}'.");
                    AppendTextToOutput("Обновленный список:");
                }
                break;
            case 9: // Дописать список
                AppendTextToOutput("9. Начальный список:");
                PrintList(head);
                if (inputValues.Length > 0)
                {
                    Node additionalList = CreateListFromInput(inputValues[0]);
                    operations.AppendList9(ref head, additionalList);
                    AppendTextToOutput("9. Список дополнен вторым списком.");
                    AppendTextToOutput("Обновленный список:");
                }
                break;
            case 10: // Разбить список по первому вхождению X
                AppendTextToOutput("10. Начальный список:");
                PrintList(head);
                if (inputValues.Length > 0)
                {
                    (Node firstPart, Node secondPart) = operations.SplitByX10(head, inputValues[0]);
                    AppendTextToOutput($"10. Список разбит по первому вхождению элемента '{inputValues[0]}'.");
                    AppendTextToOutput("Первая часть списка:");
                    PrintList(firstPart);
                    AppendTextToOutput("Вторая часть списка:");
                    PrintList(secondPart);
                    return; // Выход из метода, чтобы не печатать обновленный список
                }
                break;
            case 11: // Удвоить список
                AppendTextToOutput("11. Начальный список:");
                PrintList(head);
                operations.DuplicateList11(ref head);
                AppendTextToOutput("11. Список удвоен.");
                AppendTextToOutput("Обновленный список:");
                break;
            case 12: // Поменять местами два элемента списка
                AppendTextToOutput("12. Начальный список:");
                PrintList(head);
                if (inputValues.Length > 1)
                {
                    operations.SwapNodes12(ref head, inputValues[0], inputValues[1]);
                    AppendTextToOutput($"12. Элементы '{inputValues[0]}' и '{inputValues[1]}' поменяны местами.");
                    AppendTextToOutput("Обновленный список:");
                }
                break;
            default:
                AppendTextToOutput("Выберите корректную операцию.");
                break;
        }
        
        PrintList(head);
    }
    
    private Node CreateListFromInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            AppendTextToOutput("Пустая строка, невозможно создать список.");
            return null;
        }

        string[] elements = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Используем существующий метод CreateLinkedList из FileHandlerAlgorithms
        var fileHandler = new FileHandlerAlgorithms(AppendTextToOutput);
        return fileHandler.CreateLinkedList(elements);
    }

    private string[] GetInputValuesFromPanel()
    {
        var values = new List<string>();

        if (inputBox1 != null)
            values.Add(inputBox1.Text);

        if (inputBox2 != null)
            values.Add(inputBox2.Text);

        // Также обработаем случай с одним TextBox (если он есть)
        if (inputBox != null)
            values.Add(inputBox.Text);

        return values.ToArray();
    }

    
    private void PrintList(Node head)
    {
        Node current = head;
        while (current != null)
        {
            AppendTextToOutput(current.Data);
            current = current.Next;
        }
    }
    
    private void AppendTextToOutput(string text)
    {
        OutputTextBox.AppendText(text + Environment.NewLine);
        OutputTextBox.ScrollToEnd(); // Прокрутка вниз к последней строке
    }

}