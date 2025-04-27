
using SQLite;
using Microsoft.Maui.Controls; // Не забудьте добавить это пространство имен
using System.IO;
using AppInCube.Classes.SQLBD;
using Newtonsoft.Json;
namespace AppInCube.Classes.SQLite
{
    public class SQLliteTableBaseInfo
    {
        [PrimaryKey]
        public uint IdBirdInMySQL { get; set; }
        public uint IdProgramInMySQL { get; set; }
        public string NameBird { get; set; }
        public string Content { get; set; }
        public byte DaysUntilHatching { get; set; }
        public DateTime DateTimeValue { get; set; }
        public byte[] ImageBirdFile { get; set; } // Массив байтов для хранения изображения

        [Ignore]
        public List<TableProgram> tablePrograms
        {
            get => string.IsNullOrEmpty(TableProgramsJson) ? new List<TableProgram>() : JsonConvert.DeserializeObject<List<TableProgram>>(TableProgramsJson);
            set => TableProgramsJson = JsonConvert.SerializeObject(value);
        }

        public string TableProgramsJson { get; set; } // Хранит сериализованный JSON

        // Вычисляемое свойство для получения ImageSource
        public ImageSource ImageSource => ByteArrayToImageSource(ImageBirdFile);

        // Метод для конвертации byte[] в ImageSource
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
