using System.Windows;
using System.Windows.Controls;
using lab3.Utilities;
using lab3.Utilities.QueueAnalyzer;

namespace lab3.Pages;

public partial class QueuePage : Page
{
    private MainWindow _mainWindow;
    private FileHandlerQueue fileHandler;
    public QueuePage(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        fileHandler = new FileHandlerQueue(AppendTextToOutput);
        AddQueueControls();
    }
    
    private void AddQueueControls()
    {
        StackPanel panel = new StackPanel
        {
            Margin = new Thickness(0, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left
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
        overwriteFileButton.Click += (s, e) => OverwriteFileButton_Click(fileContentTextBox);
        
        Button readFileButton = new Button
        {
            Content = "Прочитать файл",
            Width = 360,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 172, 0, 0),
            Style = (Style)_mainWindow.FindResource("RoundedButtonStyle")
        };
        readFileButton.Click += ReadFileButton_Click;
        
        Button graphButton1 = new Button
        {
            Content = "График зависимости (Различная длина)",
            Width = 360,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 20, 0, 0),
            Style = (Style)_mainWindow.FindResource("RoundedButtonGraphStyle")
        };
        graphButton1.Click += Graph_Click;

        Button graphButton2 = new Button
        {
            Content = "График зависимости (Различный состав)",
            Width = 360,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 20, 0, 0),
            Style = (Style)_mainWindow.FindResource("RoundedButtonGraphStyle")
        };
        graphButton2.Click += GraphDifferentComposition_Click;
        
        panel.Children.Add(headerTextBlock);
        panel.Children.Add(depthTextBlock);
        panel.Children.Add(fileContentTextBox);
        panel.Children.Add(overwriteFileButton);
        panel.Children.Add(readFileButton);
        panel.Children.Add(graphButton1);
        panel.Children.Add(graphButton2);

        _mainWindow.PageContentControl.Content = panel;
    }
    
    private void OverwriteFileButton_Click(TextBox inputBox)
    {
        if (inputBox == null)
        {
            AppendTextToOutput("Не удалось найти текстовое поле для перезаписи файла.");
            return;
        }

        string newContent = inputBox.Text;
        if (string.IsNullOrWhiteSpace(newContent))
        {
            AppendTextToOutput("Нельзя перезаписать файл пустым содержимым.");
            return;
        }

        fileHandler.OverwriteFile(newContent);
    }

    private void ReadFileButton_Click(object sender, RoutedEventArgs e)
    {
        fileHandler.ReadFile();
    }

    private void Graph_Click(object sender, RoutedEventArgs e)
    {
        string filePath = "inputQueue_Test.txt"; // Путь к подготовленному файлу
        var analyzer = new QueuePerformanceAnalyzer(filePath);
        var (dataSizes, times) = analyzer.AnalyzePerformance();

        if (dataSizes.Length > 0 && times.Length > 0)
        {
            GraphQueue graphWindow = new GraphQueue(dataSizes, times);
            graphWindow.Title = "График: Различные по длине операции";
            graphWindow.Show();
        }
        else
        {
            // Логика, если данных для графика нет
            MessageBox.Show("Нет данных для построения графика.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void GraphDifferentComposition_Click(object sender, RoutedEventArgs e)
    {
        string filePath = "inputQueueTest_DifferentContent.txt"; // Путь к тестовому файлу с различным составом операций
    
        // Генерация данных
        new QueueDifferentContentAnalyzer(filePath);

        var analyzer = new QueuePerformanceAnalyzer(filePath);
        var (dataSizes, times) = analyzer.AnalyzePerformance();

        // Пример меток для графика
        string[] labels = new string[dataSizes.Length];
        for (int i = 0; i < labels.Length; i++)
        {
            labels[i] = i % 2 == 0 ? "Тяжелые операции" : "Легкие операции"; // Чередуем метки
        }

        if (dataSizes.Length > 0 && times.Length > 0)
        {
            GraphQueueDifferentComposition graphWindow = new GraphQueueDifferentComposition(labels, times);
            graphWindow.Title = "График: Одинаковая длина, различный состав операций";
            graphWindow.Show();
        }
        else
        {
            // Логика, если данных для графика нет
            MessageBox.Show("Нет данных для построения графика.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    private void AppendTextToOutput(string text)
    {
        OutputTextBox.AppendText(text + Environment.NewLine);
        OutputTextBox.ScrollToEnd(); // Прокрутка вниз к последней строке
    }
}