using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class Training
    {
        public int Id { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Cod { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Duración")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Duration { get; set; }

        [Display(Name = "Interna")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Type { get; set; }

        [Display(Name = "Proceso")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ProcessId { get; set; }
        public Process? Process { get; set; }

        [Display(Name = "Habilitado")]
        public bool Status { get; set; }
        public ICollection<TrainingTopic>? TrainingTopics { get; set; }

        [Display(Name = "Temas")]
        public int TrainingTopicsNumber => TrainingTopics == null ? 0 : TrainingTopics.Count;

        public ICollection<TrainingImage>? TrainingImages { get; set; }

        [Display(Name = "Imágenes")]
        public int TrainingImagesNumber => TrainingImages == null ? 0 : TrainingImages.Count;

        [Display(Name = "Imagén")]
        public string MainImage => TrainingImages == null || TrainingImages.Count == 0 ? "/img/NoImage.png" : TrainingImages.FirstOrDefault()!.Image;

        [Display(Name = "Imagenes")]
        public List<string> MainImages => TrainingImages == null || TrainingImages.Count == 0 ? new List<string>() { "/img/NoImage.png" } : TrainingImages!.Select(x => x.Image).ToList();
    
        public ICollection<TrainingSession>? TrainingSessions { get; set; }

        [Display(Name = "Sesiones")]
        public int TrainingSessionNumber => TrainingSessions == null ? 0 : TrainingSessions.Count;

        public ICollection<TrainingTopicExam>? TrainingTopicExams { get; set; }

        [Display(Name = "Mediciones")]
        public int TrainingTopicExamsNumber => TrainingTopicExams == null ? 0 : TrainingTopicExams.Count;


    }
}
