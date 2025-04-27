using AppInCube.Classes; // ���������, ��� ��� ������������ ���� ����������
using AppInCube.Classes.SQLite;
using AppInCube.Classes.SQLBD;
using Microsoft.Maui.Controls;
using SQLite;
using System;
//using static Android.Webkit.ConsoleMessage;

namespace AppInCube.View.Pages.Programs.UnderPages
{
    public partial class ProgramDetailPage : ContentPage
    {

        TableBird program = new TableBird();


        public ProgramDetailPage(TableBird bird, List<TableProgram> programs)
        {
            InitializeComponent();
            program.Id = bird.Id;
            // ������������� �������� �������� �� ����� ������, ������� �������� ��� �����, ��� � ���������
            BindingContext = new SQLliteTableBaseInfo
            {

                IdBirdInMySQL = bird.Id,//

                NameBird = bird.NameBird,
                Content = bird.Content,

                IdProgramInMySQL = bird.IdProgram, //

                DateTimeValue = bird.DateTimeValue,
                DaysUntilHatching = bird.DaysUntilHatching,

                ImageBirdFile = bird.ImageBirdFile, //

                tablePrograms = programs // ����������� ���������//
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
            SaveProgramData(program.Id);

        }
        private async void SaveProgramData(uint birdId)
        {
            // ���������, ���������� �� ��������� � ��������� ID
            var existingProgram = await App.Database.GetProgramByIdAsync(birdId);
            if (existingProgram == null) // ���� ����� ��������� ���, �� ���������
            {
                // �������� ������ �� BindingContext
                var programBaseInfo = BindingContext as SQLliteTableBaseInfo;


                // ������� ������ SQLliteTableBaseInfo � ��������� ��� ������� �� ������� TableBird
                var programDataBaseInfo = new SQLliteTableBaseInfo
                {
                    IdBirdInMySQL = programBaseInfo.IdBirdInMySQL, // ���������� Id �� TableBird
                    IdProgramInMySQL = programBaseInfo.IdProgramInMySQL,
                    NameBird = programBaseInfo.NameBird,
                    Content = programBaseInfo.Content,
                    DaysUntilHatching = programBaseInfo.DaysUntilHatching,
                    DateTimeValue = programBaseInfo.DateTimeValue,
                    ImageBirdFile = programBaseInfo.ImageBirdFile, // ��������� ������ ������ �����������
                    tablePrograms = programBaseInfo.tablePrograms
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