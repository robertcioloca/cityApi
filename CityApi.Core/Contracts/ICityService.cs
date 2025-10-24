using CityApi.Core.Models.Dtos;
using CityApi.Core.Models.Entities;

namespace CityApi.Core.Contracts;

public interface ICityService
{
    public Task<IEnumerable<CityDto>> GetAsync(string name);
    public Task<City?> GetByIdAsync(long id);
    public Task<CityDto> CreateAsync(CreateCityDto city);
    public Task UpdateAsync(UpdateCityDto city, City cityToUpdate);
    public Task DeleteAsync(City city);
}
