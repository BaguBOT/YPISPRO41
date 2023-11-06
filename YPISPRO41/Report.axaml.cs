using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ScottPlot.Avalonia;

namespace YPISPRO41;

public partial class Report : Window
{
    public Report()
    {
        InitializeComponent(); 
        double[] values = { 26, 20, 23 };
        double[] positions = { 0, 1, 2};
        string[] labels = { "PHP", "JS", "C++" };
      
        
        AvaPlot avaPlot1 = this.Find<AvaPlot>("AvaPlot1");
        
        avaPlot1.Plot.AddBar(values, positions);
        avaPlot1.Plot.XTicks(positions, labels);
        avaPlot1.Plot.SetAxisLimits(yMin: 0);


    }

    
}