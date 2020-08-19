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
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey("DonorId")]
        public int DonorId { get; set; }
        [MaxLength(200)]
        public string DonorSerial { get; set; }
        [MaxLength(100)]
        public DateTime GetDate { get; set; }
        [ForeignKey("FieldWorkerId")]
        public string FieldWorkerId { get; set; }
        [MaxLength(100)]
        public DateTime DistributionDate { get; set; }
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public bool PaymentStatus { get; set; }


        public virtual Donor Donor { get; set; }
        public virtual FieldWorker FieldWorker { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
