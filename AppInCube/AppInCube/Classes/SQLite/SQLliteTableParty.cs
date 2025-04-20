using SQLite;
using Microsoft.Maui.Controls; // Не забудьте добавить это пространство имен
using System.IO;


namespace AppInCube.Classes.SQLite
{
    class SQLliteTableParty
    {
        [PrimaryKey, AutoIncrement]
        public uint IdPartyInSQLlite { get; set; }
        public uint IdProgramInMySQL { get; set; }
    }
}
