using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Codigo")]
        public int Cod { get; set; }

        [Display(Name = "Departamento")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public int CountryId { get; set; }

        public Country? Country { get; set; }

        public ICollection<City>? Cities { get; set; }

        [Display(Name = "Municipios")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;

        [Display(Name = "Habilitado")]
        public Boolean Status { get; set; }
    }
}
