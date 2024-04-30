using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class TrainingSessionInscriptionExamDTO : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Sesión Inscrita")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int TrainingSessionInscriptionId { get; set; }
        public TrainingSessionInscription? TrainingSessionInscription { get; set; }

        [Display(Name = "Medición de la Sesión")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int TrainingSessionExamId { get; set; }
        public SessionExam? TrainingSessionExam { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Documento")]
        public string? Certificate { get; set; } = null!;

        public ICollection<TrainingSessionInscriptionExamAnswer>? UserTrainingSessionExamAnswers { get; set; }

        [Display(Name = "Preguntas")]
        public int UserTrainingSessionExamAnswersNumber => UserTrainingSessionExamAnswers == null ? 0 : UserTrainingSessionExamAnswers.Count;

        [Display(Name = "Correctas")]
        public int UserTrainingSessionExamAnswersCorrectNumber => UserTrainingSessionExamAnswers == null ? 0 : UserTrainingSessionExamAnswers.Where(x => x.Correct == true).Count();

        [Display(Name = "Aprobado")]
        public bool Aprobado => UserTrainingSessionExamAnswersNumber / 2 <= UserTrainingSessionExamAnswersNumber ? true : false;
    }
}
