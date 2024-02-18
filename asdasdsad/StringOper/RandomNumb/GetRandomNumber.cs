using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace asdasdsad.StringOper.RandomNumb
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
                randomNumber = await GetRandomNumberFromAPI(apiUrl, inputString);
            }
            catch (Exception)
            {
                randomNumber = GenerateRandomNumber(inputString);
            }

            string modifiedString = RemoveChar(inputString, randomNumber);

            var response = new
            {
                randomNumber,
                modifiedString
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(response);
        }

        private async Task<int> GetRandomNumberFromAPI(string apiUrl, string inputString)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{apiUrl}?min=0&max={inputString.Length - 1}&count=1");

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var randomApiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int[]>(result);
                return randomApiResponse[0];
            }
            else
            {
                throw new Exception("Failed to get random number from external API");

            }

        }

        private int GenerateRandomNumber(string inputString) // Запасной вариант, если не удалось запросить рандомное число
        {
            Random random = new Random();
            return random.Next(0, inputString.Length - 1);
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
