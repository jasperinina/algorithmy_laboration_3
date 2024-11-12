using System.Windows;

namespace lab3.Utilities.QueueAnalyzer.QueueC_;

public partial class GraphQueueDifferentContent : Window
{
    public GraphQueueDifferentContent(double[] dataSizes, double[] times, string[] labels)
    {
        InitializeComponent();
        
        // Создаем позиции X для меток
        double[] xPositions = new double[labels.Length];
        for (int i = 0; i < labels.Length; i++)
        {
            xPositions[i] = i + 1;
        }

        // Добавление точек
        wpfPlot.Plot.Add.Scatter(xPositions, times);

        wpfPlot.Plot.Title("График зависимости времени выполнения от состава операций");
        wpfPlot.Plot.XLabel("Состав набора команд");
        wpfPlot.Plot.YLabel("Время выполнения (мс)");
        wpfPlot.Refresh();
    }
}