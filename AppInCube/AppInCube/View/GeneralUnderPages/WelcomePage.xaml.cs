namespace AppInCube.View.GeneralUnderPages;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}
    private async void OnContinueClicked(object sender, EventArgs e)
    {
        // ��������� ���������� � ���, ��� ���������� ��� �����������
        Preferences.Set("IsFirstRun", false);
        // ������� �� ������� ��������
        Application.Current.MainPage = new NavigationPage(new AppShell());
    }
}