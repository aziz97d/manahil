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
        [Display(Name = "Serial")]
        public int ManahilSerial { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("DonorId")]
        [Display(Name = "Donor")]
        public int DonorId { get; set; }
        public virtual Donor Donor { get; set; }

        [MaxLength(200)]
        [Display(Name = "Org Sl")]
        public string DonorSerial { get; set; }

        [Display(Name = "Get Date")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? GetDate { get; set; } = DateTime.Today;

        [ForeignKey("CategoryId")]
        [Required]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("ContractorId")]
        [Display(Name = "Contractor")]
        public int? ContractorId { get; set; }
        public virtual Contractor Contractor { get; set; }

        [Display(Name="Distribute Date")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DistributionDate { get; set; }

        [ForeignKey("EmployeeId")]
        [Display(Name="Employee")]
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [Display(Name = "Completion Date")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CompletedDate { get; set; }

        [Display(Name ="Payment Status")]
        public bool PaymentStatus { get; set; }
        public string Notes { get; set; }
        [Display(Name="Tamid No")]
        public string TamidNumber { get; set; }





    }
}
