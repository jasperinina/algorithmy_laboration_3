using System.Windows;

namespace lab3.Utilities.StackAnalyzer;

public partial class CraphPostfixConverter : Window
{
    public CraphPostfixConverter(double[] dataSizes, double[] times)
    {
        InitializeComponent();
        wpfPlot.Plot.Add.Scatter(dataSizes, times);
        wpfPlot.Plot.Title("График зависимости времени от количества данных");
        wpfPlot.Plot.XLabel("Количество данных");
        wpfPlot.Plot.YLabel("Время выполнения (мс)");
        // Настройка максимальных значений осей (динамически)
        wpfPlot.Plot.Axes.Left.Max = dataSizes.Max();
        wpfPlot.Plot.Axes.Bottom.Max = times.Max();
        wpfPlot.Refresh();
    }
}