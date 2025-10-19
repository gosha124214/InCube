using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using AppInCube.View.Pages.Favorites.DownloadedProgramms.UnderPagesInFavorites;
using AppInCube.Classes.SQLite.Downloaded;

using AppInCube.Classes.SQLite.Partyes;
using AppInCube.View.Pages.Favorites.ProgrammsInProcess;

using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace AppInCube.View.Pages.Favorites.DownloadedProgramms
{
    public partial class DownloadedProgrammsPage : ContentPage
    {
        public DownloadedProgrammsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadFavoritePrograms(); // Загружаем программы при появлении страницы
        }

        private async Task LoadFavoritePrograms()
        {
            try
            {
                var favoritePrograms = await App.DatabaseProgram.GetProgramsAsync(); // Получаем все программы

                // Привязка данных к интерфейсу
                ProgramsListView.ItemsSource = favoritePrograms; // Привязка данных к CollectionView
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", $"Ошибка при загрузке данных: {ex.Message}", "OK");
            }
        }

        private async void OnStartButtonClicked(object sender, EventArgs e)
        {
            // Получаем объект программы, которую нужно запустить
            var button = sender as Button;
            var program = button?.CommandParameter as SQLliteTableBaseInfo;

            if (program != null)
            {
                // Создаем новую запись в базе данных для запущенной программы
                await StartProgram(program);

                //// Переход на страницу ProgrammsInProcess
                //await Navigation.PushAsync(new ProgrammsInProcess.ProgrammsInProcess());
            }
        }


        private async Task StartProgram(SQLliteTableBaseInfo program)
        {
            // Создаем новую запись в базе данных для запущенной программы
            var newParty = new SQLliteTableParty
            {
                IdProgramInMySQL = program.IdProgramInMySQL,
                IdBirdInMySQL = program.IdBirdInMySQL,
                DateTimeValue = DateTime.Now, // Устанавливаем текущее время
                ImageBirdFile = program.ImageBirdFile,
                DopInfoParty = new List<SQLliteTableDopInfoParty>() // Инициализируем список
            };

            try
            {
                // Получаем информацию о доп. данных программы по ID
                List<SQLliteTableDopInfo> existingDopInfo = await App.DatabaseProgram.GetDopInfoByProgramIdAsync(program.IdProgramInMySQL);

                // Создаем временный список для хранения информации о днях
                var dopInfoPartyList = new List<SQLliteTableDopInfoParty>();

                // Добавляем информацию о днях в временный список
                foreach (var dopInfo in existingDopInfo)
                {
                    dopInfoPartyList.Add(new SQLliteTableDopInfoParty
                    {
                        IdParty = newParty.IdParty,
                        Day = dopInfo.Day,
                        MinTemperature = dopInfo.MinTemperature,
                        MaxTemperature = dopInfo.MaxTemperature,
                        MinHumidity = dopInfo.MinHumidity,
                        MaxHumidity = dopInfo.MaxHumidity,
                        MinАmountTurn = dopInfo.MinАmountTurn,
                        MaxАmountTurn = dopInfo.MaxАmountTurn,
                        АmountCooling = dopInfo.АmountCooling,
                        MinTimeCooling = dopInfo.MinTimeCooling,
                        MaxTimeCooling = dopInfo.MaxTimeCooling
                    });
                }

                // Присваиваем временный список в свойство DopInfoParty
                newParty.DopInfoParty = dopInfoPartyList;

                // Сохраняем новую запись в базе данных
                await App.DatabaseParty.SavePartyAsync(newParty);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", $"Ошибка при запуске программы: {ex.Message}", "OK");
            }
        }



        private async void OnProgramTapped(object sender, EventArgs e)
        {
            // Блокируем взаимодействие с текущей страницей
            this.IsEnabled = false;

            var tappedItem = (sender as StackLayout).BindingContext as SQLliteTableBaseInfo;
            if (tappedItem != null)
            {
                await Navigation.PushAsync(new ProgramDetailPage(tappedItem));
            }

            this.IsEnabled = true; // Разблокируем взаимодействие с текущей страницей
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            // Получаем объект программы, который нужно удалить
            var button = sender as Button;
            var program = button?.CommandParameter as SQLliteTableBaseInfo;

            if (program != null)
            {
                await DeleteProgram(program);
            }
        }

        private async Task DeleteProgram(SQLliteTableBaseInfo program)
        {
            // Подтверждение удаления
            bool confirm = await DisplayAlert("Подтверждение", "Вы уверены, что хотите удалить эту программу?", "Да", "Нет");
            if (confirm)
            {
                try
                {
                    // Удаляем программу из базы данных по ID
                    int result = await App.DatabaseProgram.DeleteProgramAsync(program.IdBirdInMySQL); // Используем IdBirdInMySQL

                    if (result > 0)
                    {
                        await LoadFavoritePrograms(); // Обновляем список
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", "Программа не найдена для удаления.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", $"Ошибка при удалении программы: {ex.Message}", "OK");
                }
            }
        }
    }
}
