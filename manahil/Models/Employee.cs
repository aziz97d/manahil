using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Contact { get; set; }
        [MaxLength(1000)]
        public string Address { get; set; }
        
        public string Image { get; set; }
        [ForeignKey("DesignationId")]
        public int? DesignationId { get; set; }
        [ForeignKey("CityId")]
        public int? CityId { get; set; }
        public bool Status { get; set; }


        public virtual Designation Designation { get; set; }
        public virtual City City { get; set; }

       // public ICollection<Project> Projects { get; set; }
    }
}
