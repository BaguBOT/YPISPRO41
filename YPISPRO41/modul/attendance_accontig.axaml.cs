using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace YPISPRO41;

public partial class attendance_accontig : Window
{
    private MySqlConnectionStringBuilder _connectionSb;
    private ObservableCollection<Attendance> Attendances { get; set; }
   
    public attendance_accontig()
    {
        Width = 500;
        Height = 300;
        InitializeComponent();
        Attendances = new ObservableCollection<Attendance>();
        _connectionSb = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Database = "yp",
            UserID = "User",
            Password = "123456"
        };
        ShowTable();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using (var cnn = new MySqlConnection(_connectionSb.ConnectionString))
        {
            cnn.Open();
            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = " INSERT INTO учет_посещаемости (ID, обучающий, группа, оценки)  " +
                                  " VALUES ( " + Convert.ToInt32(IDBox.Text) + 
                                  " ,' " + Convert.ToInt32(EducationalBox.Text) +
                                  " ',' " + Convert.ToInt32(GroupBox.Text) + 
                                  " ',' " + Convert.ToInt32(RatingsBox.Text) + "')";
                try
                {
                    cmd.ExecuteNonQuery();
                    Attendances.Add(new Attendance()
                    {
                        ID = Convert.ToInt32(IDBox.Text),
                        client = Convert.ToInt32(EducationalBox.Text),
                        group = Convert.ToInt32(GroupBox.Text),
                        estima = Convert.ToInt32(RatingsBox.Text)
                    });
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            cnn.Close();
        }
    }
    private void ShowTable()
    {
        using (var connection = new MySqlConnection(_connectionSb.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = " SELECT * FROM учет_посещаемости ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Attendances.Add(new Attendance()
                        {
                            ID = reader.GetInt32("ID"),
                            client = reader.GetInt32("Обучающий"),
                            group = reader.GetInt32("Группа"),
                            estima =reader.GetInt32("Оценки")
                        });

                    }
                }
            }
            connection.Close();
        }
        AttendanceAaccontigDataGrid.ItemsSource = Attendances;
    }
    private void DelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}