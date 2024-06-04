using NeoSmart.ClassLibraries.Enum;
using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class Training : ISoftDetete
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

        public ICollection<TrainingTopic>? TrainingTopics { get; set; }

        [Display(Name = "Temas")]
        public int TrainingTopicsNumber => TrainingTopics == null ? 0 : TrainingTopics.Count;

        public ICollection<Session>? Sessions { get; set; }

        [Display(Name = "Sesiones")]
        public int SessionNumber => Sessions == null ? 0 : Sessions.Count;

        public ICollection<TrainingExam>? TrainingExams { get; set; }

        [Display(Name = "Mediciones")]
        public int TrainingExamsNumber => TrainingExams == null ? 0 : TrainingExams.Count;

        public ICollection<TrainingImage>? TrainingImages { get; set; }

        [Display(Name = "Imágenes")]
        public int TrainingImagesNumber => TrainingImages == null ? 0 : TrainingImages.Count;

        [Display(Name = "Imagén")]
        public string MainImage => TrainingImages == null || TrainingImages.Count == 0 ? "/img/NoImage.png" : TrainingImages.FirstOrDefault()!.Image;

        [Display(Name = "Imagenes")]
        public List<string> MainImages => TrainingImages == null || TrainingImages.Count == 0 ? new List<string>() { "/img/NoImage.png" } : TrainingImages!.Select(x => x.Image).ToList();
    }
}
