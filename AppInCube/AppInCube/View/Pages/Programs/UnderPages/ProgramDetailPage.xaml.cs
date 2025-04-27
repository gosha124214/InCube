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


        public ProgramDetailPage(TableBird bird, List<TableProgram> programs)
        {
            InitializeComponent();

            // ������������� �������� �������� �� ����� ������, ������� �������� ��� �����, ��� � ���������
            BindingContext = new
            {
                NameBird = bird.NameBird,
                Content = bird.Content,
                Id = bird.Id,
                IdProgram = bird.IdProgram,
                DateTimeValue = bird.DateTimeValue,
                DaysUntilHatching = bird.DaysUntilHatching,
                ImageSource = ImageSource.FromStream(() => new MemoryStream(bird.ImageBirdFile)),
                tableProgram = programs // ����������� ���������
            };

            LoadProgramById(bird.Id);
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
            SaveProgramData(program.Id, program.IdProgram);

        }
        private async void SaveProgramData(uint programId, uint IdProgram)
        {
            // ���������, ���������� �� ��������� � ��������� ID
            var existingProgram = await App.Database.GetProgramByIdAsync(programId);
            var existingProgramDop = await App.Database.GetProgramByIdAsyncDop(IdProgram);
            if (existingProgram == null) // ���� ����� ��������� ���, �� ���������
            {
                // �������� ������ �� BindingContext
                var programBaseInfo = BindingContext as TableBird;

                // ������� ������ SQLliteTableBaseInfo � ��������� ��� ������� �� ������� TableBird
                var programDataBaseInfo = new SQLliteTableBaseInfo
                {
                    IdBirdInMySQL = programBaseInfo.Id, // ���������� Id �� TableBird
                    IdProgramInMySQL = programBaseInfo.IdProgram,
                    NameBird = programBaseInfo.NameBird,
                    Content = programBaseInfo.Content,
                    DaysUntilHatching = programBaseInfo.DaysUntilHatching,
                    DateTimeValue = programBaseInfo.DateTimeValue,
                    ImageBirdFile = programBaseInfo.ImageBirdFile // ��������� ������ ������ �����������
                };

                    // �������� ������ �� BindingContext
                    var program = BindingContext as TableProgram;

                    var programData = new SQLliteTableDopInfo
                    {
                        IdProgram = program.IdProgram,
                        Day = program.Day,
                        MinHumidity = program.MinHumidity,
                        MaxHumidity = program.MaxHumidity,
                        MinTemperature = program.MinTemperature,
                        MaxTemperature = program.MinTemperature,
                        MinTimeCooling = program.MinTimeCooling,
                        MaxTimeCooling = program.MaxTimeCooling,
                        Min�mountTurn = program.Min�mountTurn,
                        Max�mountTurn = program.Max�mountTurn,
                        �mountCooling = program.�mountCooling

                    };
                

                try
                {
                    // ���������� ����� SaveProgramAsync ��� ���������� ������ � �������
                    await App.Database.SaveProgramAsync(programDataBaseInfo); // ���������, ��� ����� ��������� SQLliteTableBaseInfo

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