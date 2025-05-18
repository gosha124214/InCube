using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AppInCube.Classes.SQLite.Partyes
{
    public class SQLliteTableParty
    {
        [PrimaryKey]
        public uint IdParty{ get; set; }
        public uint? IdProgramInMySQL { get; set; }
        public uint? IdInsideProgram { get; set; }
        public uint? IdBirdInMySQL { get; set; }
        public uint? IdInsideBird { get; set; }
        public DateTime DateTimeValue { get; set; }
    }
}
