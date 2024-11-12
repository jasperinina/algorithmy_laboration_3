using System.Windows;

namespace lab3.Utilities.QueueAnalyzer.QueueC_;

public partial class CSharpGraphQueue : Window
{
    public CSharpGraphQueue(double[] dataSizes, double[] times)
    {
        InitializeComponent();
        // Создание графика с использованием ScottPlot
        wpfPlot.Plot.Add.Scatter(dataSizes, times);
        wpfPlot.Plot.Title("График зависимости времени от количества данных (Очередь C#)");
        wpfPlot.Plot.XLabel("Количество данных");
        wpfPlot.Plot.YLabel("Время выполнения (мс)");
        // Настройка максимальных значений осей (динамически)
        wpfPlot.Plot.Axes.Left.Max = dataSizes.Max();
        wpfPlot.Plot.Axes.Bottom.Max = times.Max();
        // Обновление отображения
        wpfPlot.Refresh();
    }
}