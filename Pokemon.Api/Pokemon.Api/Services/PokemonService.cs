using Pokemon.Api.Dtos;
using System.Text.Json;

namespace Pokemon.Api.Services
{
    public class PokemonService
    {
        private readonly HttpClient _httpClient;

        public PokemonService
        (
            HttpClient httpClient
        )
        {
            _httpClient = httpClient;
        }

        public async Task<List<DetailsDto>> GetRandomPokemonsAsync(int count = 10)
        {
            var randomPokemons = new List<DetailsDto>();

            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                int pokemonId = random.Next(1, 1000);

                var pokemonDetails = await GetPokemonByIdAsync(pokemonId);
                if (pokemonDetails != null)
                {
                    randomPokemons.Add(pokemonDetails);
                }
            }

            return randomPokemons;
        }

        public async Task<DetailsDto?> GetPokemonByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
                using JsonDocument doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                var pokemon = new DetailsDto
                {
                    Id = root.GetProperty("id").GetInt32(),
                    Name = root.GetProperty("name").GetString() ?? string.Empty,
                    Base64Sprite = await ConvertImageToBase64(root.GetProperty("sprites").GetProperty("front_default").GetString() ?? "")
                };

                pokemon.Evolutions = await GetPokemonEvolutionsAsync(id);

                return pokemon;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        private async Task<List<string>> GetPokemonEvolutionsAsync(int id)
        {
            var evolutions = new List<string>();

            try
            {
                var speciesResponse = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon-species/{id}");
                using JsonDocument speciesDoc = JsonDocument.Parse(speciesResponse);
                var speciesRoot = speciesDoc.RootElement;
                var evolutionUrl = speciesRoot.GetProperty("evolution_chain").GetProperty("url").GetString();

                if (!string.IsNullOrEmpty(evolutionUrl))
                {
                    var evolutionResponse = await _httpClient.GetStringAsync(evolutionUrl);
                    using JsonDocument evolutionDoc = JsonDocument.Parse(evolutionResponse);
                    var evolutionRoot = evolutionDoc.RootElement;

                    var chain = evolutionRoot.GetProperty("chain");
                    while (chain.TryGetProperty("species", out var species))
                    {
                        evolutions.Add(species.GetProperty("name").GetString() ?? "");
                        if (chain.TryGetProperty("evolves_to", out var evolvesTo) && evolvesTo.GetArrayLength() > 0)
                        {
                            chain = evolvesTo[0];
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            catch (HttpRequestException)
            {

            }

            return evolutions;
        }

        private async Task<string> ConvertImageToBase64(string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return string.Empty;

            var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
            return Convert.ToBase64String(imageBytes);
        }
    }
}
