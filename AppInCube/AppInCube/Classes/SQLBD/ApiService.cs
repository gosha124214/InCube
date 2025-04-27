using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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
            });
        }

        public async Task<TableBird> GetTableBirdAsync(uint id)
        {
            try
            {
                var tableBirdResponse = await _httpClient.GetAsync($"https://185.24.53.191:5162/weatherforecast/{id}");

                if (!tableBirdResponse.IsSuccessStatusCode)
                {
                    return null; // Возвращаем null, если запрос не был успешным
                }

                return await tableBirdResponse.Content.ReadFromJsonAsync<TableBird>();
            }
            catch (HttpRequestException ex)
            {
                // Обработка исключения, связанного с HTTP-запросом
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null; // Возвращаем null в случае ошибки
            }
            catch (Exception ex)
            {
                // Обработка исключения, связанного с HTTP-запросом
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null; // Возвращаем null в случае ошибки
            }
        }
        public async Task<List<TableProgram>> GetTableProgramAsync(uint id)
        {
            try
            {
                var tableProgramResponse = await _httpClient.GetAsync($"https://185.24.53.191:5162/weatherforecast/dop/{id}");

                if (!tableProgramResponse.IsSuccessStatusCode)
                {
                    return null; // Возвращаем null, если запрос не был успешным
                }

                return await tableProgramResponse.Content.ReadFromJsonAsync<List<TableProgram>>();
            }
            catch (HttpRequestException ex)
            {
                // Обработка исключения, связанного с HTTP-запросом
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null; // Возвращаем null в случае ошибки
            }
            catch (Exception ex)
            {
                // Обработка исключения, связанного с HTTP-запросом
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null; // Возвращаем null в случае ошибки
            }
        }

        public async Task<uint?> GetTableBirdCountAllProgram()
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://185.24.53.191:5162/weatherforecast/countallprogram");

                // Проверяем, успешен ли запрос
                if (!response.IsSuccessStatusCode)
                {
                    return null; // Возвращаем null, если запрос не был успешным
                }

                // Читаем содержимое ответа и десериализуем его в целое число
                return await response.Content.ReadFromJsonAsync<uint>();
            }
            catch (HttpRequestException ex)
            {
                // Обработка исключения, связанного с HTTP-запросом
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null; // Возвращаем null в случае ошибки
            }
            catch (Exception ex)
            {
                // Обработка исключения, связанного с HTTP-запросом
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null; // Возвращаем null в случае ошибки
            }
        }

    }
}
