using System.IO;
using System.Windows;
using System.Windows.Controls;
using lab3.Utilities;
using lab3.Utilities.StackAnalyzer;

namespace lab3.Pages;

public partial class StackPage : Page
{
    private MainWindow _mainWindow;
    private string _selectedOperation = string.Empty;
    public StackPage(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        AddStackControls();
    }

    private void AddStackControls()
    {
        StackPanel panel = new StackPanel();
        { 
            Margin = new Thickness(0, 30, 0, 0); 
            HorizontalAlignment = HorizontalAlignment.Left;
        };
        
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
        operationsComboBox.Items.Add("Операции со стеком");
        operationsComboBox.Items.Add("Счет постфиксной записи");
        operationsComboBox.Items.Add("Перевод в постфиксную запись");
        operationsComboBox.SelectionChanged += OperationsComboBox_SelectionChanged;
        
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
        overwriteFileButton.Click += (s, e) => HandleButtonClick("Перезаписать файл");
        
        
        Button readFileButton = new Button
        {
            Content = "Прочитать файл",
            Width = 360,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 180, 0, 0),
            Style = (Style)_mainWindow.FindResource("RoundedButtonStyle")
        };
        readFileButton.Click += (s, e) => HandleButtonClick("Прочитать файл");
        
        Button graphButton = new Button
        {
            Content = "График зависимости",
            Width = 360,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 20, 0, 0),
            Style = (Style)_mainWindow.FindResource("RoundedButtonGraphStyle")
        };
        graphButton.Click += (s, e) => HandleButtonClick("График зависимости");
        
        panel.Children.Add(header1TextBlock);
        panel.Children.Add(operationsComboBox);
        panel.Children.Add(headerTextBlock); 
        panel.Children.Add(depthTextBlock);
        panel.Children.Add(fileContentTextBox);
        panel.Children.Add(overwriteFileButton);
        panel.Children.Add(readFileButton);
        panel.Children.Add(graphButton);

        _mainWindow.PageContentControl.Content = panel;
    }
    
    private void OperationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBox comboBox = sender as ComboBox;
        if (comboBox != null && comboBox.SelectedItem != null)
        {
            _selectedOperation = comboBox.SelectedItem.ToString();
        }
    }
    
    private void HandleButtonClick(string buttonName)
    {
        switch (_selectedOperation)
        {
            case "Операции со стеком":
                if (buttonName == "Перезаписать файл")
                {
                    OverwriteFileButtonStack_Click(this, new RoutedEventArgs());
                }
                else if (buttonName == "Прочитать файл")
                {
                    ReadFileButtonStack_Click(this, new RoutedEventArgs());
                }
                else if (buttonName == "График зависимости")
                {
                    GraphStack_Click(this, new RoutedEventArgs());
                }
                break;

            case "Счет постфиксной записи":
                if (buttonName == "Перезаписать файл")
                {
                    OverwriteFileButtonPostfix_Click(this, new RoutedEventArgs());
                }
                else if (buttonName == "Прочитать файл")
                {
                    ReadFileButtonPostfix_Click(this, new RoutedEventArgs());
                }
                else if (buttonName == "График зависимости")
                {
                    GraphPostfix_Click(this, new RoutedEventArgs());
                }
                break;

            case "Перевод в постфиксную запись":
                if (buttonName == "Перезаписать файл")
                {
                    OverwriteFileButtonConversion_Click(this, new RoutedEventArgs());
                }
                else if (buttonName == "Прочитать файл")
                {
                    ReadFileButtonConversion_Click(this, new RoutedEventArgs());
                }
                else if (buttonName == "График зависимости")
                {
                    GraphConversion_Click(this, new RoutedEventArgs());
                }
                break;

            default:
                MessageBox.Show("Выберите операцию из списка.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                break;
        }
    }
    
    // Метод для очистки динамических элементов
    public void ClearDynamicElements()
    { 
        _mainWindow.PageContentControl.Content = null;
    }
    
    private void GraphStack_Click(object sender, RoutedEventArgs e)
    {
        string filePath = "inputStackTest.txt"; // Путь к подготовленному файлу
        var analyzer = new PerformanceAnalyzer(filePath);
        var (dataSizes, times) = analyzer.AnalyzePerformance();

        if (dataSizes.Length > 0 && times.Length > 0)
        {
            GraphStack graphWindow = new GraphStack(dataSizes, times);
            graphWindow.Show();
        }
        else
        {
            // Можно добавить логику, если данных для графика нет (например, показать сообщение)
            MessageBox.Show("Нет данных для построения графика.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void GraphPostfix_Click(object sender, RoutedEventArgs e)
    {
        string filePath = "inputPostfixTest.txt"; // Путь к подготовленному файлу
        var analyzer = new PostfixPerformanceAnalyzer(filePath);
        var (expressionLengths, times) = analyzer.AnalyzePerformance();

        if (expressionLengths.Length > 0 && times.Length > 0)
        {
            CraphPostfix graphWindow = new CraphPostfix(expressionLengths, times);
            graphWindow.Show();
        }
        else
        {
            // Можно добавить логику, если данных для графика нет (например, показать сообщение)
            MessageBox.Show("Нет данных для построения графика.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void GraphConversion_Click(object sender, RoutedEventArgs e)
    {
        string filePath = "inputConversionPostfixTest.txt"; // Путь к подготовленному файлу
        var analyzer = new InfixToPostfixPerformanceAnalyzer(filePath);
        var (expressionLengths, times) = analyzer.AnalyzePerformance();

        if (expressionLengths.Length > 0 && times.Length > 0)
        {
            CraphPostfixConverter graphWindow = new CraphPostfixConverter(expressionLengths, times);
            graphWindow.Show();
        }
        else
        {
            // Вывод сообщения, если данных для построения графика нет
            MessageBox.Show("Нет данных для построения графика.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void ReadFileButtonStack_Click(object sender, RoutedEventArgs e)
    {
        // Создание экземпляра FileHandlerStack с передачей метода для вывода в TextBox
        var fileHandlerStack = new FileHandlerStack(AppendTextToOutput);
        fileHandlerStack.ReadFile();
    }
    
    private void OverwriteFileButtonStack_Click(object sender, RoutedEventArgs e)
    {
        // Извлекаем текстовое поле из PageContentControl
        StackPanel panel = (StackPanel)_mainWindow.PageContentControl.Content;
        TextBox fileContentTextBox = panel.Children.OfType<TextBox>().FirstOrDefault();

        if (fileContentTextBox == null)
        {
            AppendTextToOutput("Не удалось найти текстовое поле для перезаписи файла.");
            return;
        }

        string newContent = fileContentTextBox.Text; // Используем найденный TextBox для получения текста
        if (string.IsNullOrWhiteSpace(newContent))
        {
            AppendTextToOutput("Нельзя перезаписать файл пустым содержимым.");
            return;
        }

        var fileHandler = new FileHandlerStack(AppendTextToOutput);
        fileHandler.OverwriteFile(newContent);
    }
    
    private void ReadFileButtonPostfix_Click(object sender, RoutedEventArgs e)
    {
        // Создание экземпляра FileHandlerPostfix с передачей метода для вывода в TextBox
        var fileHandlerPostfix = new FileHandlerPostfix(AppendTextToOutput);
        fileHandlerPostfix.ReadFile();
    }

    private void OverwriteFileButtonPostfix_Click(object sender, RoutedEventArgs e)
    {
        // Извлекаем текстовое поле из PageContentControl
        StackPanel panel = (StackPanel)_mainWindow.PageContentControl.Content;
        TextBox fileContentTextBox = panel.Children.OfType<TextBox>().FirstOrDefault();

        if (fileContentTextBox == null)
        {
            AppendTextToOutput("Не удалось найти текстовое поле для перезаписи файла.");
            return;
        }

        string newContent = fileContentTextBox.Text; // Используем найденный TextBox для получения текста
        if (string.IsNullOrWhiteSpace(newContent))
        {
            AppendTextToOutput("Нельзя перезаписать файл пустым содержимым.");
            return;
        }

        try
        {
            File.WriteAllText("inputPostfix.txt", newContent); // Перезапись файла с новым содержимым
            AppendTextToOutput($"Файл 'inputPostfix.txt' успешно перезаписан.");
        }
        catch (Exception ex)
        {
            AppendTextToOutput($"Ошибка при перезаписи файла: {ex.Message}");
        }
    }
    
    private void ReadFileButtonConversion_Click(object sender, RoutedEventArgs e)
    {
        // Создание экземпляра FileHandlerInfixToPostfix с передачей метода для вывода в TextBox
        var fileHandler = new FileHandlerInfixToPostfix(AppendTextToOutput);
        fileHandler.ReadFile();
    }

    private void OverwriteFileButtonConversion_Click(object sender, RoutedEventArgs e)
    {
        // Извлекаем текстовое поле из PageContentControl
        StackPanel panel = (StackPanel)_mainWindow.PageContentControl.Content;
        TextBox fileContentTextBox = panel.Children.OfType<TextBox>().FirstOrDefault();

        if (fileContentTextBox == null)
        {
            AppendTextToOutput("Не удалось найти текстовое поле для перезаписи файла.");
            return;
        }

        string newContent = fileContentTextBox.Text; // Используем найденный TextBox для получения текста
        if (string.IsNullOrWhiteSpace(newContent))
        {
            AppendTextToOutput("Нельзя перезаписать файл пустым содержимым.");
            return;
        }

        try
        {
            File.WriteAllText("inputConversionPostfix.txt", newContent); // Перезапись файла с новым содержимым
            AppendTextToOutput($"Файл 'inputConversionPostfix.txt' успешно перезаписан.");
        }
        catch (Exception ex)
        {
            AppendTextToOutput($"Ошибка при перезаписи файла: {ex.Message}");
        }
    }

    private void AppendTextToOutput(string text)
    {
        OutputTextBox.AppendText(text + Environment.NewLine);
        OutputTextBox.ScrollToEnd(); // Прокрутка вниз к последней строке
    }
}