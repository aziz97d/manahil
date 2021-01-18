using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using manahil.Models;
using manahil.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manahil.Controllers
{
    [Authorize(Roles = "Admin, Management, Computer Operator, Accounting Manager")]
    public class DashboardController : Controller
    {
        private readonly DatabaseContext db;

        public DashboardController(DatabaseContext context)
        {
            this.db = context;
        }
        public IActionResult Index()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel
            {
                TodayDeliveredProject = db.Projects.Count(c=>c.CompletedDate == DateTime.Today),
                //TodayGetProject = db.Projects.Count(c=>c.GetDate == DateTime.Today),
                TodayDistributeProject = db.Projects.Count(c=>c.DistributionDate == DateTime.Today),
                OnGoingProject = db.Projects.Count(c=>c.EmployeeId == null),
                TotalProject = db.Projects.Count()
            };
            List<Donor> donors = db.Donors.ToList();
            List<Contractor> contractors = db.Contractors.ToList();



            foreach (Donor donor in donors)
            {
                dashboardViewModel.DonorName.Add(donor.Name);
                dashboardViewModel.TotalProjectByDonor.Add(db.Projects.Count(c=>c.DonorId == donor.DonorId));
                dashboardViewModel.TotalOnGoingProjectByDonor.Add(db.Projects.Count(c => c.DonorId == donor.DonorId && c.EmployeeId == null));
            }
            foreach (Contractor contractor in contractors)
            {
                dashboardViewModel.ContractorName.Add(contractor.Name);
                dashboardViewModel.TotalProjectByContractor.Add(db.Projects.Count(c => c.ContractorId == contractor.ContractorId));
                dashboardViewModel.TotalOnGoingProjectByContractor.Add(db.Projects.Count(c => c.ContractorId == contractor.ContractorId && c.EmployeeId == null));
            }

            return View(dashboardViewModel);
        }


        public IActionResult ProjectsChart()
        {
            try
            {
                //var now = DateTimeOffset.Now;
                //var lastSixMonths = Enumerable.Range(0, 6).Select(i => now.AddDays(-i).ToString("MMyyyy"));
                
                DateTime sixMonthsBefore = DateTime.Now.AddMonths(-6);
                DateTime tommorowDate = DateTime.Now.AddDays(1);
                var sixMonthsProjects = db.Projects.Where(e => e.GetDate >sixMonthsBefore && e.GetDate<tommorowDate);
                List<int> distinctMonths = sixMonthsProjects.OrderByDescending(e => e.GetDate).Select(e => e.GetDate.Value.Month).Distinct().Take(6).ToList();
                //var u = DateTime.Parse("MMyyy");

                List<ProjectChart> projectCharts = new List<ProjectChart>();
                foreach (var month in distinctMonths)
                {
                    ProjectChart projectChart = new ProjectChart
                    {
                        Months = month,
                        TotalGetProjectsByMonth = sixMonthsProjects.Where(e => e.GetDate.Value.Month == month).Count(),
                        TotalDeliveredProjectsByMonths = sixMonthsProjects.Where(e => e.CompletedDate.Value.Month == month).Count()
                    };
                    projectCharts.Add(projectChart);
                }

                return Ok(projectCharts);
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
