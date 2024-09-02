using PokeDexter.Components.Models;

namespace PokeDexter.Components.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetPokemonListAsync(int limit, int offset);
        Task<Pokemon> GetPokemonDetailsAsync(string name);
        Task<int> GetTotalPokemonCountAsync();
       // Task<HttpResponseMessage> SaveFavoritesAsync(List<Pokemon> favorites);
    }
}
