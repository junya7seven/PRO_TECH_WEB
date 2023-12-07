namespace asdasdsad
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
    public class RandomApiResponse
    {
        public Result result { get; set; }
    }

    public class Result
    {
        public RandomData random { get; set; }
    }

    public class RandomData
    {
        public int[] data { get; set; }
    }
}
