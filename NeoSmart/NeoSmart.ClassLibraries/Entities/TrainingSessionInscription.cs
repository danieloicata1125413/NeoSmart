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

        public int SessionId { get; set; }
        public Session? Session { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        [Display(Name = "Certificado")]
        public string? Certificate { get; set; } = null!;

        public int TrainingSessionInscriptionStatusId { get; set; }
        public TrainingSessionInscriptionStatus? TrainingSessionInscriptionStatus { get; set; }
        public ICollection<TrainingSessionInscriptionAttend>? TrainingSessionInscriptionAttends { get; set; }

        [Display(Name = "Asistencia")]
        public bool TrainingSessionInscriptionAttendsExist => TrainingSessionInscriptionAttends == null || TrainingSessionInscriptionAttends.Count == 0 ? false : true;
        public ICollection<TrainingSessionInscriptionExam>? TrainingSessionInscriptionExams { get; set; }

        [Display(Name = "Diligenciados")]
        public int TrainingSessionInscriptionExamsNumber => TrainingSessionInscriptionExams == null ? 0 : TrainingSessionInscriptionExams.Count;
    }
}
