using Microsoft.EntityFrameworkCore;
using Pokemon.Api.Data;
using Pokemon.Api.Dtos;
using Pokemon.Api.Models;

namespace Pokemon.Api.Services
{
    public class CaptureService
    {
        private readonly AppDbContext _context;
        private readonly PokemonService _pokemonService;

        public CaptureService(AppDbContext context, PokemonService pokemonService)
        {
            _context = context;
            _pokemonService = pokemonService;
        }

        public async Task<Capture?> CapturePokemonAsync(CaptureDto captureDto)
        {
            var trainer = await _context.Trainers.FindAsync(captureDto.TrainerId);
            if (trainer == null)
            {
                return null;
            }

            var pokemon = await _pokemonService.GetPokemonByIdAsync(captureDto.PokemonId);
            if (pokemon == null)
            {
                return null;
            }

            var capture = new Capture
            {
                TrainerId = trainer.Id,
                PokemonId = pokemon.Id,
                PokemonName = pokemon.Name,
                CreatedAt = DateTime.UtcNow
            };

            _context.Captures.Add(capture);
            await _context.SaveChangesAsync();

            return capture;
        }

        public async Task<List<Capture>> GetCapturedPokemonsAsync()
        {
            return await _context.Captures
                .Include(c => c.Trainer)
                .ToListAsync();
        }
    }
}
