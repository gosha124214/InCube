using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AppInCube.Classes.SQLite.Maked
{
    public class SQLliteTableDopInfoMake
    {
        //[PrimaryKey]
        public uint IdMakeProgram { get; set; } // Новое поле
        public uint? IdProgram { get; set; } // Теперь может быть null
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
