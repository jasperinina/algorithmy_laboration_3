using System.Windows;
using lab3.Pages;
using lab3.Utilities;

namespace lab3;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    // Переход на страницу со стеком
    private void StackRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        // Очистить динамически добавленные элементы на текущей странице
        if (MainFrame.Content is StackPage stackPage)
        {
            stackPage.ClearDynamicElements();
        }
        MainFrame.Navigate(new StackPage(this));
    }

    // Переход на страницу с очередью
    private void QueueRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        // Очистить динамически добавленные элементы на текущей странице
        if (MainFrame.Content is QueuePage queuePage)
        {
            queuePage.ClearDynamicElements();
        }
            
        MainFrame.Navigate(new QueuePage(this));
    }
}