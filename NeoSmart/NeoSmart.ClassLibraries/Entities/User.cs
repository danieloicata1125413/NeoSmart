using NeoSmart.ClassLibraries.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using NeoSmart.ClassLibraries.DTOs;

namespace NeoSmart.ClassLibraries.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Empresa")]
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }

        [Display(Name = "Cargo")]
        public int? OccupationId { get; set; }
        public Occupation? Occupation { get; set; }

        [Display(Name = "Identificación")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }

        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Tipo de Identificación")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int DocumentTypeId { get; set; }
        public DocumentType? DocumentType { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Address { get; set; }

        [Display(Name = "Foto")]
        public string? Photo { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Habilitado")]
        public bool Status { get; set; }

        [Display(Name = "Ciudad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CityId { get; set; }
        public City? City { get; set; }

        public static implicit operator User(UserDTO v)
        {
            throw new NotImplementedException();
        }
    }
}
