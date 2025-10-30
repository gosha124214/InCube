using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppInCube.Classes.SQLite.Downloaded;
using SQLite;
using Newtonsoft.Json;

namespace AppInCube.Classes.SQLite.Partyes
{
    public class SQLliteTableDopInfoParty
    {
        //[PrimaryKey]
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

        // Новые свойства для отслеживания состояния
        public bool IsNotCompleted { get; set; } = true; // По умолчанию не выполнено
        public bool IsCompleted { get; set; } = false; // По умолчанию не выполнено

        // Свойство для хранения статуса (сохраняется в БД)
        // Enum для статуса (сохраняется в БД как int)
        public DayStatus Status { get; set; } = DayStatus.Waiting;
    }

        // Enum для статусов дня
        public enum DayStatus
        {
            Waiting = 0,      // "В ожидании" - день еще не наступил
            NotRecorded = 1,  // "Не записано" - день прошел, данные не записаны
            Completed = 2,    // "Выполнено" - данные записаны
            Available = 3     // "Не выполнено" - день наступил, можно записывать
        }
        
}