using NeoSmart.ClassLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingCalendar : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Fecha de Finalización")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateEnd { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;
    }
}
