using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class Topic
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Detalle")]
        [MaxLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string? Details { get; set; } = null!;

        [Display(Name = "Habilitado")]
        public bool Status { get; set; }

        public ICollection<TrainingTopic>? TrainingTopics { get; set; }

        [Display(Name = "Capacitaciones")]
        public int TrainingTopicsNumber => TrainingTopics == null ? 0 : TrainingTopics.Count;

        public ICollection<FormationTopic>? FormationTopics { get; set; }

        [Display(Name = "Formaciones")]
        public int FormationTopicsNumber => FormationTopics == null ? 0 : FormationTopics.Count;
    }
}
