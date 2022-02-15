using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class ProjectPricing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProectPricingId { get; set; }
        [ForeignKey("DonorId")]
        [Required]
        public int DonorId { get; set; }
        public virtual Donor Donor { get; set; }

        [ForeignKey("CategoryId")]
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("ContractorId")]
        public int? ContractorId { get; set; }
        public virtual Contractor Contractor { get; set; }
        public float Price { get; set; }

    }
}
