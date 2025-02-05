using System.ComponentModel.DataAnnotations;

namespace Pokemon.Api.Models
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
}
