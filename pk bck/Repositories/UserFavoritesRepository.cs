using MongoDB.Driver;
using WeatherBackend.Data;
using WeatherBackend.Models;

namespace WeatherBackend.Repositories
{
    public class UserFavoritesRepository
    {
        private readonly AppDbContext _context;

        public UserFavoritesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserFavorites>> GetFavoritesAsync()
        {
            return await _context.UserFavorites.Find(_ => true).ToListAsync();
        }

        public async Task AddFavoriteAsync(UserFavorites favorite)
        {
            await _context.UserFavorites.InsertOneAsync(favorite);
        }
    }
}
