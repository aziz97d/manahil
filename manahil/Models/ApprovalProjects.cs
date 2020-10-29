using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class ApprovalProjects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApprovalProjectId { get; set; }

        [Display(Name ="ক্র.নং")]
        public int SerialNo { get; set; }
        [Display(Name = "প্রকল্পের নাম")]
        public string ProjectName { get; set; }

        [Display(Name = "স্মারক নং")]
        public string MonumentalNo { get; set; }

        [Display(Name = "অনুমোদনের তারিখ")]
        [DataType(DataType.Date)]
        public DateTime? ApprovalDate { get; set; }
        [DataType(DataType.Date)]

        [Display(Name = "মেয়াদ শুরু")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "মেয়াদ শেষ")]
        [DataType(DataType.Date)]
        public DateTime? EndtDate { get; set; }

        [Display(Name = "দাতার নাম")]
        public string Donor { get; set; }

        [Display(Name = "অডিট রিপোর্ট")]
        public string AuditStatus { get; set; }

        [Display(Name = "প্রত্যয়ন পত্র")]
        public string CertificateStatus { get; set; }

        [Display(Name = "অনুমোদিত অর্থ")]
        public double ApprovedMoney { get; set; }

        [Display(Name = "বিনিময় দর")]
        public double CurrencyRate { get; set; }

        [Display(Name = "গৃহীত টাকা ও তারিখ")]
        public double TotalAcceptedMoney { get; set; }

        [Display(Name = "ব্যয়িত টাকা")]
        public double ExpenseAmount { get; set; }

        [Display(Name = "প্রকল্পের ঠিকানা")]
        public string ProjectImplementAddress { get; set; }
    }
}
