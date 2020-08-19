﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class Thana
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThanaId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [ForeignKey("CountryId")]
        [Required]
        public int CityId { get; set; }
        public virtual City City { get; set; }


        public ICollection<Contractor> FieldWorkers { get; set; }
    }
}