using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [ForeignKey("CountryId")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public ICollection<Thana> Thanas { get; set; }
        public virtual Country Country { get; set; }
    }
}
