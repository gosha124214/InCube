using AppInCube.View.Pages.Favorites.MakeProgramm.UnderPagesInMakeProgramm;

namespace AppInCube.View.Pages.Favorites.MakeProgramm
{
    public partial class MakeProgramm : ContentPage
    {
        public MakeProgramm()
        {
            InitializeComponent();
        }

        private async void OnCreateProgramButtonClicked(object sender, EventArgs e)
        {
            // Переход на страницу UnderPagesMakeProgramm
            await Navigation.PushAsync(new UnderPagesMakeProgramm());
        }
    }
}
