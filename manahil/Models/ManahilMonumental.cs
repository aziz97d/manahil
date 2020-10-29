using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class ManahilMonumental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManahilMonumentalId { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="তারিখ")]
        public DateTime Date { get; set; }
        [Display(Name = "স্মারক নং")]
        public string MonumentalNo { get; set; }
        [Display(Name = "বিষয়")]
        public string Subject { get; set; }
        [Display(Name = "প্রতি")]
        public string Destination { get; set; }
        [Display(Name = "মন্তব্য")]
        public string Comments { get; set; }
    }
}
