using SQLite;
using Microsoft.Maui.Controls; // Не забудьте добавить это пространство имен
using System.IO;

namespace AppInCube.Classes.SQLite
{
    public class SQLliteTableStandardDays
    {
        [PrimaryKey, AutoIncrement]
        public uint IdProgramInMySQL { get; set; }
        public byte Day { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }


    }
}