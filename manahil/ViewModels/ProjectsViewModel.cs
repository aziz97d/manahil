using manahil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.ViewModels
{
    public class ProjectsViewModel
    {
        public DateTime? StartDate { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime? EndDate { get; set; }
        public int? ProjectType { get; set; }
        public int? SearchBy { get; set; }
        //public int selectedMonths { get; set; }
        public List<Project> Projects { get; set; }
        public ProjectsViewModel()
        {
            Projects = new List<Project>();
        }
    }
}
