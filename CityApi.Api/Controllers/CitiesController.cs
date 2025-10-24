using Microsoft.AspNetCore.Mvc;
using CityApi.Core.Contracts;
using CityApi.Core.Models.Dtos;

namespace CityApi.Api.Controllers
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

            try
            {
                var results = (await _cityService.GetAsync(name)).ToList();
                if (results == null || results.Count == 0)
                {
                    return NotFound();
                }

                return results;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CityDto>> CreateAsync([FromBody] CreateCityDto city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _cityService.CreateAsync(city);

                return CreatedAtAction("Get", new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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

            try
            {
                var cityToUpdate = await _cityService.GetByIdAsync(id);
                if (cityToUpdate == null)
                {
                    return NotFound();
                }

                await _cityService.UpdateAsync(city, cityToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                var city = await _cityService.GetByIdAsync(id);
                if (city == null)
                {
                    return NotFound();
                }

                await _cityService.DeleteAsync(city);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
