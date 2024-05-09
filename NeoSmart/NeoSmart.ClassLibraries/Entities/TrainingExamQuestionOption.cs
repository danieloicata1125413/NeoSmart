using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingExamQuestionOption : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Opción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int TrainingExamQuestionId { get; set; }
        public TrainingExamQuestion? TrainingExamQuestion { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Correcta")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Correct { get; set; }
    }
}
