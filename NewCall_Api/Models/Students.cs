using System.ComponentModel.DataAnnotations;

namespace NewCall_Api.Models
{
    public class Students
    {
        [Key]
        [Required]
        public int id { get; set; }

        [StringLength(50)]
        public string firstname { get; set; }

        [StringLength(50)]
        public string lastname { get; set; }

        [StringLength(20)]
        public string statut { get; set; }
        public Students(int id, string firstname, string lastname, string statut)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.statut = statut;
        }
    }
}
