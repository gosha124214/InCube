using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace AppInCube.Classes.SQLBD
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            })
            {
                BaseAddress = new Uri("https://185.24.53.191:5162/")
            };
        }

        // Метод для регистрации пользователя
        public async Task<bool> RegisterAsync(string email)
        {
            try
            {
                var requestBody = new
                {
                    Email = email
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("weatherforecast/register", content);

                Console.WriteLine($"Response status code: {response.StatusCode}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при регистрации: {ex.Message}");
                return false;
            }
        }

        // Метод для авторизации пользователя
        public async Task<bool> LoginAsync(string email)
        {
            try
            {
                var requestBody = new
                {
                    Email = email
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("weatherforecast/login", content);

                Console.WriteLine($"Response status code: {response.StatusCode}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при авторизации: {ex.Message}");
                return false;
            }
        }

        // Метод для проверки кода
        public async Task<bool> VerifyCodeAsync(string email, string code)
        {
            try
            {
                var requestBody = new
                {
                    Email = email,
                    Code = code
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("weatherforecast/verifycode", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при проверке кода: {ex.Message}");
                return false;
            }
        }

        // Метод для получения информации о TableBird
        public async Task<TableBird> GetTableBirdAsync(uint id)
        {
            try
            {
                var tableBirdResponse = await _httpClient.GetAsync($"weatherforecast/{id}");

                if (!tableBirdResponse.IsSuccessStatusCode)
                {
                    return null; // Возвращаем null, если запрос не был успешным
                }

                return await tableBirdResponse.Content.ReadFromJsonAsync<TableBird>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null; // Возвращаем null в случае ошибки
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null; // Возвращаем null в случае ошибки
            }
        }

        // Метод для получения информации о TableProgram
        public async Task<List<TableProgram>> GetTableProgramAsync(uint id)
        {
            try
            {
                var tableProgramResponse = await _httpClient.GetAsync($"weatherforecast/dop/{id}");

                if (!tableProgramResponse.IsSuccessStatusCode)
                {
                    return null; // Возвращаем null, если запрос не был успешным
                }

                return await tableProgramResponse.Content.ReadFromJsonAsync<List<TableProgram>>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null; // Возвращаем null в случае ошибки
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null; // Возвращаем null в случае ошибки
            }
        }
        // Метод для получения общего количества записей в TableBird
        public async Task<uint?> GetTableBirdCountAllProgram()
        {
            try
            {
                var response = await _httpClient.GetAsync("weatherforecast/countallprogram");

                if (!response.IsSuccessStatusCode)
                {
                    return null; // Возвращаем null, если запрос не был успешным
                }

                return await response.Content.ReadFromJsonAsync<uint>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null;
            }
        }
    }
}
