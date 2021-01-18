using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.ViewModels
{
    public class ContractorProjectViewModel
    {
        public int ProjectId { get; set; }
        public int ManahilSerial { get; set; }
        public string Name { get; set; }
        public string Donor { get; set; }
        public string DonorSerial { get; set; }
        public string DistributionDate { get; set; }
        public string Category { get; set; }
        public int PassDays { get; set; }
        public string Notes { get; set; }
        public bool IsComplete { get; set; }
    }
}
