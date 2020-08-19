using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class FieldWorker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(100)]
        public string PrintEmail { get; set; }
        [MaxLength(100)]
        [Phone]
        public string Contact { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [MaxLength(200)]
        public string Image { get; set; }
        public int ThanaId { get; set; }


        public virtual Thana Thana { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
