using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class Donor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonorId { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string OwnerName { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Contact { get; set; }
        [MaxLength(200)]
        public string Image { get; set; }
        [ForeignKey("CountryId")]
        public int? CountryId { get; set; }


        public virtual Country Country { get; set; }

        //public ICollection<ManageDrive> ManageDrives { get; set; }
        //public ICollection<Project> Projects { get; set; }
        //public ICollection<Payment> Payments { get; set; }
    }
}
