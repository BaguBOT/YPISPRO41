using Avalonia.Controls;
using Avalonia.Interactivity;
namespace YPISPRO41;
public partial class LogPass : Window
{
    public LogPass()
    {
        Width = 130;
        Height = 180;
        InitializeComponent();
    }
    private void login_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Login.Text.Contains("Logo") && Password.Text.Contains("Pass"))
        { new Menu().Show(); }
        else
        { this.Close(); }
    }
}