using Microsoft.AspNetCore.Mvc;
using Pokemon.Api.Dtos;
using Pokemon.Api.Models;
using Pokemon.Api.Services;

namespace Pokemon.Api.Controllers
{
    [Route("api/trainers")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly TrainerService _trainerService;

        public TrainerController(TrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Trainer), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Trainer>> CreateTrainer([FromBody] TrainerDto trainerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainer = await _trainerService.CreateTrainerAsync(trainerDto);
            return CreatedAtAction(nameof(CreateTrainer), new { id = trainer.Id }, trainer);
        }
    }
}
