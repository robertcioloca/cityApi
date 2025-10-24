namespace CityApi.Core.Models.API;

public class WeatherApiResponse
{
    public required MainInfo Main { get; set; }
    public required WindInfo Wind { get; set; }
    public required string Name { get; set; }

    public class MainInfo
    {
        public double Temp { get; set; }
        public double Feels_Like { get; set; }
        public double Temp_Min { get; set; }
        public double Temp_Max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }

    public class WindInfo
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
    }
}
