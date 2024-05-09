using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class UserNotification : ISoftDetete
    {
        [Key]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserId { get; set; }

        [Display(Name = "User")]
        public User? User { get; set; }

        [Display(Name = "Modulo")]
        public string Module { get; set; }

        [Display(Name = "Module")]
        public Guid? ModuleId { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Title { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        [Display(Name = "Imagen")]
        public string? Image { get; set; }

        [Display(Name = "Redirecciona")]
        public string? Redirect { get; set; }

        [Display(Name = "Notificado")]
        public bool Notify { get; set; }
    }
}
