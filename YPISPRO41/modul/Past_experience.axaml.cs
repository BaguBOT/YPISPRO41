using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace YPISPRO41.modul;

public partial class Past_experience : Window
{
    private MySqlConnectionStringBuilder _connectionSb;
    private ObservableCollection<Past_exprience> PassExpriences { get; set; }
    public Past_experience()
    {
        Width = 400;
        Height = 350;
        InitializeComponent();
        PassExpriences = new ObservableCollection<Past_exprience>();
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
                cmd.CommandText = " INSERT INTO опыт_прошлых_языков (ID, Клиент, Языки, Уровень)  " +
                                  " VALUES ( " + Convert.ToInt32(IdBox.Text) + 
                                  " ,' " + ClientBox.Text +
                                  " ',' " + LangBox.Text + 
                                  " ',' " + LevelBox.Text + "')";
                try
                {
                    cmd.ExecuteNonQuery();
                    PassExpriences.Add(new Past_exprience()
                    {
                        ID = Convert.ToInt32(IdBox.Text),
                        client = Convert.ToInt32(ClientBox.Text),
                        lang = Convert.ToInt32(LangBox.Text),
                        level = Convert.ToInt32(LevelBox.Text)
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
        if (CheckBox.IsChecked == true)
        {
            var remove = PastExperienceDataGrid.SelectedItem as YPISPRO41.Past_exprience;
            string del = DELBox.Text;
            using (var cnn = new MySqlConnection(_connectionSb.ConnectionString))
            { using (var cmd = cnn.CreateCommand())
                { cmd.CommandText = "DELETE FROM опыт_прошлых_языков where ID = "+ del;
                    cnn.Open();
                    cmd.ExecuteNonQuery();}
                PassExpriences.Remove(remove);
                cnn.Close();}
            PastExperienceDataGrid.DataContext = PassExpriences; 
        }
        else
        { this.Close();}
    }
    private void ShowTable()
    {
        using (var connection = new MySqlConnection(_connectionSb.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = " SELECT * FROM опыт_прошлых_языков ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PassExpriences.Add(new Past_exprience()
                        {
                            ID = reader.GetInt32("ID"),
                            client = reader.GetInt32("Клиент"),
                            lang = reader.GetInt32("Языки"),
                            level = reader.GetInt32("Уровень")
                        });
                    }
                }
            }
            connection.Close();
        }
        PastExperienceDataGrid.ItemsSource = PassExpriences;
    }
}