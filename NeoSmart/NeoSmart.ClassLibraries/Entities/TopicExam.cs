using NeoSmart.ClassLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TopicExam : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Tema")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int TopicId { get; set; }
        public Topic? Topic { get; set; } = null!;

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        public ICollection<TopicExamQuestion>? TopicExamQuestions { get; set; }

        [Display(Name = "Preguntas")]
        public int TopicExamQuestionsNumber => TopicExamQuestions == null ? 0 : TopicExamQuestions.Count;
    }
}
