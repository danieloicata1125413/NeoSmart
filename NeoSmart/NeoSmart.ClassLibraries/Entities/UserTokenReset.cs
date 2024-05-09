using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class UserTokenReset :ISoftDetete
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid Id { get; set; }

        [Display(Name = "UserId")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime FechaMax { get; set; }

        [MaxLength]
        public string? Token { get; set; }

        public bool Processed { get; set; }
    }
}
