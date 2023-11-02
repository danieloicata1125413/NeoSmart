using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class Topic : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;
        public ICollection<TrainingTopic>? TrainingTopics { get; set; }

        [Display(Name = "Capacitaciones")]
        public int TrainingTopicsNumber => TrainingTopics == null ? 0 : TrainingTopics.Count;
    }
}
