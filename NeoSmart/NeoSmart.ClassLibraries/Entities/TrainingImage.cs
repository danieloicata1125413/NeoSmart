using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class TrainingImage
    {
        public int Id { get; set; }

        public int TrainingId { get; set; }

        public Training Training { get; set; } = null!;

        [Display(Name = "Imagen")]
        public string Image { get; set; } = null!;
    }
}
