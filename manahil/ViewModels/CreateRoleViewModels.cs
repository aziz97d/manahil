using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.ViewModels
{
    public class CreateRoleViewModels
    {
        [Required(ErrorMessage ="Please Provide a Role")]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
