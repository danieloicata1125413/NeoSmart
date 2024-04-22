using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingSession : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Capacitador")]
        public string? UserId { get; set; }

        public User? User { get; set; }

        [Display(Name = "Capacitador")]
        public string ExistUser => User == null ? "Sin asignar" : User!.FullName;

        [Display(Name = "Capacitación")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int TrainingId { get; set; }
        public Training? Training { get; set; }

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

        public City? City { get; set; }

        [Display(Name = "Presencial")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Type { get; set; }

        [Display(Name = "Link")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string? Link { get; set; } = null!;
        public ICollection<TrainingSessionInscriptionTemporal>? TrainingSessionInscriptionTemporals { get; set; }
        public ICollection<TrainingSessionInscription>? TrainingSessionInscriptions { get; set; }

    }
}
