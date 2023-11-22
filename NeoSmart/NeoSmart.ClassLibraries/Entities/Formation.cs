using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class Formation
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

        [Display(Name = "Habilitado")]
        public bool Status { get; set; }

        public ICollection<FormationTopic>? FormationTopics { get; set; }

        [Display(Name = "Temas")]
        public int FormationTopicsNumber => FormationTopics == null ? 0 : FormationTopics.Count;

        public ICollection<FormationOccupation>? FormationOccupations { get; set; }

        [Display(Name = "Cargos")]
        public int FormationOccupationsNumber => FormationOccupations == null ? 0 : FormationOccupations.Count;
    }
}
