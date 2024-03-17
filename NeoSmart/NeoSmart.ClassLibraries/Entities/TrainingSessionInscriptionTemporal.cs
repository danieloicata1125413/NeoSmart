using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingSessionInscriptionTemporal
    {
        public int Id { get; set; }

        public User? User { get; set; }

        public string? UserId { get; set; }

        public int TrainingSessionId { get; set; }

        public TrainingSession? TrainingSession { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }
    }
}
