using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.ViewModels
{
    public class EmployeeViewModel:Models.Employee
    {
        public int TotalCompleteProjects { get; set; }
        public int TodayCompleteProjects { get; set; }
    }
}
