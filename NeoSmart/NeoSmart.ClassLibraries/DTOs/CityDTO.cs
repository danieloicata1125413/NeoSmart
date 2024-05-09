using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class CityDTO
    {
        public int Id { get; set; }

        [Display(Name = "Ciudad")]
        public string Name { get; set; } = null!;
    }
}
