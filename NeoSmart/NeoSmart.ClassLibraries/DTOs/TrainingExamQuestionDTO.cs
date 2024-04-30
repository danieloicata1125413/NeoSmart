using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class TrainingExamQuestionDTO : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Medición")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int TrainingExamId { get; set; }
        public TrainingExamDTO? TrainingExam { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Correcta")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Correct { get; set; }
    }
}
