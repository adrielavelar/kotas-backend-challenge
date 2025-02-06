using Moq;
using Newtonsoft.Json;
using Pokemon.Api.Dtos;
using Pokemon.Api.Services;

public class PokemonServiceTests
{
    private readonly Mock<HttpClient> _mockHttpClient;
    private readonly PokemonService _pokemonService;

    public PokemonServiceTests()
    {
        _mockHttpClient = new Mock<HttpClient>();
        _pokemonService = new PokemonService(_mockHttpClient.Object);
    }

    [Fact]
    public async Task GetPokemonByIdAsync_ShouldReturnPokemonDetails_WhenPokemonExists()
    {
        var expectedPokemon = new DetailsDto
        {
            Id = 1,
            Name = "bulbasaur",
            Base64Sprite = "sprite_base64",
            Evolutions = new List<string> { "ivysaur", "venusaur" }
        };

        var jsonResponse = JsonConvert.SerializeObject(expectedPokemon);
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent(jsonResponse)
        };

        _mockHttpClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(mockResponse);

        var result = await _pokemonService.GetPokemonByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(expectedPokemon.Id, result.Id);
        Assert.Equal(expectedPokemon.Name, result.Name);
        Assert.Equal(expectedPokemon.Base64Sprite, result.Base64Sprite);
    }

    [Fact]
    public async Task GetRandomPokemonsAsync_ShouldReturnListOfRandomPokemons()
    {
        var expectedPokemons = new List<DetailsDto>
        {
            new DetailsDto
            {
                Id = 1,
                Name = "bulbasaur",
                Base64Sprite = "sprite_base64_1",
                Evolutions = new List<string> { "ivysaur", "venusaur" }
            },
            new DetailsDto
            {
                Id = 2,
                Name = "ivysaur",
                Base64Sprite = "sprite_base64_2",
                Evolutions = new List<string> { "venusaur" }
            }
        };

        var jsonResponse = JsonConvert.SerializeObject(expectedPokemons);
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent(jsonResponse)
        };

        _mockHttpClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(mockResponse);

        var result = await _pokemonService.GetRandomPokemonsAsync(2);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(expectedPokemons[0].Name, result[0].Name);
        Assert.Equal(expectedPokemons[1].Name, result[1].Name);
    }
}
