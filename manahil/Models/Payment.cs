﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
        [MaxLength(500)]
        public string Invoice { get; set; }
        
        [Required]
        [MaxLength(100)]
        public DateTime PaymentDate { get; set; }

        [Required]
        [ForeignKey("ContractorId")]
        public int ContractorId { get; set; }
        public virtual Contractor Contractor { get; set; }

        public string PaymentMethod { get; set; }

        public string Reference { get; set; }
        
        [Required]
        public float TotalAmount { get; set; }
        public float Discount { get; set; }




    }
}
