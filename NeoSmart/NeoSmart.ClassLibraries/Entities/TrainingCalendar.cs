using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingCalendar
    {
        public int Id { get; set; }

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

        [Display(Name = "Capacitador")]
        public string? UserId { get; set; }

        public User? User { get; set; }

        [Display(Name = "Temas")]
        public string ExistUser => User == null ? "Sin asignar" : User!.FullName;

        [Display(Name = "Capacitación")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int TrainingId { get; set; }

        public Training? Training { get; set; }

        [Display(Name = "Ciudad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CityId { get; set; }

        public City? City { get; set; }

        [Display(Name = "Presencial")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Type { get; set; }

        [Display(Name = "Habilitado")]
        public bool Status { get; set; }

        public ICollection<TemporalInscription>? TemporalInscriptions { get; set; }
        public ICollection<Inscription>? Inscriptions { get; set; }
    }
}
