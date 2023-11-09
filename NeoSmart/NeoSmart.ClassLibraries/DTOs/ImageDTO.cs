using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class ImageDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public List<string> Images { get; set; } = null!;
    }
}
