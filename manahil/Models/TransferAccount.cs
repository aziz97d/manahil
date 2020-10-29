using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class TransferAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransferAccountId { get; set; }

        [ForeignKey("DepositAccountId")]
        [Required]
        public int DepositAccountId { get; set; }
        public virtual DepositAccount DepositAccount { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime TransferDate { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid amount")]
        public double TransferAmount { get; set; }
        [Required]
        public string ContractorName { get; set; }
        [Required]
        public string AgainstProject { get; set; }
        public string FC1orFD7 { get; set; }
        public string ApprovalStatus { get; set; }
    }
}
