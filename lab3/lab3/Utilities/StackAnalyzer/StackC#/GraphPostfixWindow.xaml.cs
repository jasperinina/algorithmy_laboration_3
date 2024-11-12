using System.Windows;

namespace lab3.Utilities.StackAnalyzer.StackC_;

public partial class GraphPostfixWindow : Window
{
    public GraphPostfixWindow(double[] expressionLengths, double[] times)
    {
        InitializeComponent();
        wpfPlot.Plot.Add.Scatter(expressionLengths, times);
        wpfPlot.Plot.Title("График зависимости времени от длины постфиксного выражения");
        wpfPlot.Plot.XLabel("Длина выражения");
        wpfPlot.Plot.YLabel("Время выполнения (мс)");
        wpfPlot.Plot.Axes.Left.Max = expressionLengths.Max();
        wpfPlot.Plot.Axes.Bottom.Max = times.Max();
        // Обновление графика
        wpfPlot.Refresh();
    }
}