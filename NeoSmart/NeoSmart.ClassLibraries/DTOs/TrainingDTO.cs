using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class TrainingDTO : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Interna")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Type { get; set; }

        [Display(Name = "Proceso")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int ProcessId { get; set; }
        public Process? Process { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Duración")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Duration { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string? Observation { get; set; } = null!;

        public List<int>? TrainingTopicIds { get; set; }

        public ICollection<TrainingTopicDTO>? TrainingTopics { get; set; }

        public ICollection<SessionDTO>? TrainingSessions { get; set; }

        public ICollection<TrainingExamDTO>? TrainingExams { get; set; }

        public List<string>? NewTrainingImages { get; set; }
        public List<string>? ListTrainingImages { get; set; }
        public ICollection<TrainingImage>? TrainingImages { get; set; }

    }
}
