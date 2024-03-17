using NeoSmart.ClassLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TopicExamQuestion : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Medición")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int TopicExamId { get; set; }
        public TopicExam? TopicExam { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Correcta")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Correct { get; set; }
    }
}
