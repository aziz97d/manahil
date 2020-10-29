using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.ViewModels
{
    public class ContractorViewModels
    {
        public int ContractorId { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string PrintEmail { get; set; }

        public string Contact { get; set; }

        public string Address { get; set; }
        
        public IFormFile Image { get; set; }

        public int? CityId { get; set; }


    }
}
