using AppInCube.Classes.SQLite.Maked;

namespace AppInCube.View.Pages.Favorites.MakeProgramm.UnderPagesInMakeProgramm
{
    public partial class ProgramDetailPageMake : ContentPage
    {
        public ProgramDetailPageMake(SQLliteTableBaseInfoMake program)
        {
            InitializeComponent();

            // Устанавливаем контекст привязки на объект программы
            BindingContext = program;
        }
    }
}
