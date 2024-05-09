using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class UserFirebaseToken : ISoftDetete
    {
        [Key]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserId { get; set; }

        [Display(Name = "User")]
        public User? User { get; set; }

        [Display(Name = "Dispositivo")]
        public string? device { get; set; }

        [Display(Name = "Token")]
        public string? Token { get; set; }

        [Display(Name = "Versión")]
        public string? Version { get; set; }
    }
}
