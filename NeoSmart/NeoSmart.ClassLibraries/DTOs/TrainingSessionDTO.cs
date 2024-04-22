using NeoSmart.ClassLibraries.Entities;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class TrainingSessionDTO
    {
        public int Id { get; set; }

        [Display(Name = "Capacitador")]
        public string? UserId { get; set; }

        public UserDTO? User { get; set; }

        [Display(Name = "Capacitador")]
        public string ExistUser => User == null ? "Sin asignar" : User!.FullName;

        [Display(Name = "Fecha de Inicio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Fecha de Finalización")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateEnd { get; set; }

        [Display(Name = "Hora de Inicio")]
        public TimeSpan? TimeStart { get; set; }

        [Display(Name = "Hora de Finalización")]
        public TimeSpan? TimeEnd { get; set; }

        [Display(Name = "Ciudad")]
        public int CityId { get; set; }

        public CityDTO? City { get; set; }

        [Display(Name = "Presencial")]
        public bool Type { get; set; }

        [Display(Name = "Link")]
        public string? Link { get; set; } = null!;
    }
}
