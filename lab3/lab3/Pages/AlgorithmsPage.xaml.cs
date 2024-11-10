using System.Windows;
using System.Windows.Controls;

namespace lab3.Pages;

public partial class AlgorithmsPage : Page
{
    private MainWindow _mainWindow;
    public AlgorithmsPage(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
    }
    
    private void AddAlgorithmsControls()
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

        //operationsComboBox.SelectionChanged += OperationsComboBox_SelectionChanged;
        
        panel.Children.Add(header1TextBlock);
        panel.Children.Add(operationsComboBox);

        _mainWindow.PageContentControl.Content = panel;
    }
    
}