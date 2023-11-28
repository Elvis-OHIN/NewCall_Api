using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewCall_Api.Models
{
    public class Absences
    {
        [Key]
        [Required]
        public int id { get; set; }

   
        [Required(ErrorMessage = "L'étudiant est requis.")]
        public int studentId { get; set; }

        [Required(ErrorMessage = "La date de début d'absence est requise.")]
        public DateTime startDate { get; set; }

        public DateTime? endDate { get; set; }

        public string? reason { get; set; }

        public string? comments { get; set; }
    }

}
