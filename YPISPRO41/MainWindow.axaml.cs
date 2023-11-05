using System;
using System.Runtime.InteropServices.ObjectiveC;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Tmds.DBus.Protocol;

namespace YPISPRO41;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        Width = 200;
        Height = 150;
        InitializeComponent();
    }

    private void login_OnClick(object? sender, RoutedEventArgs e)
    {
       // if (Login.Text.Contains("Logo") && Password.Text.Contains("Pass"))
      //  {
            new menu().Show();
      //  }
     //   else
      //  {
       //   this.Close();
      //  }
    }
}