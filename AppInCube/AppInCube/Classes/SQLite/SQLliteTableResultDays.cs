using SQLite;
using Microsoft.Maui.Controls; // Не забудьте добавить это пространство имен
using System.IO;

namespace AppInCube.Classes.SQLite
{
    class SQLliteTableResultDays
    {
        [PrimaryKey, AutoIncrement]

        public uint IdPartyInSQLlite { get; set; }

        public byte Day { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
