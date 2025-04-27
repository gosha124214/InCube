using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AppInCube.Classes.SQLite
{
    public class SQLliteBirdProgram
    {
        [PrimaryKey]
        public SQLliteTableBaseInfo tableBird { get; set; }
        public List<SQLliteTableDopInfo> tableProgram { get; set; }
    }
}
