
using AppInCube.Classes.SQLite;

namespace AppInCube.View.Pages.Favorites.UnderPagesInFavorites
{


    public partial class NewPage1 : ContentPage
    {

        public NewPage1(SQLliteTableBaseInfo programs)
        {
            InitializeComponent();

            // Устанавливаем контекст привязки на новый объект, который содержит как птицу, так и программы
            BindingContext = new SQLliteTableBaseInfo
            {

                IdBirdInMySQL = programs.IdBirdInMySQL,//

                NameBird = programs.NameBird,
                Content = programs.Content,

                IdProgramInMySQL = programs.IdProgramInMySQL, //

                DateTimeValue = programs.DateTimeValue,
                DaysUntilHatching = programs.DaysUntilHatching,

                ImageBirdFile = programs.ImageBirdFile, //

                tablePrograms = programs.tablePrograms // Привязываем программы//
            };

        }

    }
}