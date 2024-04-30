using NeoSmart.ClassLibraries.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeoSmart.ClassLibraries.Entities
{
    public class RequestUser : ISoftDetete
    {
        public int Id { get; set; }

        [Display(Name = "Solicitud")]
        public int RequestId { get; set; }
        public Request? Request { get; set; }

        [Display(Name = "Trabajandor")]
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
