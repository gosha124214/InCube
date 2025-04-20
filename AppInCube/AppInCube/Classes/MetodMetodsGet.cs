using AppInCube.Classes.SQLBD;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AppInCube.Classes.MetodsGet; // Импортируем статические методы


namespace AppInCube.Classes
{

    public static class MetodMetodsGet
    {
        public static async Task MetodMetodsGetBasicInformation(TableBird variable, MySqlDataReader reader)
        {
            await GetId(variable, reader);
            await GetNameBird(variable, reader);
            await GetContent(variable, reader);
         //   await GetDaysUntilHatching(variable, reader);
            await GetDateTimeValue(variable, reader);
            await GetImageBirdFile(variable, reader);

        }
    }
}
