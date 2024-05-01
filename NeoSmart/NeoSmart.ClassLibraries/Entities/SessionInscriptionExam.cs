using NeoSmart.ClassLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.Entities
{
    public class SessionInscriptionExam : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Sesión Inscrita")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int SessionInscriptionId { get; set; }
        public SessionInscription? SessionInscription { get; set; }

        [Display(Name = "Medición de la Sesión")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SessionExamId { get; set; }
        public SessionExam? SessionExam { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        public ICollection<SessionInscriptionExamAnswer>? SessionInscriptionExamAnswers { get; set; }

        [Display(Name = "Preguntas")]
        public int SessionInscriptionExamAnswersNumber => SessionInscriptionExamAnswers == null ? 0 : SessionInscriptionExamAnswers.Count;

        [Display(Name = "Correctas")]
        public int SessionInscriptionExamAnswersCorrectNumber => SessionInscriptionExamAnswers == null ? 0 : SessionInscriptionExamAnswers.Where(x=>x.Correct == true).Count();

        [Display(Name = "Aprobado")]
        public bool Aprobado => SessionInscriptionExamAnswersNumber / 2 <= SessionInscriptionExamAnswersCorrectNumber ? true : false;
    }
}
