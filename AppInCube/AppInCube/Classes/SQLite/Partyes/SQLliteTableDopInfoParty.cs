using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AppInCube.Classes.SQLite.Partyes
{
    class SQLliteTableDopInfoParty
    {
        [PrimaryKey]
        public uint IdParty { get; set; }
        public byte Day { get; set; }
        public float MinTemperature { get; set; }
        public float MaxTemperature { get; set; }
        public int MinHumidity { get; set; }
        public int MaxHumidity { get; set; }
        public byte? MinАmountTurn { get; set; }
        public byte? MaxАmountTurn { get; set; }
        public byte? АmountCooling { get; set; }
        public TimeSpan? MinTimeCooling { get; set; }
        public TimeSpan? MaxTimeCooling { get; set; }
    }
}
