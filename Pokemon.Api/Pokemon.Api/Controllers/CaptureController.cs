using Microsoft.AspNetCore.Mvc;
using Pokemon.Api.Dtos;
using Pokemon.Api.Models;
using Pokemon.Api.Services;

namespace Pokemon.Api.Controllers
{
    [Route("api/pokemons")]
    [ApiController]
    public class CaptureController : ControllerBase
    {
        private readonly CaptureService _captureService;

        public CaptureController
        (
            CaptureService captureService
        )
        {
            _captureService = captureService;
        }

        [HttpPost("capture")]
        [ProducesResponseType(typeof(Capture), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Capture>> CapturePokemon([FromBody] CaptureDto captureDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var capture = await _captureService.CapturePokemonAsync(captureDto);
            if (capture == null)
            {
                return NotFound(new { message = "Treinador ou Pokémon não encontrado." });
            }

            return CreatedAtAction(nameof(CapturePokemon), new { id = capture.Id }, capture);
        }

        [HttpGet("captured")]
        [ProducesResponseType(typeof(List<Capture>), 200)]
        public async Task<ActionResult<List<Capture>>> GetCapturedPokemons()
        {
            var captures = await _captureService.GetCapturedPokemonsAsync();
            return Ok(captures);
        }
    }
}
