using System.Windows;
using System.Windows.Controls;
using lab3.Utilities;
using lab3.Utilities.StackAnalyzer;

namespace lab3.Pages;

public partial class StackPage : Page
{
    private MainWindow _mainWindow;
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
        overwriteFileButton.Click += OverwriteFileButton_Click;
        
        
        Button readFileButton = new Button
        {
            Content = "Прочитать файл",
            Width = 360,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 300, 0, 0),
            Style = (Style)_mainWindow.FindResource("RoundedButtonStyle")
        };
        readFileButton.Click += ReadFileButton_Click;
        
        Button graphButton = new Button
        {
            Content = "График зависимости",
            Width = 360,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 20, 0, 0),
            Style = (Style)_mainWindow.FindResource("RoundedButtonGraphStyle")
        };
        graphButton.Click += Graph_Click;
        
        panel.Children.Add(headerTextBlock); 
        panel.Children.Add(depthTextBlock);
        panel.Children.Add(fileContentTextBox);
        panel.Children.Add(overwriteFileButton);
        panel.Children.Add(readFileButton);
        panel.Children.Add(graphButton);

        _mainWindow.PageContentControl.Content = panel;
    }
    
    // Метод для очистки динамических элементов
    public void ClearDynamicElements()
    { 
        _mainWindow.PageContentControl.Content = null;
    }
    
    private void Graph_Click(object sender, RoutedEventArgs e)
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
    
    private void ReadFileButton_Click(object sender, RoutedEventArgs e)
    {
        // Создание экземпляра FileHandlerStack с передачей метода для вывода в TextBox
        var fileHandlerStack = new FileHandlerStack(AppendTextToOutput);
        fileHandlerStack.ReadFile();
    }
    
    private void OverwriteFileButton_Click(object sender, RoutedEventArgs e)
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

    private void AppendTextToOutput(string text)
    {
        OutputTextBox.AppendText(text + Environment.NewLine);
        OutputTextBox.ScrollToEnd(); // Прокрутка вниз к последней строке
    }
}