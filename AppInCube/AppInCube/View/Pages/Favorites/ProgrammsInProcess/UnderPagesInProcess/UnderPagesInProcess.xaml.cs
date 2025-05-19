







using AppInCube.Classes.SQLite.Partyes;

namespace AppInCube.View.Pages.Favorites.ProgrammsInProcess.UnderPagesInProcess
{
    public partial class UnderPagesInProcess : ContentPage
    {
        public UnderPagesInProcess(SQLliteTableParty party)
        {
            InitializeComponent();

            // Устанавливаем контекст привязки на новый объект, который содержит как птицу, так и программы
            BindingContext = new SQLliteTableParty
            {

                IdBirdInMySQL = party.IdBirdInMySQL,//


                IdProgramInMySQL = party.IdProgramInMySQL, //

                DateTimeValue = party.DateTimeValue,


                DopInfoParty = party.DopInfoParty //



                //DaysUntilHatching = programs.DaysUntilHatching,

                //ImageBirdFile = programs.ImageBirdFile, //
                //NameBird = programs.NameBird,
                //Content = programs.Content

            };
        }
        private async void OnCompletedButtonClicked(object sender, EventArgs e)
        {
            // Получаем объект дня, который был завершен
            var button = sender as Button;
            var dopInfo = button?.CommandParameter as SQLliteTableDopInfoParty;

            if (dopInfo != null)
            {
                // Логика для обработки завершения дня
                await DisplayAlert("Завершено", $"День {dopInfo.Day} завершен!", "OK");

                // Здесь вы можете добавить дополнительную логику, например, обновление базы данных или интерфейса
            }
        }

    }

}