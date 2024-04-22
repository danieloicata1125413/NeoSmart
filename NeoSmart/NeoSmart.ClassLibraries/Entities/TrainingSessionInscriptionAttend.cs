using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingSessionInscriptionAttend : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Sesión Inscrita")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int TrainingSessionInscriptionId { get; set; }
        public TrainingSessionInscription? TrainingSessionInscription { get; set; }

        [Display(Name = "Fecha y hora")]
        public DateTime? Date { get; set; }

        [Display(Name = "Firma")]
        public string? Signature { get; set; }

        [Display(Name = "Latitud")]
        [Column(TypeName = "decimal(18, 8)")]
        public Decimal? Latitude { get; set; }

        [Display(Name = "Longitud")]
        [Column(TypeName = "decimal(18, 8)")]
        public Decimal? Longitude { get; set; }

        [Display(Name = "Altitud")]
        [Column(TypeName = "decimal(18, 8)")]
        public Decimal? Altitude { get; set; }

        [Display(Name = "Precisión")]
        [Column(TypeName = "decimal(18, 8)")]
        public Decimal? Accuracy { get; set; }

        [Display(Name = "Precisión de altitud")]
        [Column(TypeName = "decimal(18, 8)")]
        public Decimal? AltitudeAccuracy { get; set; }
    }
}
