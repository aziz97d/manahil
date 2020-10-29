using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.ViewModels
{
    public class DonorViewModel
    {
        public int DonorId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Image { get; set; }
        public int TotalProject { get; set; }
        public int DeliveredProject { get; set; }
    }
}
