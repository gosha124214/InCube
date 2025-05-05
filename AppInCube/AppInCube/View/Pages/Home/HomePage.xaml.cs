using System;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLBD;

namespace AppInCube.View.Pages.Home
{
    public partial class HomePage : ContentPage
    {
        private string currentEmail;
        private bool isLoginMode;

        private readonly ApiService _apiService = new ApiService();

        public HomePage()
        {
            InitializeComponent();
        }

        private void OnLoginChoiceClicked(object sender, EventArgs e)
        {
            isLoginMode = true;
            ShowEmailPanel("Вход по электронной почте");
        }

        private void OnRegisterChoiceClicked(object sender, EventArgs e)
        {
            isLoginMode = false;
            ShowEmailPanel("Регистрация по электронной почте");
        }

        private void ShowEmailPanel(string title)
        {
            titleLabel.Text = title;
            choicePanel.IsVisible = false;
            emailPanel.IsVisible = true;
            codePanel.IsVisible = false;
            infoLabel.IsVisible = false;

            emailEntry.Text = "";
            codeEntry.Text = "";

            sendCodeButton.IsEnabled = true;
            emailEntry.IsEnabled = true;
        }

        private async void OnSendCodeButtonClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите корректную электронную почту.", "ОК");
                return;
            }

            currentEmail = email;

            bool sendSuccess = await _apiService.SendCodeAsync(email, isLoginMode);
            if (!sendSuccess)
            {
                await DisplayAlert("Ошибка", "Не удалось отправить код. Попробуйте позже.", "ОК");
                return;
            }

            await DisplayAlert("Код отправлен", $"Код подтверждения отправлен на {currentEmail}.", "ОК");

            emailPanel.IsVisible = false;
            codePanel.IsVisible = true;
            infoLabel.IsVisible = false;
            infoLabel.Text = "";

            sendCodeButton.IsEnabled = false;
            emailEntry.IsEnabled = false;
        }

        private async void OnVerifyCodeButtonClicked(object sender, EventArgs e)
        {
            string enteredCode = codeEntry.Text?.Trim();
            if (string.IsNullOrWhiteSpace(enteredCode) || enteredCode.Length != 4)
            {
                infoLabel.Text = "Пожалуйста, введите 4-значный код.";
                infoLabel.IsVisible = true;
                return;
            }

            bool verifySuccess = await _apiService.VerifyCodeAsync(currentEmail, enteredCode, isLoginMode);
            if (verifySuccess)
            {
                string modeText = isLoginMode ? "Вы успешно вошли" : "Вы успешно зарегистрировались";
                await DisplayAlert("Успех", $"{modeText} как {currentEmail}", "ОК");

                // TODO: переход на следующий экран приложения

                ResetToStart();
            }
            else
            {
                infoLabel.Text = "Неверный код. Попробуйте ещё раз.";
                infoLabel.IsVisible = true;
            }
        }

        private void OnBackToChoiceClicked(object sender, EventArgs e)
        {
            ResetToStart();
        }

        private void ResetToStart()
        {
            titleLabel.Text = "Добро пожаловать";
            choicePanel.IsVisible = true;
            emailPanel.IsVisible = false;
            codePanel.IsVisible = false;
            infoLabel.IsVisible = false;

            emailEntry.Text = "";
            codeEntry.Text = "";

            sendCodeButton.IsEnabled = true;
            emailEntry.IsEnabled = true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}