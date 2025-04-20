using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AppInCube.Classes.SQLBD
{
    internal class Conect
    {

        private const string connectionString = "User Id=goshalikhoy12;password=nv5_yFYgj-W_haJ;Host=db4free.net;Database=goshalikhoy12;Persist Security Info=True;";

        public Conect(string connectionString = connectionString)
        {
        }

        public async Task<TableBird> GetDataFromDB(string query, Func<TableBird, MySqlDataReader, Task> getValue, string connectionString = connectionString)
        {
            TableBird classVariables = new TableBird(); // Создаем новый экземпляр класса


            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Чтение первой строки результата
                            {

                                await getValue(classVariables, reader);

                            }
                        }

                    }
                }
                catch (Exception ex) { StringMessages.stringMessage = "Проверьте подключение с интернетом"; }

            }
            return classVariables; // Возвращаем объект 
        }

        public async Task<uint> GetNumberAllObjectAsync()
        {
            string commandSQL = "SELECT COUNT(*) AS TotalCount FROM TableBird;";
            uint totalCount = 0; // Инициализируем переменную

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync(); // Асинхронное открытие соединения

                using (var command = new MySqlCommand(commandSQL, connection))
                {
                    // Используем await для получения результата
                    var result = await command.ExecuteScalarAsync();
                    if (result != null && uint.TryParse(result.ToString(), out uint count))
                    {
                        totalCount = count; // Сохраняем результат в totalCount
                    }
                }
                }
                catch (Exception ex) { StringMessages.stringMessage = "Проверьте подключение с интернетом"; }
            }

            // Возвращаем totalCount, который будет 0, если не удалось получить количество
            return totalCount;
        }
    }
}
