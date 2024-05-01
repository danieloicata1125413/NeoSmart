using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class Request : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Líder")]
        public string? UserLeaderId { get; set; }
        public User? UserLeader { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Justifiación")]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string? Requirement { get; set; } = null!;

        [Display(Name = "Fecha tentativa")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Duración")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Duration { get; set; }

        [Display(Name = "Interna")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Type { get; set; }

        [Display(Name = "Entidad")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string? Entity { get; set; } = null!;

        [Display(Name = "Precio")]
        public int? Price { get; set; } = null!;

        [Display(Name = "Gerente")]
        public string? UserManagerId { get; set; }
        public User? UserManager { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string? Observation { get; set; } = null!;

        [Display(Name = "Estado")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int RequestStatusId { get; set; }
        public RequestStatus? RequestStatus { get; set; }

        public ICollection<RequestUser>? RequestUsers { get; set; }

    }
}
