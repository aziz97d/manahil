using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace manahil.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Display(Name ="New Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Required]
        [Display(Name ="Confim Password")]
        [Compare("Password",ErrorMessage ="Password Don't Match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
