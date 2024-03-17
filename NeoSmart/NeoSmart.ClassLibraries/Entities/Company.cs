using NeoSmart.ClassLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.Entities
{
    public class Company : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Identificación")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nit { get; set; }

        [Display(Name = "Nombre")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Ciudad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CityId { get; set; }
        public City? City { get; set; }

        public ICollection<Process>? Process { get; set; }

        [Display(Name = "Procesos")]
        public int ProcessNumber => Process == null ? 0 : Process.Count;
    }
}
