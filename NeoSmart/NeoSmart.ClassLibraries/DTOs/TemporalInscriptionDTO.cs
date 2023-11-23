namespace NeoSmart.ClassLibraries.DTOs
{
    public class TemporalInscriptionDTO
    {
        public int Id { get; set; }

        public int trainingCalendarId { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}