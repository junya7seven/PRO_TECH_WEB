using System;
using System.Net.Http;
using System.Threading.Tasks;
using YourNamespace.Controllers;
namespace asdasdsad
{
    public class GetRandomNumbers
    {
        // Создание двух объектов для хранения модифицированной строки и самого рандомного числа
        public class RandomResult
        {
            public string ModifiedString { get; set; }
            public int RandomNumber { get; set; }
        }
        // Метод для получения модифицированной строки и рандомного числа
        public static async Task<RandomResult> Random(string inputString)
        {
            string apiKey = "159d8c1b-7d0e-4099-ac84-e3490aee3303"; // Мой API
            // Получение случайного числа, указывающего позицию символа для удаления
            int randomPosition = await GetRandomNumber(apiKey, inputString.Length); 

            if (randomPosition == -1)
            {
                randomPosition = new Random().Next(0, inputString.Length);  // Если не удалось получить число от random.org, используется метод Random из .NET
            }
            // Возврат объекта RandomResult с модифицированной строкой и рандомным числом
            string modifiedString = RemoveCharacterAt(inputString, randomPosition);
            return new RandomResult
            {
                ModifiedString = modifiedString,
                RandomNumber = randomPosition
            };
        }
        // Метод для получения случайного числа через API random.org

        static async Task<int> GetRandomNumber(string apiKey, int max)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = $"https://api.random.org/json-rpc/2/invoke"; //URL для запроса к random.org
                    string requestBody = $"{{\"jsonrpc\": \"2.0\", \"method\": \"generateIntegers\", \"params\": {{\"apiKey\": \"{apiKey}\", \"n\": 1, \"min\": 0, \"max\": {max - 1}, \"replacement\": false}}, \"id\": 42}}"; // Формирование тела запроса

                    HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(requestBody));

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();  // Получение ответа
                        int randomValue = Newtonsoft.Json.JsonConvert.DeserializeObject<RandomApiResponse>(json).result.random.data[0]; // Извлечение случайного числа из ответа

                        return randomValue; // Возвращение случайного числа
                    }
                    else
                    {
                        return -1; // Если запрос не удался, возвращается -1
                    }
                }
            }
            catch (Exception ex)
            {
                return -1; // В случае ошибки также возвращается -1
            }
        }
        // Метод для удаления символа из строки по указанной позиции
        static string RemoveCharacterAt(string input, int index)
        {
            if (index < 0 || index >= input.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Некорректная позиция для удаления символа"); // Проверка корректности позиции символа для удаления
            }

            return input.Remove(index, 1); // Удаление символа из строки
        }
    }
}
