using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class Project
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        [Display(Name="Serial")]
        public int ManahilSerial { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("DonorId")]
        public int DonorId { get; set; }
        public virtual Donor Donor { get; set; }

        [MaxLength(200)]
        [Display(Name="Org Serial")]
        public string DonorSerial { get; set; }

        [MaxLength(100)]
        public DateTime GetDate { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("ContractorId")]
        public string ContractorId { get; set; }
        [MaxLength(100)]
        public DateTime DistributionDate { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public bool PaymentStatus { get; set; }


        
        
        
    }
}
