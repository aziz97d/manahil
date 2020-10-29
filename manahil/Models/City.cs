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
        public int CityId { get; set; }
        [Required(ErrorMessage ="Please Provide a Name")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please Select a Country")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        //public ICollection<Thana> Thanas { get; set; }
        //public ICollection<Contractor> Contractors { get; set; }

    }
}
