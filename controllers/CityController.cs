using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsClubApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsClubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [Authorize(Policy = "All")]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cities>>> GetCities()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            return Ok(cities);
        }

        [Authorize(Policy = "All")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Cities>> GetCity(decimal id)
        {
            var city = await _cityService.GetCityByIdAsync(id);

            if (city == null)
                return NotFound();

            return Ok(city);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPost]
        public async Task<ActionResult<Cities>> PostCity(Cities city)
        {
            if (city.CityId != 0)
            {
                return BadRequest("Cannot specify CityId.");
            }

            var createdCity = await _cityService.CreateCityAsync(city);
            return CreatedAtAction(nameof(GetCity), new { id = createdCity.CityId }, createdCity);
        }



        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(decimal id, Cities city)
        {
            if (id != city.CityId)
                return BadRequest();

            var updatedCity = await _cityService.UpdateCityAsync(id, city);

            if (updatedCity == null)
                return NotFound();

            return NoContent();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(decimal id)
        {
            var result = await _cityService.DeleteCityAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
