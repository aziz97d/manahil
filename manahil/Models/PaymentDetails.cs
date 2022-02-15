using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class PaymentDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentDetailsId { get; set; }

        [ForeignKey("PaymentId")]
        [Required]
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        //[ForeignKey("DonorId")]
        //[Required]
        //public int DonorId { get; set; }
        //public virtual Donor Donor { get; set; }

        [ForeignKey("ProjectId")]
        [Required]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        //[ForeignKey("ProjectCategoryId")]
        //[Required]
        //public int ProjectCategoryId { get; set; }
        //public virtual Category Category { get; set; }

        public float ProjectPrice { get; set; }
    }
}
