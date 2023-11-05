using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using YPISPRO41.Class;

namespace YPISPRO41.modul;

public partial class Groupsel : Window
{
    private MySqlConnectionStringBuilder _connectionSb;
    private ObservableCollection<GroupN> GroupNs { get; set; }
    public Groupsel()
    {
        Width = 400;
        Height = 300;
        InitializeComponent();
        GroupNs = new ObservableCollection<GroupN>();
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
                cmd.CommandText = " INSERT INTO группа (ID, Название_группы , Курс)  " +
                                  " VALUES ( " + Convert.ToInt32(IdBox.Text) + 
                                  " ,' " + NamegroupBox.Text +
                                  " ',' " + Convert.ToInt32(CourseBox.Text) + "')";
                try
                {
                    cmd.ExecuteNonQuery();
                    GroupNs.Add(new YPISPRO41.Class.GroupN()
                    {
                        ID = Convert.ToInt32(IdBox.Text),
                        namegroup = Convert.ToString(NamegroupBox.Text),
                        course =Convert.ToInt32(CourseBox.Text)
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

    private void DelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
    private void ShowTable()
    {
        using (var connection = new MySqlConnection(_connectionSb.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = " SELECT * FROM группа ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GroupNs.Add(new GroupN()
                        {
                            ID = reader.GetInt32("ID"),
                            namegroup = reader.GetString("Название_группы"),
                            course = reader.GetInt32("Курс"),
                        });
                    }
                }
            }
            connection.Close();
        }
        GroupDataGrid.ItemsSource = GroupNs;
    }
}