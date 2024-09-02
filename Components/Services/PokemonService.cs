using PokeDexter.Components.Models;
using System.Text.Json;

namespace PokeDexter.Components.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly HttpClient _httpClient;

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Pokemon>> GetPokemonListAsync(int limit, int offset)
        {
            var response = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon?limit={limit}&offset={offset}");
            var pokemonResponse = JsonSerializer.Deserialize<PokemonResponse>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var pokemonList = new List<Pokemon>();
            foreach (var result in pokemonResponse.Results)
            {
                var pokemon = new Pokemon { Name = result.Name }; // Only store the name for fast loading
                pokemonList.Add(pokemon);
            }
            return pokemonList;
        }

        public async Task<Pokemon> GetPokemonDetailsAsync(string name)
        {
            var response = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{name}");
            var pokemon = JsonSerializer.Deserialize<Pokemon>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            pokemon.ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/{pokemon.Id}.png";
            return pokemon;
        }


        public async Task<int> GetTotalPokemonCountAsync()
        {
            var response = await _httpClient.GetStringAsync("https://pokeapi.co/api/v2/pokemon?limit=100000"); // Get a high limit to ensure we get the total count
            var pokemonResponse = JsonSerializer.Deserialize<PokemonResponse>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return pokemonResponse.Count;
        }

        //public async Task<HttpResponseMessage> SaveFavoritesAsync(List<Pokemon> favorites)
        //{
        //    return await _httpClient.PostAsJsonAsync("api/favorites/save", favorites);
        //}

    }
}

