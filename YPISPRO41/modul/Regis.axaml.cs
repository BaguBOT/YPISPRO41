using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using YPISPRO41.modul;

namespace YPISPRO41;

public partial class Regis : Window
{
    private MySqlConnectionStringBuilder _connectionSb;
    private ObservableCollection<Client> Clients { get; set; }
    public Regis()
    {
        Width = 650;
        Height = 350;
        InitializeComponent();
        Clients = new ObservableCollection<Client>();
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
                    cmd.CommandText = " INSERT INTO клиент (ID, ФИО, Дата_рождения, Контактные_данные) " +
                                      " VALUES ( " + Convert.ToInt32(IdBox.Text) + 
                                      " ,' " + FioBox.Text +
                                      " ',' " + DatabBox.Text + 
                                      " ',' " + ContakctNumBox.Text + "')";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        Clients.Add(new Client()
                        {
                            ID = Convert.ToInt32(IdBox.Text),
                            FIO = FioBox.Text,
                            data = DatabBox.Text,
                            contakct = ContakctNumBox.Text
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
                command.CommandText = " SELECT * FROM клиент ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Clients.Add(new Client()
                        {
                            ID = reader.GetInt32("ID"),
                            FIO = reader.GetString("ФИО"),
                            data = reader.GetString("Дата_рождения"),
                            contakct = reader.GetString("Контактные_данные")
                        });
                    }
                }
            }
            connection.Close();
        }
        RegisDataGrid.ItemsSource = Clients;
    }

    private void DelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var remove = RegisDataGrid.SelectedItem as Client;
        using (var cnn = new MySqlConnection(_connectionSb.ConnectionString))
        using (var cmd = cnn.CreateCommand())
        {
            cmd.CommandText = "DELETE FROM клиент where "+remove.ID;
            cnn.Open();
            cmd.ExecuteNonQuery();
        }
        Clients.Remove(remove);
        RegisDataGrid.DataContext = Clients;
    }

    private void Schedule_OnClick(object? sender, RoutedEventArgs e)
    {
        new Past_experience().Show();
    }
}
