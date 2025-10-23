namespace CityApi.Models.Dtos;

public class WeatherDto
{
    public double Temp { get; set; }
    public double FeesLike { get; set; }
    public double TempMin { get; set; }
    public double TempMax { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
}
