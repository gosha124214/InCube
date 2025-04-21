using AppInCube.Classes; // ���������, ��� ��� ������������ ���� ����������
using AppInCube.Classes.SQLite;
using AppInCube.Classes.SQLBD;
using Microsoft.Maui.Controls;
using SQLite;
//using static Android.Webkit.ConsoleMessage;

namespace AppInCube.View.Pages.Programs.UnderPages
{
    public partial class ProgramDetailPage : ContentPage
    {

        TableBird program = null;

        public ProgramDetailPage(TableBird program)
        {
            this.program = program;

            LoadProgramById(program.Id);
            InitializeComponent();
            BindingContext = program; // ������������� �������� �������� �� ���������� ������

        }

        private async void LoadProgramById(uint programId)
        {
            var idProgram = await App.Database.GetProgramByIdAsync(programId);
            if (idProgram != null)
            {
                GoToProgramButton.IsVisible = true;
            }
            else
            {
                GoToProgramButton.IsVisible = false;
            }
        }



        private void DownloadTheProgram(object sender, EventArgs e)
        {
            // ����� ������ ��� ���������� ������
            SaveProgramData(program.Id);

        }
        private async void SaveProgramData(uint programId)
        {
            // ���������, ���������� �� ��������� � ��������� ID
            var existingProgram = await App.Database.GetProgramByIdAsync(programId);
            if (existingProgram == null) // ���� ����� ��������� ���, �� ���������
            {
                // �������� ������ �� BindingContext
                var program = BindingContext as TableBird;

                // ������� ������ SQLliteTableBaseInfo � ��������� ��� ������� �� ������� TableBird
                var programData = new SQLliteTableBaseInfo
                {
                    IdBirdInMySQL = program.Id, // ���������� Id �� TableBird
                    IdProgramInMySQL = program.IdProgram,
                    NameBird = program.NameBird,
                    Content = program.Content,
                 //   DaysUntilHatching = program.DaysUntilHatching,
                    DateTimeValue = program.DateTimeValue,
                    ImageBirdFile = program.ImageBirdFile // ��������� ������ ������ �����������
                };

                try
                {
                    // ���������� ����� SaveProgramAsync ��� ���������� ������ � �������
                    await App.Database.SaveProgramAsync(programData); // ���������, ��� ����� ��������� SQLliteTableBaseInfo

                    GoToProgramButton.IsVisible = true;
                    Console.WriteLine("������ ������� ���������!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"������ ��� ���������� ������: {ex.Message}");
                }

                ShowMessage("��������� ���������");
            }
            else
            {
                ShowMessage("��� ��������� ��� ���������");
            }
        }

        private async void ShowMessage(string message, int duration = 5000)
        {
            MessageLabel.Text = message; // ������������� ����� ���������
            MessageFrame.IsVisible = true; // ������ Frame �������

            // ���� ��������� �����
            await Task.Delay(duration);

            // �������� Frame
            MessageFrame.IsVisible = false;
        }



        // ���������� ������� ��� �������� � ���������
        private async void GoToProgram(object sender, EventArgs e)
        {
            // ������ �������� � ���������
            await DisplayAlert("�������", "�� ������� � ���������!", "OK");
            // ����� �� ������ �������� ������ ��� �������� � ������ �������� ��� ���������� ������ ��������
        }

    }
}