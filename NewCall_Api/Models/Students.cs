using System.ComponentModel.DataAnnotations;

namespace NewCall_Api.Models
{
    public class Students
    {
        [Key]
        [Required]
        public int id { get; set; }

        [Required(ErrorMessage = "Le prénom est requis.")]
        [StringLength(50, ErrorMessage = "Le prénom doit comporter au maximum 50 caractères.")]
        public string firstname { get; set; }

        [Required(ErrorMessage = "Le nom de famille est requis.")]
        [StringLength(50, ErrorMessage = "Le nom de famille doit comporter au maximum 50 caractères.")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "Le statut est requis.")]
        [StringLength(20, ErrorMessage = "Le statut doit comporter au maximum 20 caractères.")]
        public string statut { get; set; }

       // public ICollection<Absences>? Absences { get; set; }
        public Students(int id, string firstname, string lastname, string statut)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.statut = statut;
        }
    }
}
