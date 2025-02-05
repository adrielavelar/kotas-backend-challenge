using System.ComponentModel.DataAnnotations;

namespace Pokemon.Api.Models
{
    public class Trainer : Entity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public string Cpf { get; set; } = string.Empty;
    }
}
