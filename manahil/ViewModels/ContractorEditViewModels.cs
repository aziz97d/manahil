using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.ViewModels
{
    public class ContractorEditViewModels:ContractorViewModels
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }


        // These property use for only view contractor use in index page
        //public string CityName { get; set; }
        //public int TotalProject { get; set; }
        //public int CompletedProject { get; set; }
    }
}
