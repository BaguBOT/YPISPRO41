using System;
using System.Collections.ObjectModel;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace YPISPRO41;

public partial class Financial_Accounting : Window
{
    private MySqlConnectionStringBuilder _connectionSb;
    private ObservableCollection<Financial> Financials { get; set; }
    public Financial_Accounting()
    {
        Width = 600;
        Height = 400;
        InitializeComponent();
        Financials = new ObservableCollection<Financial>();
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
                cmd.CommandText = " INSERT INTO финансовый_учет (ID,Клиент, Курс, Статус_оплаты, Сумма, Дата) " +
                                  " VALUES ( " + Convert.ToInt32(IDBox.Text) + 
                                  " ,' " + Convert.ToInt32(ClientBox.Text) +
                                  " ',' " + Convert.ToInt32(WellBox.Text) + 
                                  " ',' " + PaymentBox.Text + 
                                  " ',' " + Convert.ToInt32(SumBox.Text) + 
                                  " ',' " + DataBox.Text + "')";
                try
                {
                    cmd.ExecuteNonQuery();
                    Financials.Add(new Financial()
                    {
                        ID = Convert.ToInt32(IDBox.Text),
                        client = Convert.ToInt32(ClientBox.Text),
                        course = Convert.ToInt32(WellBox.Text),
                        status = PaymentBox.Text,
                        Sum = Convert.ToInt32(SumBox.Text),
                        data = DataBox.Text
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
                command.CommandText = " SELECT * FROM финансовый_учет ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Financials.Add(new Financial()
                        {
                            ID = reader.GetInt32("ID"),
                            client = reader.GetInt32("Клиент"),
                            course = reader.GetInt32("Курс"),
                            status = reader.GetString("Статус_оплаты"),
                            Sum =reader.GetInt32("Сумма"),
                            data = reader.GetString("Дата")
                        });

                    }
                }
            }

            connection.Close();
        }

        FinancialAccountingDataGrid.ItemsSource = Financials;
    }

    private void DelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (CheckBox.IsChecked == true)
        {
            var remove = FinancialAccountingDataGrid.SelectedItem as Financial;
            string del = DELBox.Text;
            using (var cnn = new MySqlConnection(_connectionSb.ConnectionString))
            { using (var cmd = cnn.CreateCommand())
                { cmd.CommandText = "DELETE FROM финансовый_учет where ID = "+ del;
                    cnn.Open();
                    cmd.ExecuteNonQuery();}
                Financials.Remove(remove);
                cnn.Close();}
            FinancialAccountingDataGrid.DataContext = Financials; 
        }
        else
        { this.Close();}
    }
}