using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.ViewModels
{
    public class DashboardViewModel
    {
        public int TodayDeliveredProject { get; set; }
        public int TodayGetProject { get; set; }
        public int OnGoingProject { get; set; }
        public int TotalProject { get; set; }

        public List<string> DonorName { get; set; }
        public List<int> TotalProjectByDonor { get; set; }
        public List<int> TotalOnGoingProjectByDonor { get; set; }

        public List<string> ContractorName { get; set; }
        public List<int> TotalProjectByContractor { get; set; }
        public List<int> TotalOnGoingProjectByContractor { get; set; }


        public List<ProjectChart> ProjectCharts { get; set; }


        public DashboardViewModel()
        {
            DonorName = new List<string>();
            TotalProjectByDonor = new List<int>();
            TotalOnGoingProjectByDonor = new List<int>();

            ContractorName = new List<string>();
            TotalProjectByContractor = new List<int>();
            TotalOnGoingProjectByContractor = new List<int>();

            ProjectCharts = new List<ProjectChart>();

        }


    }

    public class ProjectChart
    {
        public int Months { get; set; }
        public int TotalGetProjectsByMonth { get; set; }
        public int TotalDeliveredProjectsByMonths { get; set; }
    }

}
