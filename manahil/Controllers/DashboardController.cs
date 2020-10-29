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
                TodayGetProject = db.Projects.Count(c=>c.GetDate == DateTime.Today),
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
    }
}
