using SQLite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AppInCube.Classes.SQLite.Partyes
{
    public class SQLliteTableParty
    {
        [PrimaryKey, AutoIncrement]
        public uint IdParty { get; set; }

        public uint? IdBirdInMySQL { get; set; } 
        public uint? IdProgramInMySQL { get; set; }

        public uint? IdMake { get; set; }
        public byte[] ImageBirdFile { get; set; } // Массив байтов для хранения изображения
                                                  // Вычисляемое свойство для получения ImageSource
        public ImageSource ImageSource => ByteArrayToImageSource(ImageBirdFile);
        private ImageSource ByteArrayToImageSource(byte[] imageBytes)
        {
            if (imageBytes != null && imageBytes.Length > 0)
            {
                return ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
            return null;
        }


        public DateTime DateTimeValue { get; set; }

        // Это свойство будет хранить сериализованный JSON
        public string TablePartyJson { get; set; }

        // Это свойство будет игнорироваться при работе с базой данных
        [Ignore]
        public List<SQLliteTableDopInfoParty> DopInfoParty
        {
            get => string.IsNullOrEmpty(TablePartyJson) ? new List<SQLliteTableDopInfoParty>() : JsonConvert.DeserializeObject<List<SQLliteTableDopInfoParty>>(TablePartyJson);
            set => TablePartyJson = JsonConvert.SerializeObject(value);
        }
    }
}
