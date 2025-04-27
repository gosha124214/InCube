using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace AppInCube.Classes.SQLBD
{
    public class TableBird
    {
        [Key]
        public uint Id { get; set; }
        public uint IdProgram { get; set; }
        public string NameBird { get; set; }
        public string Content { get; set; }

        public DateTime DateTimeValue { get; set; }
        public byte[] ImageBirdFile { get; set; }

        public ImageSource ImageSource { get; set; } // свойство для хранения изображения

        public byte DaysUntilHatching { get; set; }


    }
}
