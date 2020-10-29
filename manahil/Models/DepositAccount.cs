using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class DepositAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepositAccountId { get; set; }
        public string Donor { get; set; }
        public string DepositCode { get; set; }

        [DataType(DataType.Date)]
        public DateTime DepositDate { get; set; }

        
        public string DepositType { get; set; }
        public double DepositAmount { get; set; }
        public double? Balance { get; set; }

    }
}
