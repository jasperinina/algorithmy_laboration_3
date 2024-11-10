using System.Windows.Controls;

namespace lab3.Pages;

public partial class QueuePage : Page
{
    private MainWindow _mainWindow;
    public QueuePage(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
    }
    
}