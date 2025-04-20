using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using AppInCube.Classes.SQLBD;


namespace AppInCube.Classes
{

    internal class MetodsGet
    {
        public static async Task<TableBird> GetId(TableBird variable, MySqlDataReader reader)
        {
            variable.Id = await Task.FromResult(reader.GetUInt32("Id"));
            return variable;
        }

        public static async Task<TableBird> GetNameBird(TableBird variable, MySqlDataReader reader)
        {
            variable.NameBird = await Task.FromResult(reader.GetString("NameBird"));
            return variable;
        }

        public static async Task<TableBird> GetContent(TableBird variable, MySqlDataReader reader)
        {
            variable.Content = await Task.FromResult(reader.GetString("Content"));
            return variable;
        }

        //public static async Task<TableBird> GetDaysUntilHatching(TableBird variable, MySqlDataReader reader)
        //{
        //    variable.DaysUntilHatching = await Task.FromResult(reader.GetByte("LastDay"));
        //    return variable;
        //}

        public static async Task<TableBird> GetDateTimeValue(TableBird variable, MySqlDataReader reader)
        {
            variable.DateTimeValue = await Task.FromResult(reader.GetDateTime("DateTimeValue"));
            return variable;
        }

        public static async Task<TableBird> GetImageBirdFile(TableBird variable, MySqlDataReader reader)
        {

            // Предполагается, что imageBirtFile хранится в виде BLOB в базе данных
            long length = reader.GetBytes("ImageBirdFile", 0, null, 0, 0);
            variable.ImageBirdFile = new byte[length];

            await Task.Run(() => reader.GetBytes("ImageBirdFile", 0, variable.ImageBirdFile, 0, (int)length));

            // Конвертация массива байтов в ImageSource
            variable.ImageSource = ImageSource.FromStream(() => new MemoryStream(variable.ImageBirdFile));

            return variable;
        }
    }
}
