using NeoSmart.ClassLibraries.Enum;
using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingSessionInscription : ISoftDetete
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }

        public User? User { get; set; }

        public string? UserId { get; set; }

        public int TrainingSessionId { get; set; }
        public TrainingSession? TrainingSession { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        public int TrainingSessionInscriptionStatusId { get; set; }
        public TrainingSessionInscriptionStatus? TrainingSessionInscriptionStatus { get; set; }

        public ICollection<TrainingSessionInscriptionAttend>? TrainingSessionInscriptionAttends { get; set; }
    }
}
