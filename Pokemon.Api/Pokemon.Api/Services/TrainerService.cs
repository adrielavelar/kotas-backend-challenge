using Pokemon.Api.Data;
using Pokemon.Api.Dtos;
using Pokemon.Api.Models;

namespace Pokemon.Api.Services
{
    public class TrainerService
    {
        private readonly AppDbContext _context;

        public TrainerService
        (
            AppDbContext context
        )
        {
            _context = context;
        }

        public async Task<Trainer> CreateTrainerAsync(TrainerDto trainerDto)
        {
            var trainer = new Trainer
            {
                Name = trainerDto.Name,
                Age = trainerDto.Age,
                Cpf = trainerDto.Cpf,
                CreatedAt = DateTime.UtcNow
            };

            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
            return trainer;
        }
    }
}
