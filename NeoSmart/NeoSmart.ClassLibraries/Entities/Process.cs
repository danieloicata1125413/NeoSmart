using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class Process : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Cod { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;
        public ICollection<Occupation>? Occupations { get; set; }

        [Display(Name = "Cargos")]
        public int OccupationsNumber => Occupations == null ? 0 : Occupations.Count;
    }
}
