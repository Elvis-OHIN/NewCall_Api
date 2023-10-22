using System.ComponentModel.DataAnnotations;

namespace NewCall_Api.Models
{
    public class Admins
    {
        [Key]
        [Required]
        public int id { get; set; }

        [Required(ErrorMessage = "L'identifiant est requis.")]
        [StringLength(50, ErrorMessage = "L'identifiant doit comporter au maximum 50 caractères.")]
        public required string identifiant { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit comporter au moins 6 caractères.")]
        public required string password { get; set; }

    }
}
