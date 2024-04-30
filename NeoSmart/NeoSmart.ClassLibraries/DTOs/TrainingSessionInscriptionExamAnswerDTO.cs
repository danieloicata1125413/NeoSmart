﻿using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class TrainingSessionInscriptionExamAnswerDTO : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Medición")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int TrainingSessionInscriptionExamId { get; set; }
        public TrainingSessionInscriptionExamDTO? TrainingSessionInscriptionExam { get; set; }

        [Display(Name = "Pregunta")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Question { get; set; } = null!;

        [Display(Name = "Respuesta")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Answer { get; set; } = null!;

        [Display(Name = "Correcta")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Correct { get; set; }
    }
}
