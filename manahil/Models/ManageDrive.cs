using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class ManageDrive
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManageDriveId { get; set; }

        [Required(ErrorMessage = "Please Select a Organizarion")]
        [Display(Name = "Organization")]
        [ForeignKey("DonorId")]
        public int DonorId { get; set; }
        public virtual Donor Donor { get; set; }

        [Required(ErrorMessage ="Please Provide an Email")]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Drive Account Password")]
        [MaxLength(200)]
        public string Password { get; set; }

        [Url]
        public string DriveLink { get; set; } = "http://";

    }
}
