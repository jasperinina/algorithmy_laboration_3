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

        AddStackControls();
    }
    private void AddStackControls()
    {
        StackPanel panel = new StackPanel();
        {
            Margin = new Thickness(0, 0, 0, 0);
            HorizontalAlignment = HorizontalAlignment.Left;
        };
        Button PreorderTree_travers = new Button
        {
            Content = "Прямой обход дерева",
            Width = 360,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 460, 0, 0),
            Style = (Style)_mainWindow.FindResource("RoundedButtonStyle")
        };
        PreorderTree_travers.Click += (s, e) => ViewPreorderButton_Click(s, e);
        
        panel.Children.Add(PreorderTree_travers);

        _mainWindow.PageContentControl.Content = panel;
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
    private void ViewPreorderButton_Click(object sender, RoutedEventArgs e)
    {
        // Вывод дерева в текстовое поле
        OutputTextBox.Text = tree.PreorderPrint(root);
    }
    /*
    private void ViewInorderButton_Click(object sender, RoutedEventArgs e)
    {
        // Вывод дерева в текстовое поле
        OutputTextBox.Text = tree.InorderPrint(root);
    }
    private void ViewPostorderButton_Click(object sender, RoutedEventArgs e)
    {
        // Вывод дерева в текстовое поле
        OutputTextBox.Text = tree.PostorderPrint(root);
    }*/
}