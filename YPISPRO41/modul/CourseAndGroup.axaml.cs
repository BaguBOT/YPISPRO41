using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using YPISPRO41.modul;

namespace YPISPRO41;

public partial class CourseAndGroup : Window
{
    private MySqlConnectionStringBuilder _connectionSb;
    private ObservableCollection<GroupYp> Groups { get; set; }
    private ObservableCollection<CourseYp> Courses { get; set; }
    public CourseAndGroup()
    {
        Width = 850;
        Height = 400;
        InitializeComponent();
        Groups = new ObservableCollection<GroupYp>();
        Courses = new ObservableCollection<CourseYp>();
        _connectionSb = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Database = "yp",
            UserID = "User",
            Password = "123456"
        };
        ShowTable();
        ShowTable2();
    }
    private void AdAddButton_OnClick(object? sender, RoutedEventArgs e)
       {
              using (var cnn = new MySqlConnection(_connectionSb.ConnectionString))
              {
                  cnn.Open();
                  using (var cmd = cnn.CreateCommand())
                  {
                      cmd.CommandText = " INSERT INTO управление_группами (id, обучающися, группа, расписание)  " +
                                        " VALUES ( " + Convert.ToInt32(IDGBox.Text) +
                                        " ,' " + Convert.ToInt32(studentBox.Text) +
                                        " ',' " + Convert.ToInt32(groupBox.Text) +
                                        " ',' " + Convert.ToInt32(ScheduleBox.Text) + "')";
                      try
                      {
                          cmd.ExecuteNonQuery();
                          Groups.Add(new GroupYp()
                          {
                              ID = Convert.ToInt32(IDGBox.Text),
                              Client = Convert.ToInt32(studentBox.Text),
                              group = Convert.ToInt32(groupBox.Text),
                              ras = Convert.ToInt32(ScheduleBox.Text)
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
                command.CommandText = " SELECT * FROM управление_группами ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Groups.Add(new GroupYp()
                        {
                            ID = reader.GetInt32("ID"),
                            Client = reader.GetInt32("Обучающися"),
                            group = reader.GetInt32("Группа"),
                            ras =reader.GetInt32("Расписание")
                        });
                    }
                }
            }
            connection.Close();
        }
        GropDataGrid.ItemsSource = Groups;
    }
private void ShowTable2()
    {
        using (var connection = new MySqlConnection(_connectionSb.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = " SELECT * FROM управление_курсами ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Courses.Add(new CourseYp()
                        {
                            IDс = reader.GetInt32("ID"),
                            namecourse = reader.GetString("Название_Курса"),
                            lang = reader.GetInt32("Язык"),
                            stage = reader.GetInt32("Этап"),
                        });
                    }
                }
            }
            connection.Close();
        }
        coursegDataGrid.ItemsSource = Courses;
    }
    private void DelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (CheckBox.IsChecked == true)
        {
            var remove = GropDataGrid.SelectedItem as GroupYp;
            string del = DELBox.Text;
            using (var cnn = new MySqlConnection(_connectionSb.ConnectionString))
            { using (var cmd = cnn.CreateCommand())
                { cmd.CommandText = "DELETE FROM управление_группами where ID = "+ del;
                    cnn.Open();
                    cmd.ExecuteNonQuery();}
                Groups.Remove(remove);
                cnn.Close();}
            GropDataGrid.DataContext = Groups; 
        }
        else
        { this.Close();}
    }

    private void AdAdd2Button_OnClick(object? sender, RoutedEventArgs e)
    {
        using (var cnn = new MySqlConnection(_connectionSb.ConnectionString))
        {
            cnn.Open();
            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = " INSERT INTO управление_курсами (id, название_курса, язык, этап)   " +
                                  " VALUES ( " + Convert.ToInt32(IDсBox.Text) +
                                  " ,' " + CourseBox.Text +
                                  " ',' " + Convert.ToInt32(LanguageBox.Text) +
                                  " ',' " + Convert.ToInt32(StageBox.Text) + "')";
                try
                {
                    cmd.ExecuteNonQuery();
                    Courses.Add(new CourseYp()
                    {
                        IDс = Convert.ToInt32(IDсBox.Text),
                        namecourse = CourseBox.Text,
                        lang = Convert.ToInt32(LanguageBox.Text),
                        stage = Convert.ToInt32(StageBox.Text)
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

    private void Del2Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Check2Box.IsChecked == true)
        {
            var remove = coursegDataGrid.SelectedItem as CourseYp;
            string dele = DEL2Box.Text;
            using (var cnn = new MySqlConnection(_connectionSb.ConnectionString))
            { using (var cmd = cnn.CreateCommand())
                { cmd.CommandText = "DELETE FROM управление_курсами where ID = "+ dele;
                    cnn.Open();
                    cmd.ExecuteNonQuery();}
                Courses.Remove(remove);
                cnn.Close();}
            coursegDataGrid.DataContext = Courses; 
        }
        else
        { this.Close();}
    }
    private void Расписание_OnClick(object? sender, RoutedEventArgs e)
    {
        new modul.Schedule().Show();
    }
    private void GroopButton_OnClick(object? sender, RoutedEventArgs e)
    {
        new modul.Groupsel().Show();
    }
}