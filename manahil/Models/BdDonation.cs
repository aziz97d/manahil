using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class BdDonation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BdDonationId { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string Contact { get; set; }
        public string Address { get; set; }


        [ForeignKey("PaymentTypeId")]
        public int PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType {get; set;}
        public string ReceivedNumber { get; set; }

        public double Amount { get; set; }
        public string TranxCode { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string Remarks { get; set; }
        

    }
}
