using NeoSmart.ClassLibraries.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.ClassLibraries.DTOs
{
    public class UserRolesDTO
    {
        public string UserName { get; set; }

        [Display(Name = "Tipos de usuario")]
        public List<string>? UserTypes { get; set; }
    }
}
