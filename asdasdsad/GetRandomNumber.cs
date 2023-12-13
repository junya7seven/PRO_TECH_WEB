using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tasks;

namespace Tasks
{
    public class RandomNumberService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public RandomNumberService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<string> GetModifiedStringAsync(string inputString)
        {
            string apiUrl = _configuration["AppUrl"]; // URL
            int randomNumber;

            try
            {
                randomNumber = await GetRandomNumberFromAPI(apiUrl, inputString); // Проверка на получение рандомного числа
            }
            catch (Exception)
            {
                randomNumber = GenerateRandomNumber(inputString); // Если не удалось получить запрос
            }

            string modifiedString = RemoveChar(inputString, randomNumber); // Удаляем символ в строке

            var response = new
            {
                randomNumber,
                modifiedString
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(response); // Возвращаем значение в формате JSON 
        }

        private async Task<int> GetRandomNumberFromAPI(string apiUrl,string inputString) // Запрос рандомного числа
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{apiUrl}?min=0&max={inputString.Length-1}&count=1"); // URL из конфигуратора + параметры рандомного числа

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var randomApiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int[]>(result);
                return randomApiResponse[0]; // т.к. рандомное число выдаётся в массиве, возвращаем первый его элемент
            }
            else
            {
                throw new Exception("Failed to get random number from external API");
                
            }
            
        }

        private int GenerateRandomNumber(string inputString) // Запасной вариант, если не удалось запросить рандомное число
        {
            Random random = new Random();
            return random.Next(0, inputString.Length-1);
        }

        private string RemoveChar(string input, int index) // Проверка рандомного числа на вхождение в длину строки
        {
            if (index >= 0 && index < input.Length)
            {
                return input.Remove(index, 1);
            }
            return input;
        }
    }
}
