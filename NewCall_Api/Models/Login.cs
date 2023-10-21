using System.ComponentModel.DataAnnotations;

namespace NewCall_Api.Models
{
    public class Login
    {
        [Required(ErrorMessage = "User Name is required")]
        public required string identifiant { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string password { get; set; }
    }
}