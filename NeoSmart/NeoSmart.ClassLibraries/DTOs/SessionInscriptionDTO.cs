using NeoSmart.ClassLibraries.Entities;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class SessionInscriptionDTO
    {
        public int Id { get; set; }

        public int SessionInscriptionStatusId { get; set; }

        public SessionInscriptionStatus? SessionInscriptionStatus { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}