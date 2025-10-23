using AutoMapper;
using CityApi.Contracts;
using CityApi.Models.Dtos;
using CityApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityApi.Services;

public class CityService : ICityService
{
    private readonly ICityContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpProvider _httpProvider;

    public CityService(ICityContext context, IMapper mapper, IHttpProvider httpProvider)
    {
        _context = context;
        _mapper = mapper;
        _httpProvider = httpProvider;
    }

    public async Task<IEnumerable<CityDto>> GetAsync(string name)
    {
        var cities = await _context.Cities.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        var cityDtos = _mapper.Map<IEnumerable<CityDto>>(cities).ToList();
        var externalApiCalls = new List<Task>();
        foreach (var city in cityDtos)
        {
            if (string.IsNullOrEmpty(city.Country))
            {
                continue;
            }

            externalApiCalls.Add(_httpProvider.GetCountryDetailsAync(city));
            externalApiCalls.Add(_httpProvider.GetWeatherAsync(city));
        }

        await Task.WhenAll(externalApiCalls);

        return cityDtos;
    }

    public async Task<City?> GetByIdAsync(long id)
    {
        return await _context.Cities.FindAsync(id);
    }

    public async Task<CityDto> CreateAsync(CreateCityDto city)
    {
        var cityToCreate = _mapper.Map<CreateCityDto, City>(city);
        _context.Cities.Add(cityToCreate);
        await _context.SaveChangesAsync();

        return _mapper.Map<City, CityDto>(cityToCreate);
    }

    public async Task UpdateAsync(UpdateCityDto city, City cityToUpdate)
    {
        _mapper.Map(city, cityToUpdate);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(City city)
    {
        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();
    }
}
