using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class SessionInscriptionExamAnswer : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Medición")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int SessionInscriptionExamId { get; set; }
        public SessionInscriptionExam? SessionInscriptionExam { get; set; }

        [Display(Name = "Pregunta")]
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Question { get; set; } = null!;

        [Display(Name = "Opción")]
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Answer { get; set; } = null!;

        [Display(Name = "Correcta")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Correct { get; set; }
    }
}
