using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Enum;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class TrainingSessionInscriptionDTO
    {
        public int Id { get; set; }

        public int InscriptionStatusId { get; set; }

        public InscriptionStatus? InscriptionStatus { get; set; }
    }
}