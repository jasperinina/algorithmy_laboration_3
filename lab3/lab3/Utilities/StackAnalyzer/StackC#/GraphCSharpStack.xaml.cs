using System.Windows;

namespace lab3.Utilities.StackAnalyzer.StackC_;

public partial class GraphCSharpStack : Window
{
    public GraphCSharpStack(double[] dataSizes, double[] times)
    {
        InitializeComponent();
        wpfPlot.Plot.Add.Scatter(dataSizes, times);
        wpfPlot.Plot.Title("График зависимости времени от количества данных");
        wpfPlot.Plot.XLabel("Количество данных");
        wpfPlot.Plot.YLabel("Время выполнения (мс)");
        wpfPlot.Plot.Axes.Left.Max = dataSizes.Max();
        wpfPlot.Plot.Axes.Bottom.Max = times.Max();
        // Отображение графика
        wpfPlot.Refresh();
    }
}