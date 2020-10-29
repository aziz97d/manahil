using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using manahil.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using manahil.ViewModels;

namespace manahil.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DatabaseContext db;
        

        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            db = context;
            
        }

        public IActionResult Index()
        {
            //ViewData["Categories"] = db.Categories.ToList();
            var Categories = db.Categories.ToList();
            HttpContext.Session.SetObject("categories", Categories);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Contractor")]
        public IActionResult GetProjectsByContractor()
        {
            int contractorId = Convert.ToInt32(Request.Cookies["UserId"]);
            var projects = db.Projects.Where(c=>c.ContractorId == contractorId && c.EmployeeId == null);
            List<ContractorProjectViewModel> contractorProjects = new List<ContractorProjectViewModel>();

            foreach (Project projcet in projects)
            {
                ContractorProjectViewModel contractorProject = new ContractorProjectViewModel();
                contractorProject.ProjectId = projcet.ProjectId;
                contractorProject.ManahilSerial = projcet.ManahilSerial;
                contractorProject.Name = projcet.Name;
                contractorProject.Donor = db.Donors.Find(projcet.DonorId).Name;
                contractorProject.DonorSerial = projcet.DonorSerial;
                contractorProject.Category = db.Categories.Find(projcet.CategoryId).Name;
                contractorProject.DistributionDate = Convert.ToDateTime(projcet.DistributionDate).ToShortDateString();
                TimeSpan diff = DateTime.Today - Convert.ToDateTime(projcet.DistributionDate);
                contractorProject.PassDays = diff.Days;
                contractorProject.Notes = projcet.Notes;

                contractorProjects.Add(contractorProject);
            }
            return View(contractorProjects);
        }
        [HttpGet]
        [Authorize(Roles = "Contractor")]
        public IActionResult GetAllProjectsByContractor()
        {
            int contractorId = Convert.ToInt32(Request.Cookies["UserId"]);
            var projects = db.Projects.Where(c => c.ContractorId == contractorId);
            List<ContractorProjectViewModel> contractorProjects = new List<ContractorProjectViewModel>();

            foreach (Project projcet in projects)
            {
                ContractorProjectViewModel contractorProject = new ContractorProjectViewModel();
                contractorProject.ProjectId = projcet.ProjectId;
                contractorProject.ManahilSerial = projcet.ManahilSerial;
                contractorProject.Name = projcet.Name;
                contractorProject.Donor = db.Donors.Find(projcet.DonorId).Name;
                contractorProject.DonorSerial = projcet.DonorSerial;
                contractorProject.Category = db.Categories.Find(projcet.CategoryId).Name;
                contractorProject.DistributionDate = Convert.ToDateTime(projcet.DistributionDate).ToShortDateString();
                TimeSpan diff = DateTime.Today - Convert.ToDateTime(projcet.DistributionDate);
                contractorProject.PassDays = diff.Days;
                contractorProject.Notes = projcet.Notes;

                contractorProjects.Add(contractorProject);
            }
            return View(contractorProjects);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult OurProjects()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
