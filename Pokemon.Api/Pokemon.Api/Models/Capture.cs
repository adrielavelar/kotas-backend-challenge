using System.ComponentModel.DataAnnotations;

namespace Pokemon.Api.Models
{
    public class Capture : Entity
    {
        [Required]
        public int TrainerId { get; set; }

        [Required]
        public int PokemonId { get; set; }

        public string PokemonName { get; set; } = string.Empty;

        public Trainer? Trainer { get; set; }
    }
}
