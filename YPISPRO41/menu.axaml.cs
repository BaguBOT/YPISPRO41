using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace YPISPRO41;

public partial class menu : Window
{
    public menu()
    {
        Width = 200;
        Height = 200;
        InitializeComponent();
    }
    

    private void FinancialAccounting_OnClick(object? sender, RoutedEventArgs e)
    {
       new Financial_Accounting().Show();
    }

    private void AttendanceAccontig_OnClick(object? sender, RoutedEventArgs e)
    {
       new attendance_accontig().Show();
    }

    private void Regis_OnClick(object? sender, RoutedEventArgs e)
    {
        new Regis().Show();
    }

  

    private void CourseManagement_OnClick_OnClick(object? sender, RoutedEventArgs e)
    {
        new CourseAndGroup().Show();
    }
    

    private void Report_OnClick(object? sender, RoutedEventArgs e)
    {
        new Report().Show();
    }
}