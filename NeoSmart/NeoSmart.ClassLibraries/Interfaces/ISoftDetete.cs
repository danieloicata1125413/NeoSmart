using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Interfaces
{
    public class ISoftDetete
    {
        [Display(Name = "Habilitado")]
        public bool Status { get; set; }

        [Display(Name = "Creado")]
        public DateTime Created { get; set; }

        [Display(Name = "Modificado")]
        public DateTime Updated { get; set; }

        [Display(Name = "Eliminado")]
        public DateTime? Deleted { get; set; }
    }
}
