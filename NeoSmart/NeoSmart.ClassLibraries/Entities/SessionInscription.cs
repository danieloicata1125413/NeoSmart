using NeoSmart.ClassLibraries.Enum;
using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class SessionInscription : ISoftDetete
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }

        public int SessionId { get; set; }
        public Session? Session { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        [Display(Name = "Certificado")]
        public string? Certificate { get; set; } = null!;

        public int SessionInscriptionStatusId { get; set; }
        public SessionInscriptionStatus? SessionInscriptionStatus { get; set; }

        public ICollection<SessionInscriptionAttend>? SessionInscriptionAttends { get; set; }

     
        [Display(Name = "Asistencia")]
        public bool SessionInscriptionAttendsExist => SessionInscriptionAttends == null || SessionInscriptionAttends.Count == 0 ? false : true;
       
        public ICollection<SessionInscriptionExam>? SessionInscriptionExams { get; set; }

        [Display(Name = "Diligenciados")]
        public int SessionInscriptionExamsNumber => SessionInscriptionExams == null ? 0 : SessionInscriptionExams.Count;
    }
}
