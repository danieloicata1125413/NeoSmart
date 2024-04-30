using NeoSmart.ClassLibraries.Entities;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class TrainingSessionInscriptionTemporalDTO
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}