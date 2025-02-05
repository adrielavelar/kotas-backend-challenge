using System.ComponentModel.DataAnnotations;

namespace Pokemon.Api.Models
{
    public class Pokemon : Entity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string SpriteBase64 { get; set; } = string.Empty;

        public int? EvolutionId { get; set; }

        public Pokemon? Evolution { get; set; }
    }
}
