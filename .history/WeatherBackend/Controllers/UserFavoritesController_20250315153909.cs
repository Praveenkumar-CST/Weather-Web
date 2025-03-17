using Microsoft.AspNetCore.Mvc;
using WeatherBackend.Models;
using WeatherBackend.Repositories;

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
            var favorites = await _repository.GetFavoritesAsync();
            return Ok(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(UserFavorites favorite)
        {
            await _repository.AddFavoriteAsync(favorite);
            return CreatedAtAction(nameof(GetFavorites), new { id = favorite.Id }, favorite);
        }
    }
}
