using System.Windows;
using System.Windows.Controls;
using lab3.Logic;

namespace lab3.Pages;
public partial class TreePage : Page
{
    private MainWindow _mainWindow;
    private TreeNode root;
    private BinaryTree tree;

    public TreePage(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;

        // Создание предопределенного дерева
        CreateTree();

        AddTreeControls();
    }
    private void AddTreeControls()
    {
        StackPanel panel = new StackPanel();
        {
            Margin = new Thickness(0, 0, 0, 0);
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
            Margin = new Thickness(0, 8, 0, 30),
            HorizontalAlignment = HorizontalAlignment.Left,
            Style = (Style)_mainWindow.FindResource("PopUp")
        };
        operationsComboBox.Items.Add("Прямой обход дерева");
        operationsComboBox.Items.Add("Симметричный обход дерева");
        operationsComboBox.Items.Add("Обратный обход дерева");

        Button executeButton = new Button
        {
            Content = "Выполнить",
            Width = 360,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 352, 0, 0),
            Style = (Style)_mainWindow.FindResource("RoundedButtonStyle")
        };
        executeButton.Click += (s, e) => ExecuteButton_Click(operationsComboBox);

        panel.Children.Add(header1TextBlock);
        panel.Children.Add(operationsComboBox);
        panel.Children.Add(executeButton);

        _mainWindow.PageContentControl.Content = panel;
    }
    
    private void ExecuteButton_Click(ComboBox operationsComboBox)
    {
        string selectedOperation = operationsComboBox.SelectedItem as string;
        if (selectedOperation == null)
        {
            AppendTextToOutput("Пожалуйста, выберите операцию.");
            return;
        }

        if (tree == null || root == null)
        {
            AppendTextToOutput("Дерево не было загружено или создано.");
            return;
        }

        try
        {
            switch (selectedOperation)
            {
                case "Прямой обход дерева":
                    OutputTextBox.Text = tree.PreorderPrint(root);
                    break;
                case "Симметричный обход дерева":
                    OutputTextBox.Text = tree.InorderPrint(root);
                    break;
                case "Обратный обход дерева":
                    OutputTextBox.Text = tree.PostorderPrint(root);
                    break;
                default:
                    AppendTextToOutput("Выбрана неизвестная операция.");
                    break;
            }
        }
        catch (Exception ex)
        {
            AppendTextToOutput($"Ошибка при выполнении операции: {ex.Message}");
        }
    }
    private void CreateTree()
    {
        // Создание предопределенного дерева
        root = new TreeNode("A");
        root.Left = new TreeNode("B");
        root.Left.Left = new TreeNode("D");
        root.Left.Left.Right = new TreeNode("G");
        root.Right = new TreeNode("C");
        root.Right.Left = new TreeNode("E");
        root.Right.Right = new TreeNode("F");
        root.Right.Right.Left = new TreeNode("H");
        root.Right.Right.Right = new TreeNode("J");

        tree = new BinaryTree();
    }
    
    private void AppendTextToOutput(string text)
    {
        OutputTextBox.AppendText(text + Environment.NewLine);
        OutputTextBox.ScrollToEnd(); // Прокрутка вниз к последней строке
    }
}