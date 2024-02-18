namespace Pro_Tech_Web.ModifiedString.GetRandomNumb
{
    public class RandomNumber
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public int RandomNumb { get; set; }
        public RandomNumber(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }
        public async Task<int> GetRandomNumberAsync(string inputString)
        {
            var apiUrl = $"{_configuration["ApiUrl"]}?min={1}&max={inputString.Length-1}";
            if (string.IsNullOrEmpty(apiUrl))
            {
                throw new InvalidOperationException("ApiUrl is missing in appsettings.json.");
            }
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var randomNumberStr = await response.Content.ReadAsStringAsync();
                    var des = Newtonsoft.Json.JsonConvert.DeserializeObject<int[]>(randomNumberStr);
                    return des[0];
                } 
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("An error occurred while sending the request to the API.", ex);
            }
            return -1;
        }
        public async Task<string> RemoveChar(string input)
        {
            int indexAsync = await GetRandomNumberAsync(input);
            if (indexAsync >= 0)
            {
                var result = await RemoveCharAtIndex(input, indexAsync);
                var response = new
                {
                    result,
                    indexAsync
                };
                return response.ToString();
            }
            else
            {
                Random random = new Random();
                int index = random.Next(0, input.Length);
                var result = await RemoveCharAtIndex(input, index);
                var response = new
                {
                    result,
                    index
                };
                return response.ToString();
            }
        }

        async Task<string> RemoveCharAtIndex(string input, int index)
        {
            if (index < 0 || index >= input.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            return input.Remove(index, 1);
        }
    }
}
