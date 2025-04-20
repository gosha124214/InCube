namespace AppInCube.View.GeneralUnderPages;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}
    private async void OnContinueClicked(object sender, EventArgs e)
    {
        // Сохраняем информацию о том, что приложение уже запускалось
        Preferences.Set("IsFirstRun", false);
        // Переход на главную страницу
        Application.Current.MainPage = new NavigationPage(new AppShell());
    }
}