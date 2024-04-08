using NeoSmart.ClassLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingSessionAttend : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Sesión")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int TrainingSessionId { get; set; }
        public TrainingSession? TrainingSession { get; set; }

        [Display(Name = "Asistente")]
        public string? UserId { get; set; }
        public User? User { get; set; }

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
