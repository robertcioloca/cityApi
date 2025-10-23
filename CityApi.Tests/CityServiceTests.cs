using AutoMapper;
using CityApi.Contracts;
using CityApi.MappingProfiles;
using CityApi.Models.Dtos;
using CityApi.Models.Entities;
using CityApi.Services;
using Moq;

namespace CityApi.Tests;

public class CityServiceTests
{
    private readonly IMapper _mapper;

    public CityServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CityProfile>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task GetCity_ReturnsCorrectCity()
    {
        var mockContext = SetupMockContext();
        var mockHttp = SetupMockHttpProvider();
        var service = new CityService(mockContext.Object, _mapper, mockHttp.Object);

        var result = (await service.GetAsync("Lon")).ToList();

        Assert.NotNull(result);
        Assert.True(result.Count == 1, "Should contain 1 country");
        Assert.Equal("London", result[0].Name);
        Assert.Equal("United Kingdom", result[0].Country);
    }

    [Fact]
    public async Task GetNonExistingCityByName_ReturnsEmpty()
    {
        var mockContext = SetupMockContext();
        var mockHttp = SetupMockHttpProvider();
        var service = new CityService(mockContext.Object, _mapper, mockHttp.Object);

        var deleted = (await service.GetAsync("Hamburg")).ToList();

        Assert.Empty(deleted);
    }

    [Fact]
    public async Task GetCity_ReturnsCountryAndWeatherData()
    {
        var mockContext = SetupMockContext();
        var mockHttp = SetupMockHttpProvider();
        var service = new CityService(mockContext.Object, _mapper, mockHttp.Object);

        var result = (await service.GetAsync("Lon")).ToList();

        Assert.NotNull(result);
        Assert.NotNull(result[0].Weather);
        Assert.Equal("USD", result[0].CurrencyCode);
    }

    private static Mock<ICityContext> SetupMockContext()
    {
        var cities = new List<City>
        {
            new() { Id = 1, Name = "London", State = "", Country = "United Kingdom" },
            new() { Id = 2, Name = "Paris", State = "", Country = "France" }
        }.AsQueryable();
        var mockSet = DbSetMock.Create(cities);
        var mockContext = new Mock<ICityContext>();
        mockContext.Setup(c => c.Cities).Returns(mockSet.Object);

        return mockContext;
    }

    private static Mock<IHttpProvider> SetupMockHttpProvider()
    {
        var mockHttp = new Mock<IHttpProvider>();
        mockHttp
            .Setup(h => h.GetCountryDetailsAync(It.IsAny<CityDto>()))
            .Callback<CityDto>((dto) =>
            {
                dto.TwoDigitCountryCode = "2digitCountryCode";
                dto.ThreeDigitCountryCode = "3digitCountryCode";
                dto.CurrencyCode = "USD";
            })
            .Returns(Task.CompletedTask);

        mockHttp
            .Setup(h => h.GetWeatherAsync(It.IsAny<CityDto>()))
            .Callback<CityDto>((dto) =>
            {
                dto.Weather = new WeatherDto
                {
                    FeesLike = 10,
                    TempMin = 1,
                    TempMax = 12,
                    Humidity = 70,
                    Pressure = 980,
                    Temp = 12,
                };
            })
            .Returns(Task.CompletedTask);
        return mockHttp;
    }
}
