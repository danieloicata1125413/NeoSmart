using NeoSmart.ClassLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingExam : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Capacitación")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int TrainingId { get; set; }
        public Training? Training { get; set; } = null!;

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        public ICollection<TrainingExamQuestion>? TrainingExamQuestions { get; set; }

        [Display(Name = "Preguntas")]
        public int TrainingExamQuestionsNumber => TrainingExamQuestions == null ? 0 : TrainingExamQuestions.Count;
    }
}
