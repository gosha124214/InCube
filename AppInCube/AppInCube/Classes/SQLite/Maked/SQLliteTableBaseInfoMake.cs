using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Controls;

namespace AppInCube.Classes.SQLite.Maked
{
    public class SQLliteTableBaseInfoMake
    {
        [PrimaryKey, AutoIncrement] // Автоматическое инкрементирование
        public uint IdMakeBird { get; set; } // Уникальный идентификатор для птицы

        // Инициализируем IdMakeProgram от IdMakeBird
        public uint IdMakeProgram => IdMakeBird; // Теперь IdMakeProgram равен IdMakeBird

        public uint? IdBirdInMySQL { get; set; } // Теперь может быть null
        public uint? IdProgramInMySQL { get; set; } // Теперь может быть null
       

        public string NameBird { get; set; }
        public string Content { get; set; }
        public byte DaysUntilHatching { get; set; }
        public DateTime DateTimeValue { get; set; }
        public byte[] ImageBirdFile { get; set; }

        [Ignore]
        public List<SQLliteTableDopInfoMake> tablePrograms // Изменено на SQLliteTableDopInfoMake
        {
            get => string.IsNullOrEmpty(TableProgramsJson) ? new List<SQLliteTableDopInfoMake>() : JsonConvert.DeserializeObject<List<SQLliteTableDopInfoMake>>(TableProgramsJson);
            set => TableProgramsJson = JsonConvert.SerializeObject(value);
        }

        public string TableProgramsJson { get; set; } // Хранит сериализованный JSON

        public ImageSource ImageSource => ByteArrayToImageSource(ImageBirdFile);

        private ImageSource ByteArrayToImageSource(byte[] imageBytes)
        {
            if (imageBytes != null && imageBytes.Length > 0)
            {
                return ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
            return null;
        }
    }
}
