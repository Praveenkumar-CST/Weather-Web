using Microsoft.AspNetCore.Mvc;
using WeatherBackend.Models;
using WeatherBackend.Repositories;
using System;
using System.Threading.Tasks;

namespace WeatherBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFavoritesController : ControllerBase
    {
        private readonly UserFavoritesRepository _repository;

        public UserFavoritesController(UserFavoritesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            try
            {
                var favorites = await _repository.GetFavoritesAsync();
                return Ok(favorites);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving data.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] UserFavorites favorite)
        {
            if (favorite == null || string.IsNullOrWhiteSpace(favorite.CityName))
            {
                return BadRequest(new { message = "City name is required." });
            }

            try
            {
                favorite.AddedOn = DateTime.UtcNow; // Set the timestamp

                await _repository.AddFavoriteAsync(favorite);

                return Ok(new { message = "City saved successfully!", favorite });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error saving data.", error = ex.Message });
            }
        }
    }
}
