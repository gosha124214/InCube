using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLite;
using AppInCube.View.Pages.Favorites.UnderPagesInFavorites;

namespace AppInCube.View.Pages.Favorites
{
    public partial class FavoritesPage : ContentPage
    {
        public FavoritesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadFavoritePrograms(); // ��������� ��������� ��� ��������� ��������
        }

        private async Task LoadFavoritePrograms()
        {
            try
            {
                var favoritePrograms = await App.Database.GetProgramsAsync(); // ��������������, ��� ���� ����� ���������� ��� ���������

                // �������� ������ � ����������
                ProgramsListView.ItemsSource = favoritePrograms; // �������� ������ � CollectionView
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("������", $"������ ��� �������� ������: {ex.Message}", "OK");
            }
        }


        private async void OnProgramTapped(object sender, EventArgs e)
        {

            // ��������� �������������� � ������� ���������
            this.IsEnabled = false;

            var tappedItem = (sender as StackLayout).BindingContext as SQLliteTableBaseInfo;
            if (tappedItem != null)
            {
                await Navigation.PushAsync(new NewPage1(tappedItem));
            }


            this.IsEnabled = true; // ������������ �������������� � ������� ���������
        }


        private async void DeleteProgram(SQLliteTableBaseInfo program)
        {
            // ������������� ��������
            bool confirm = await DisplayAlert("�������������", "�� �������, ��� ������ ������� ��� ���������?", "��", "���");
            if (confirm)
            {
                try
                {
                    // ������� ��������� �� ���� ������ �� ID
                    int result = await App.Database.DeleteProgramAsync(program.IdBirdInMySQL); // ���������� ProgramId

                    if (result > 0)
                    {
                        await LoadFavoritePrograms(); // ��������� ������
                    }
                    else
                    {
                        await DisplayAlert("������", "��������� �� ������� ��� ��������.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("������", $"������ ��� �������� ���������: {ex.Message}", "OK");
                }
            }
        }
        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            // �������� ������ ���������, ������� ����� �������
            var button = sender as Button;
            var program = button?.CommandParameter as SQLliteTableBaseInfo;

            if (program != null)
            {
                DeleteProgram(program);
            }
        }
    }
}