using System.Security.AccessControl;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;

namespace AppInCube.View;

public partial class MainPage : ContentPage
{

    private async void OnCounterClicked(object? sender, EventArgs e)
    {
       await Navigation.PushAsync(new AppShell());
       
        //await Shell.Current.GoToAsync("AppShell");
        //MainPage = new HomePage();
    }

    //Label connectionStateLbl;
    public MainPage()
    {
        InitializeComponent();


        //string connectionString = "User Id=goshalikhoy12;password=nv5_yFYgj-W_haJ;Host=db4free.net;Database=goshalikhoy12;Persist Security Info=True;";
        //string tableName = "Tabl1"; // Замените на имя вашей таблицы
        //string query = $"SHOW COLUMNS FROM {tableName}";
        //string columnName;
        //using (var connection = new MySqlConnection(connectionString))
        //{
        //    try
        //    {

        //        connection.Open();

                
        //        using (var command = new MySqlCommand(query, connection))
        //        {
        //            using (var reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    columnName = reader["Field"].ToString();
        //                    Console.WriteLine($"Имя столбца: {columnName}");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex) { }

        //}
    }

}
