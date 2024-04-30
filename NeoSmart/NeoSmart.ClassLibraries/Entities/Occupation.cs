using NeoSmart.ClassLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.Entities
{
    public class Occupation : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Proceso")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int ProcessId { get; set; }
        public Process? Process { get; set; }

        public ICollection<FormationOccupation>? FormationOccupations { get; set; }

        [Display(Name = "Formaciones")]
        public int FormationOccupationsNumber => FormationOccupations == null ? 0 : FormationOccupations.Count;

    }
}
