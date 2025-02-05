using Microsoft.AspNetCore.Mvc;
using Pokemon.Api.Dtos;
using Pokemon.Api.Services;

namespace Pokemon.Api.Controllers
{
    [Route("api/pokemons")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonService _pokemonService;

        public PokemonController(PokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("random")]
        [ProducesResponseType(typeof(List<DetailsDto>), 200)]
        public async Task<ActionResult<List<DetailsDto>>> GetRandomPokemons()
        {
            var pokemons = await _pokemonService.GetRandomPokemonsAsync();
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DetailsDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<DetailsDto>> GetPokemon(int id)
        {
            var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
            if (pokemon == null)
            {
                return NotFound(new { message = "Pokémon não encontrado." });
            }

            return Ok(pokemon);
        }
    }
}
