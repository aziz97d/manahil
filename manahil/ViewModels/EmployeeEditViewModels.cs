using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.ViewModels
{
    public class EmployeeEditViewModels:EmployeeCreateViewModels
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
