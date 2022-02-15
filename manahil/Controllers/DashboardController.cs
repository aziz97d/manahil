using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using manahil.Models;
using manahil.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public IActionResult Index()
        {
            int todayProjectsDelivered = db.Projects.Count(c => c.CompletedDate == DateTime.Today);
            int todayProjectsDistribute = db.Projects.Count(c => c.DistributionDate == DateTime.Today);
            int onGoing = db.Projects.Count(c => c.EmployeeId == null);
            int total = db.Projects.Count();
            DashboardViewModel dashboardViewModel = new DashboardViewModel
            {
                TodayDeliveredProject = todayProjectsDelivered,
                //TodayGetProject = db.Projects.Count(c=>c.GetDate == DateTime.Today),
                TodayDistributeProject = todayProjectsDistribute,
                OnGoingProject = onGoing,
                TotalProject = total
            };
            List<Donor> donors = db.Donors.ToList();
            List<Contractor> contractors = db.Contractors.ToList();

            DateTime thisYearFirstDate = Convert.ToDateTime("01/01/2022");

            foreach (Donor donor in donors)
            {
                dashboardViewModel.DonorName.Add(donor.Name);
                dashboardViewModel.TotalProjectByDonor.Add(db.Projects.Where(d=> d.GetDate >= thisYearFirstDate).Count(c=>c.DonorId == donor.DonorId));
                dashboardViewModel.TotalOnGoingProjectByDonor.Add(db.Projects.Where(d => d.GetDate >= thisYearFirstDate).Count(c => c.DonorId == donor.DonorId && c.EmployeeId == null));
            }
            foreach (Contractor contractor in contractors)
            {
                dashboardViewModel.ContractorName.Add(contractor.Name);
                dashboardViewModel.TotalProjectByContractor.Add(db.Projects.Where(d => d.GetDate >= thisYearFirstDate).Count(c => c.ContractorId == contractor.ContractorId));
                dashboardViewModel.TotalOnGoingProjectByContractor.Add(db.Projects.Where(d => d.GetDate >= thisYearFirstDate).Count(c => c.ContractorId == contractor.ContractorId && c.EmployeeId == null));
            }

            return View(dashboardViewModel);
        }

        // not complete yet create for year wise data in dashboard
        [HttpPost]
        public IActionResult Index(int showData)
        {
            int todayProjectsDelivered = db.Projects.Count(c => c.CompletedDate == DateTime.Today);
            int todayProjectsDistribute = db.Projects.Count(c => c.DistributionDate == DateTime.Today);
            int onGoing = db.Projects.Count(c => c.EmployeeId == null);
            int total = db.Projects.Count();
            DashboardViewModel dashboardViewModel = new DashboardViewModel
            {
                TodayDeliveredProject = todayProjectsDelivered,
                //TodayGetProject = db.Projects.Count(c=>c.GetDate == DateTime.Today),
                TodayDistributeProject = todayProjectsDistribute,
                OnGoingProject = onGoing,
                TotalProject = total
            };
            List<Donor> donors = db.Donors.ToList();
            List<Contractor> contractors = db.Contractors.ToList();



            foreach (Donor donor in donors)
            {
                dashboardViewModel.DonorName.Add(donor.Name);
                dashboardViewModel.TotalProjectByDonor.Add(db.Projects.Count(c => c.DonorId == donor.DonorId));
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

        //Today Delivered Projects List

        [HttpGet]
        public IActionResult showDashboardProjects(int value= 0)
        {
            //this line for initial list
            var dProjects = db.Projects.Where(c => c.ProjectId == 0).ToList();

            List<DashboardProjectsViewModels> dashboardProjects = new List<DashboardProjectsViewModels>();

            if (value == 1)
            {
                dProjects = db.Projects.Where(c => c.CompletedDate == DateTime.Today).Include(d => d.Donor).Include(c => c.Category).
                Include(c => c.Contractor).Include(e => e.Employee).ToList();

                foreach (var dProject in dProjects)
                {
                    DashboardProjectsViewModels td = new DashboardProjectsViewModels()
                    {
                        ManahilSerial = dProject.ManahilSerial,
                        DonorName = dProject.Donor.Name,
                        DonorSerial = dProject.DonorSerial,
                        Category = dProject.Category.Name,
                        ContractorName = dProject.Contractor.Name,
                        EmployeeName = dProject.Employee.Name,
                        TamidNumber = dProject.TamidNumber
                    };
                    dashboardProjects.Add(td);
                }
            }
            else if (value == 2)
            {
                dProjects = db.Projects.Where(c => c.DistributionDate == DateTime.Today).Include(d => d.Donor).Include(c => c.Category).
                Include(c => c.Contractor).ToList();

                foreach (var dProject in dProjects)
            {
                DashboardProjectsViewModels td = new DashboardProjectsViewModels()
                {
                    ManahilSerial = dProject.ManahilSerial,
                    DonorName = dProject.Donor.Name,
                    DonorSerial = dProject.DonorSerial,
                    Category = dProject.Category.Name,
                    ContractorName = dProject.Contractor.Name,
                    TamidNumber = dProject.TamidNumber
                };
                dashboardProjects.Add(td);
            }
            }

            return View(dashboardProjects);

        }
        //Today Delivered Projects List


        public IActionResult ProjectsChart()
        {
            try
            {
                //var now = DateTimeOffset.Now;
                //var lastSixMonths = Enumerable.Range(0, 6).Select(i => now.AddDays(-i).ToString("MMyyyy"));

                //try another way to get six months data
                var lastSixMonths = Enumerable.Range(0, 6)
                              .Select(i => DateTime.Now.AddMonths(i - 6))
                              .Select(date => date.ToString("MM/yyyy"));

                List<ProjectChart> projectCharts = new List<ProjectChart>();
                //foreach (var item in lastSixMonths)
                //{
                //    ProjectChart projectChart = new ProjectChart
                //    {
                //        Months = item,
                //        TotalGetProjectsByMonth = sixMonthsProjects.Where(e => e.GetDate.Value.Month == month).Count(),
                //        TotalDeliveredProjectsByMonths = sixMonthsProjects.Where(e => e.CompletedDate.Value.Month == month).Count()
                //    };
                //    projectCharts.Add(projectChart);
                //}

                DateTime sixMonthsBefore = DateTime.Now.AddMonths(-6);
                DateTime tommorowDate = DateTime.Now.AddDays(1);
                var sixMonthsProjects = db.Projects.Where(e => e.GetDate >sixMonthsBefore && e.GetDate<tommorowDate);
                List<int> distinctMonths = sixMonthsProjects.OrderByDescending(e => e.GetDate).Select(e => e.GetDate.Value.Month).Distinct().Take(6).ToList();
                //var u = DateTime.Parse("MMyyy");

                //List<ProjectChart> projectCharts = new List<ProjectChart>();
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
