using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using lab3.Logic;

namespace lab3.Pages;
public partial class TreePage : Page
{
    private MainWindow _mainWindow;
    public TreePage(MainWindow mainWindow)
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
        Button CreateTree = new Button
        {
            Content = "Прямой обход дерева",
            Width = 360,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 20, 0, 0),
            Style = (Style)_mainWindow.FindResource("RoundedButtonGraphStyle")
        };
        CreateTree.Click += (s, e) => CreateTreeButton_Click(s, e);
        panel.Children.Add(CreateTree);

        _mainWindow.PageContentControl.Content = panel;
    }
    private void CreateTreeButton_Click(object sender, RoutedEventArgs e)
    {
        // Создание предопределенного дерева
        TreeNode root = new TreeNode("A");
        root.Left = new TreeNode("B");
        root.Left.Left = new TreeNode("D");
        root.Left.Left.Right = new TreeNode("G");
        root.Right = new TreeNode("C");
        root.Right.Left = new TreeNode("E");
        root.Right.Right = new TreeNode("F");
        root.Right.Right.Left = new TreeNode("H");
        root.Right.Right.Right = new TreeNode("J");

        BinaryTree tree = new BinaryTree();

        // Вывод дерева в текстовое поле
        OutputTextBox.Text = tree.PreorderPrint(root);
    }
}
