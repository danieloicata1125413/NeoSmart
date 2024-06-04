using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class Session : ISoftDetete
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

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Hora de Inicio")]
        public TimeSpan? TimeStart { get; set; }

        [Display(Name = "Hora de Finalización")]
        public TimeSpan? TimeEnd { get; set; }

        [Display(Name = "Virtual")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Type { get; set; }

        [Display(Name = "Link")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string? Link { get; set; } = null!;

        [Display(Name = "Estado")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SessionStatusId { get; set; }
        public SessionStatus? SessionStatus { get; set; }

        public ICollection<SessionInscriptionTemporal>? SessionInscriptionTemporals { get; set; }

        [Display(Name = "Temporales")]
        public int SessionInscriptionTemporalsNumber => SessionInscriptionTemporals == null ? 0 : SessionInscriptionTemporals.Count;
        public ICollection<SessionInscription>? SessionInscriptions { get; set; }

        [Display(Name = "Inscritos")]
        public int SessionInscriptionsNumber => SessionInscriptions == null ? 0 : SessionInscriptions.Count;

    }
}
