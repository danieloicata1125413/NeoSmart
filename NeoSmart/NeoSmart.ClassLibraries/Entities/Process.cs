using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class Process : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Empresa")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Cod { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;
        public ICollection<Occupation>? Occupations { get; set; }

        [Display(Name = "Cargos")]
        public int OccupationsNumber => Occupations == null ? 0 : Occupations.Count;

        public ICollection<Training>? Trainings { get; set; }

        [Display(Name = "Capacitaciones")]
        public int TrainingsNumber => Trainings == null ? 0 : Trainings.Count;
    }
}
