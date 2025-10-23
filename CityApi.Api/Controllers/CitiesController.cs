using Microsoft.AspNetCore.Mvc;
using CityApi.Models.Dtos;
using CityApi.Contracts;

namespace CityApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public partial class CitiesController(ICityService cityService) : ControllerBase
    {
        private readonly ICityService _cityService = cityService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetAsync([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Please provide a valid city name");
            }

            var results = (await _cityService.GetAsync(name)).ToList();
            if (results == null || results.Count == 0)
            {
                return NotFound();
            }

            return results;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CityDto>> CreateAsync([FromBody] CreateCityDto city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _cityService.CreateAsync(city);

            return CreatedAtAction("Get", new { id = result.Id }, result);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] UpdateCityDto city)
        {
            if (id != city.Id)
            {
                return BadRequest("Id mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cityToUpdate = await _cityService.GetByIdAsync(id);
            if (cityToUpdate == null)
            {
                return NotFound();
            }

            await _cityService.UpdateAsync(city, cityToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var city = await _cityService.GetByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            await _cityService.DeleteAsync(city);

            return NoContent();
        }
    }
}
