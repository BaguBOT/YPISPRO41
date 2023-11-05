using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace YPISPRO41.modul;

public partial class Schedule : Window
{
    private MySqlConnectionStringBuilder _connectionSb;
    private ObservableCollection<YPISPRO41.Schedule> Schedules { get; set; }
    public Schedule()
    {
        Width = 400;
        Height = 300;
        InitializeComponent();
        Schedules = new ObservableCollection<YPISPRO41.Schedule>();
        _connectionSb = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Database = "yp",
            UserID = "User",
            Password = "123456"
        };
        ShowTable();
    }
    private void DelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using (var cnn = new MySqlConnection(_connectionSb.ConnectionString))
        {
            cnn.Open();
            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = " INSERT INTO расписание (ID, Количество_дней, Недели)  " +
                                  " VALUES ( " + Convert.ToInt32(IdBox.Text) + 
                                  " ,' " + Convert.ToInt32(NumberofdaysBox.Text) +
                                  " ',' " + Convert.ToInt32(WeekBox.Text) + "')";
                try
                {
                    cmd.ExecuteNonQuery();
                    Schedules.Add(new YPISPRO41.Schedule()
                    {
                        ID = Convert.ToInt32(IdBox.Text),
                        number_of_days = Convert.ToInt32(NumberofdaysBox.Text),
                        week =Convert.ToInt32(WeekBox.Text)
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
                command.CommandText = " SELECT * FROM расписание ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Schedules.Add(new YPISPRO41.Schedule()
                        {
                            ID = reader.GetInt32("ID"),
                            number_of_days = reader.GetInt32("Количество_дней"),
                            week = reader.GetInt32("Недели"),
                       
                        });
                    }
                }
            }
            connection.Close();
        }
        ScheduleDataGrid.ItemsSource = Schedules;
    }

}