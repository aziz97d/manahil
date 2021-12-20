using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.ViewModels
{
    public class ProjectWithName
    {
        public int ManahilSerial { get; set; }
        public string ProjectName { get; set; }
        public string DonorName { get; set; }
        public string DonorSerial { get; set; }
        public DateTime? GetDate { get; set; }
        public string CategoryName { get; set; }
        public string ContractorName { get; set; }
        public DateTime? DistributionDate { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool PaymentStatus { get; set; }
        public string Notes { get; set; }
        public string TamidNumber { get; set; }
    }
}
