using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingTopicExam
    {
        public int Id { get; set; }

        [Display(Name = "Capacitación")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int TrainingId { get; set; }
        public Training Training { get; set; } = null!;

        [Display(Name = "Medición")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int TopicExamId { get; set; }
        public TopicExam TopicExam { get; set; } = null!;

        [Display(Name = "Fecha de Inicio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Fecha de Finalización")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateEnd { get; set; }

        [Display(Name = "Hora de Inicio")]
        public TimeSpan? TimeStart { get; set; }

        [Display(Name = "Hora de Finalización")]
        public TimeSpan? TimeEnd { get; set; }

        [Display(Name = "Intentos")]
        public int Attempt { get; set; }
    }
}
