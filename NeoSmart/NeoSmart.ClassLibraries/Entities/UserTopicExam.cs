using NeoSmart.ClassLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.Entities
{
    public class UserTopicExam : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Medición")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public User? User { get; set; }
        public string? UserId { get; set; }

        [Display(Name = "Medición")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int TopicExamId { get; set; }
        public TopicExam? TopicExam { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        public ICollection<UserTopicExamAnswer>? UserTopicExamAnswers { get; set; }

        [Display(Name = "Preguntas")]
        public int UserTopicExamAnswersNumber => UserTopicExamAnswers == null ? 0 : UserTopicExamAnswers.Count;

        [Display(Name = "Correctas")]
        public int UserTopicExamAnswersCorrectNumber => UserTopicExamAnswers == null ? 0 : UserTopicExamAnswers.Where(x=>x.Correct == true).Count();

        [Display(Name = "Aprobado")]
        public bool Aprobado => UserTopicExamAnswersNumber / 2 <= UserTopicExamAnswersCorrectNumber ? true : false;
    }
}
