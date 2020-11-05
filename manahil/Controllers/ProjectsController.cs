using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using manahil.Models;
using System.Security.Cryptography.X509Certificates;
using manahil.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace manahil.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly DatabaseContext db;

        public ProjectsController(DatabaseContext context)
        {
            db = context;
        }

        // GET: Projects
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            
            return View(await db.Projects.Include(d => d.Donor).Include(c => c.Contractor).
                Include(c => c.Category).Include(e => e.Employee).ToListAsync());
        }

        //GET: Projects/Details/5


        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");
            ViewData["ContractorId"] = new SelectList(db.Contractors, "ContractorId", "Name");
            ViewData["EmployeeId"] = new SelectList(db.Employees, "EmployeeId", "Name");
            ViewBag.Title = "Create";
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {

            if (ModelState.IsValid)
            {
                if (project.ProjectId > 0)
                {
                    db.Update(project);
                    await db.SaveChangesAsync();
                    TempData["Message"] = "Data Update Successfully";
                    TempData["Status"] = "2";
                }
                else
                {
                    try
                    {
                        for (int i = 0; i < 1000; i++)
                        {
                            project.ProjectId = 0;
                            db.Add(project);
                            await db.SaveChangesAsync();
                        }



                        TempData["Message"] = "Data Added Successfully";
                        TempData["Status"] = "1";
                        //ViewBag.Message = "Data Added Successfully";
                        //ViewBag.Status = "1";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProjectExists(project.ProjectId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");
            ViewData["ContractorId"] = new SelectList(db.Contractors, "ContractorId", "Name");
            ViewData["EmployeeId"] = new SelectList(db.Employees, "EmployeeId", "Name");
            return View(project);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult CreateMultiple()
        {
            ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");
            ViewData["ContractorId"] = new SelectList(db.Contractors, "ContractorId", "Name");
            ViewData["EmployeeId"] = new SelectList(db.Employees, "EmployeeId", "Name");
            ViewBag.Title = "Create";
            return View(new Project());
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult CreateMultiple(List<Project> projects)
        {
            try
            {
                foreach (Project project in projects)
                {
                     db.Projects.Add(project);
                }
                db.SaveChanges();
                TempData["Message"] = projects.Count+" projects Add Successfully";
                TempData["Status"] = "1";
                //    return View(new Project());
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //        throw;
                //}
                return Json(new { Success = 1 });
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Success = 0, ex = ex.InnerException.InnerException.Message.ToString() });
            }

            //ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
            //ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");
            //ViewData["ContractorId"] = new SelectList(db.Contractors, "ContractorId", "Name");
            //ViewData["EmployeeId"] = new SelectList(db.Employees, "EmployeeId", "Name");
            //return View("CreateMultiple",projects);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult GetManahilSerialByCategoryId(int categoryId)
        {
            try
            {
                var manahilSerial =  db.Projects.AsEnumerable().Where(c => c.CategoryId == categoryId)
                    .Select(p=>p.ManahilSerial
                    ).LastOrDefault();

                return Json(new {Success = manahilSerial });
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Success = 0, ex = ex.InnerException.InnerException.Message.ToString() });
            }
            
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            var project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");
            ViewData["ContractorId"] = new SelectList(db.Contractors, "ContractorId", "Name");
            ViewData["EmployeeId"] = new SelectList(db.Employees, "EmployeeId", "Name");
            return View("Create", project);
        }



        // GET: Cities/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var project = await db.Projects.Include(d => d.Donor).Include(c => c.Contractor).
                Include(c => c.Category).Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");
            ViewData["ContractorId"] = new SelectList(db.Contractors, "ContractorId", "Name");
            ViewData["EmployeeId"] = new SelectList(db.Employees, "EmployeeId", "Name");
            return View("Create", project);
        }

        // POST: Cities/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var project = await db.Projects.FindAsync(id);
                db.Projects.Remove(project);
                db.SaveChanges();

                TempData["Message"] = "Data Delete Successfully";
                TempData["Status"] = "3";


                //TempData["Message"] = "Data Delete Successfully";
                return Json(new { Success = 1 });
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Success = 0, ex = ex.InnerException.InnerException.Message.ToString() });
            }
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Any(e => e.ProjectId == id);
        }


        //ProjectView
        [Authorize(Roles = "Admin,Management,Accounting Manager,Computer Operator")]
        public async Task<IActionResult> ProjectView()
        {
            ProjectsViewModel projects = new ProjectsViewModel();
            projects.Projects = await db.Projects.Include(d => d.Donor).
                Include(c => c.Category).Include(c=>c.Contractor).Include(c=>c.Employee).ToListAsync();
            return View(projects);
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Management,Accounting Manager,Computer Operator")]
        public async Task<IActionResult> ProjectView(/*DateTime? startDate, DateTime? endDate, int? projectType*/ ProjectsViewModel projectsViewModel)
        {
            DateTime? startDate = projectsViewModel.StartDate;
            DateTime? endDate = projectsViewModel.EndDate;
            int? projectType = projectsViewModel.ProjectType;
            DateTime endDateAdd = Convert.ToDateTime(endDate).AddDays(1);

            var query =  db.Projects.Include(d => d.Donor).
                Include(c => c.Category).Include(c => c.Contractor).Include(e=>e.Employee);

            List<Project> projects = new List<Project>();
            if (startDate!=null && endDate==null && projectType == null)
            {
                 projects = await query.Where(d=>d.GetDate>startDate).ToListAsync();
            }
            else if(startDate == null && endDate != null && projectType == null)
            {
                 projects = await query.Where(d => d.GetDate < endDateAdd).ToListAsync();

            }else if (startDate != null && endDate != null && projectType == null)
            {
                 projects = await query.Where(d =>d.GetDate>startDate && d.GetDate < endDateAdd).ToListAsync();
            }else if (startDate == null && endDate == null && projectType == 1)
            {
                 projects = await query.Where(d=>d.EmployeeId==null).ToListAsync();
            }else if (startDate == null && endDate == null && projectType == 2)
            {
                 projects = await query.Where(d =>d.EmployeeId != null).ToListAsync();
            }
            else if (startDate != null && endDate != null && projectType == 1)
            {
                projects = await query.Where(d => d.GetDate > startDate && d.GetDate < endDateAdd && d.EmployeeId == null).ToListAsync();
            }
            else if (startDate != null && endDate != null && projectType == 2)
            {
                projects = await query.Where(d => d.GetDate > startDate && d.GetDate < endDateAdd && d.EmployeeId != null).ToListAsync();
            }
            else
            {
                projects = await query.ToListAsync();
            }
            projectsViewModel.Projects = projects;
            return View(projectsViewModel);
        }

        // GET: Projects/Details/5


        // GET: Projects/Create
        //public IActionResult OnlyCreateProject()
        //{
        //    ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
        //    ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");
        //    ViewBag.Title = "Create";
        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> OnlyCreateProject(Project project)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        if (project.ProjectId > 0)
        //        {
        //            db.Update(project);
        //            await db.SaveChangesAsync();
        //            TempData["Message"] = "Data Update Successfully";
        //            TempData["Status"] = "2";
        //        }
        //        else
        //        {
        //            try
        //            {
        //                db.Add(project);
        //                await db.SaveChangesAsync();

        //                TempData["Message"] = "Data Added Successfully";
        //                TempData["Status"] = "1";
        //                //ViewBag.Message = "Data Added Successfully";
        //                //ViewBag.Status = "1";
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!ProjectExists(project.ProjectId))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }

        //        }

        //        return RedirectToAction(nameof(OnlyProjectView));
        //    }
        //    ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
        //    ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");

        //    return View(project);
        //}

        //[HttpGet]
        //public async Task<IActionResult> OnlyEditProject(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.Title = "Edit";
        //    var project = await db.Projects.FindAsync(id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
        //    ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");

        //    return View("OnlyCreateProject", project);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> OnlyEditProject(Project project)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        if (project.ProjectId > 0)
        //        {
        //            db.Update(project);
        //            await db.SaveChangesAsync();
        //            TempData["Message"] = "Data Update Successfully";
        //            TempData["Status"] = "2";
        //        }
        //        else
        //        {
        //            try
        //            {
        //                db.Add(project);
        //                await db.SaveChangesAsync();

        //                TempData["Message"] = "Data Added Successfully";
        //                TempData["Status"] = "1";
        //                //ViewBag.Message = "Data Added Successfully";
        //                //ViewBag.Status = "1";
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!ProjectExists(project.ProjectId))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }

        //        }

        //        return RedirectToAction(nameof(OnlyProjectView));
        //    }
        //    ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
        //    ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");

        //    return View(project);
        //}

        //public async Task<IActionResult> DeleteOnlyProject(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var project = await db.Projects.Include(d => d.Donor).
        //        Include(c => c.Category).FirstOrDefaultAsync(m => m.ProjectId == id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.Title = "Delete";
        //    ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
        //    ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");

        //    return View("OnlyCreateProject", project);
        //}

        //// POST: Cities/Delete/5
        //[HttpPost, ActionName("DeleteOnlyProject")]
        ////  [ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmedOnlyProject(int id)
        //{
        //    try
        //    {
        //        var project = await db.Projects.FindAsync(id);
        //        db.Projects.Remove(project);
        //        db.SaveChanges();

        //        TempData["Message"] = "Data Delete Successfully";
        //        TempData["Status"] = "3";


        //        //TempData["Message"] = "Data Delete Successfully";
        //        return Json(new { Success = 1 });
        //    }
        //    catch (Exception ex)
        //    {
        //        // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
        //        return Json(new { Success = 0, ex = ex.InnerException.InnerException.Message.ToString() });
        //    }
        //}

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DistributionProject()
        {
            ViewData["ContractorId"] = new SelectList(db.Contractors, "ContractorId", "Name");
            return View(await db.Projects.Where(d =>d.ContractorId == null).Include(d => d.Donor).
                Include(c => c.Category).ToListAsync());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DistributionProject(int[] projectId, string contractorId, DateTime getDate)
        {
            try
            {
                foreach (int id in projectId)
                {
                    var project = await db.Projects.FindAsync(id);
                    project.ContractorId = Convert.ToInt32(contractorId);
                    project.DistributionDate = getDate;
                    db.Projects.Update(project);
                }
                db.SaveChanges();
                TempData["Message"] = "Project Distribute Successfully";
                TempData["Status"] = "1";


                
                return Json(new { Success = 1 });
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Success = 0, ex = ex.InnerException.InnerException.Message.ToString() });
            }
            
        }

        //completing Project by employee
        [Authorize(Roles = "Admin,Computer Operator")]
        public async Task<IActionResult> CompletedProject(int? id)
        {
            ViewData["Categories"] = db.Categories.ToList();

            if (id>0)
            {
                return View(await db.Projects.Where(e=>e.ContractorId !=null && e.EmployeeId == null && e.CategoryId==id).Include(d => d.Donor).
                Include(c => c.Contractor).Include(c => c.Category).ToListAsync());
            }
            return View(await db.Projects.Where(e => e.EmployeeId == null && e.ContractorId !=null).Include(d => d.Donor).
                Include(c => c.Contractor).Include(c=>c.Category).ToListAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Computer Operator")]
        public async Task<IActionResult> CompletedProject(int[] projectId)
        {
            try
            {
                string employeeId = Request.Cookies["UserId"];
                foreach (int id in projectId)
                {
                    var project = await db.Projects.FindAsync(id);
                    project.EmployeeId = Convert.ToInt32(employeeId);
                    project.CompletedDate = DateTime.Today;
                    db.Projects.Update(project);
                }
                db.SaveChanges();
                TempData["Message"] = "Thank you For Ready "+projectId.Length+" project";
                TempData["Status"] = "1";


                return Json(new { Success = 1 });
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Success = 0, ex = ex.InnerException.InnerException.Message.ToString() });
            }

        }

        //Add notes to project
        [HttpPost]
        [Authorize(Roles = "Admin,Computer Operator")]
        public async Task<IActionResult> AddNotesInProject(string projectId, string notes)
        {
            try
            {
                Project project = await db.Projects.FindAsync(Convert.ToInt32(projectId));
                if (project == null)
                {
                    return NotFound();
                }
                project.Notes = notes;
              
                    db.Projects.Update(project);
                    await db.SaveChangesAsync();
               
                
                return Json(new { Success = 1 });

            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, ex = ex.InnerException.InnerException.Message.ToString() });
            }


        }

    }
}
