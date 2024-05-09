using NeoSmart.ClassLibraries.Entities;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class SessionDTO
    {
        public int Id { get; set; }

        [Display(Name = "Capacitador")]
        public string? UserId { get; set; }

        public UserDTO? User { get; set; }

        [Display(Name = "Capacitador")]
        public string ExistUser => User == null ? "Sin asignar" : User!.FullName;

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Hora de Inicio")]
        public TimeSpan? TimeStart { get; set; }

        [Display(Name = "Hora de Finalización")]
        public TimeSpan? TimeEnd { get; set; }

        [Display(Name = "Virtual")]
        public bool Type { get; set; }

        [Display(Name = "Link")]
        public string? Link { get; set; } = null!;

        [Display(Name = "Estado")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SessionStatusId { get; set; }
        public SessionStatus? SessionStatus { get; set; }
    }
}
