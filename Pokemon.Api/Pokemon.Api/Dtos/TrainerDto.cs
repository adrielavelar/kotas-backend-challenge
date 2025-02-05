using System.ComponentModel.DataAnnotations;

namespace Pokemon.Api.Dtos
{
    public class TrainerDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(10, 120, ErrorMessage = "A idade deve estar entre 10 e 120 anos.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos numéricos.")]
        public string Cpf { get; set; } = string.Empty;
    }
}
