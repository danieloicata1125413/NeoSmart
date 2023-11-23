using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TemporalInscription
    {
        public int Id { get; set; }

        public User? User { get; set; }

        public string? UserId { get; set; }

        public int TrainingCalendarId { get; set; }

        public TrainingCalendar? TrainingCalendar { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }
    }
}
