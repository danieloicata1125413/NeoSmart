using NeoSmart.ClassLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingExamQuestion : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Medición")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int TrainingExamId { get; set; }
        public TrainingExam? TrainingExam { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        public ICollection<TrainingExamQuestionOption>? TrainingExamQuestionOptions { get; set; }

        [Display(Name = "Opciones")]
        public int TrainingExamQuestionOptionsNumber => TrainingExamQuestionOptions == null ? 0 : TrainingExamQuestionOptions.Count;

    }
}
