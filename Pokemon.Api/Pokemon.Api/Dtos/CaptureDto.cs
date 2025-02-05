using System.ComponentModel.DataAnnotations;

namespace Pokemon.Api.Dtos
{
    public class CaptureDto
    {
        [Required(ErrorMessage = "O ID do treinador é obrigatório.")]
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "O ID do Pokémon é obrigatório.")]
        public int PokemonId { get; set; }
    }
}
