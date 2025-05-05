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

        private static readonly byte[] AesKey = Encoding.UTF8.GetBytes("0123456789abcdef"); // 16 байт
        private static readonly byte[] AesIV = Encoding.UTF8.GetBytes("abcdef9876543210");  // 16 байт

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

        public async Task<bool> SendCodeAsync(string email, bool isLogin)
        {
            try
            {
                var requestBody = new
                {
                    Email = email,
                    IsLogin = isLogin
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Убедитесь, что URL-адрес правильный
                var response = await _httpClient.PostAsync($"https://185.24.53.191:5162/weatherforecast/sendcode", content);

                Console.WriteLine($"Response status code: {response.StatusCode}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отправке кода: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> VerifyCodeAsync(string email, string code, bool isLogin)
        {
            try
            {
                var requestBody = new
                {
                    Email = email,
                    Code = code,
                    IsLogin = isLogin
                };

                var json = JsonSerializer.Serialize(requestBody);
                var encryptedJson = Encrypt(json);

                var content = new StringContent(encryptedJson, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("auth/verifycode", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при проверке кода: {ex.Message}");
                return false;
            }
        }

        private string Encrypt(string plainText)
        {
            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = AesKey;
                aes.IV = AesIV;
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                var plainBytes = Encoding.UTF8.GetBytes(plainText);

                byte[] encryptedBytes;
                using (var ms = new System.IO.MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.FlushFinalBlock();
                        encryptedBytes = ms.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedBytes);
            }
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
