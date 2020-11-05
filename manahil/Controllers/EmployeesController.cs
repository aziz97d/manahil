using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using manahil.Models;
using manahil.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace manahil.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private readonly DatabaseContext db;
        private readonly IWebHostEnvironment env;

        public EmployeesController(DatabaseContext context, IWebHostEnvironment env)
        {
            db = context;
            this.env = env;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            List<EmployeeViewModel> employeeViews = new List<EmployeeViewModel>();
            var employees = await db.Employees.ToListAsync();
            foreach (Employee employee in employees)
            {
                EmployeeViewModel empvm = new EmployeeViewModel
                {
                    EmployeeId = employee.EmployeeId,
                    Name = employee.Name,
                    Email = employee.Email,
                    CityId = employee.CityId,
                    Contact = employee.Contact,
                    Address = employee.Address,
                    DesignationId = employee.DesignationId,
                    Image = employee.Image,
                    Status = employee.Status,
                    TodayCompleteProjects = db.Projects.Count(e=>e.EmployeeId == employee.EmployeeId && e.CompletedDate == DateTime.Today),
                    TotalCompleteProjects = db.Projects.Count(e => e.EmployeeId == employee.EmployeeId)
                };

                employeeViews.Add(empvm);
                
            }

            return View(employeeViews);
        }

        // GET: Cities/Details/5
     

        //// GET: Cities/Create
        //public IActionResult Create()
        //{
        //    ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name");
        //    ViewData["DesignationId"] = new SelectList(db.Designations, "DesignationId", "Name");
        //    ViewBag.Title = "Create";
        //    return View();
        //}

        //// POST: Cities/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create( EmployeeCreateViewModels employee)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        string uniqueFileName = ProcessUploadFile(employee);
        //        Employee newEmployee = new Employee
        //        {
        //            Name = employee.Name,
        //            Email = employee.Email,
        //            Contact = employee.Contact,
        //            Address = employee.Address,
        //            DesignationId = employee.DesignationId,
        //            CityId = employee.CityId,
        //            Image = uniqueFileName,
        //            Status = employee.Status

        //        };
        //        db.Add(newEmployee);
        //        await db.SaveChangesAsync();
        //        TempData["Message"] = "Data Added Successfully";
        //        TempData["Status"] = "1";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", employee.CityId);
        //    ViewData["DesignationId"] = new SelectList(db.Designations, "DesignationId", "Name", employee.DesignationId);
        //    return View(employee);
        //}

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employee = await db.Employees.FindAsync(id);
            EmployeeEditViewModels employeeEdit = new EmployeeEditViewModels
            {
                Name = employee.Name,
                Email = employee.Email,
                Contact = employee.Contact,
                Address = employee.Address,
                DesignationId = employee.DesignationId,
                CityId = employee.CityId,
                ExistingPhotoPath = employee.Image,
                Status = employee.Status
            };
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", employee.CityId);
            ViewData["DesignationId"] = new SelectList(db.Designations, "DesignationId", "Name",employee.DesignationId);
            return View("Edit", employeeEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeEditViewModels employee)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(employee);
                Employee updateEmployee = await db.Employees.FindAsync(employee.Id);
                updateEmployee.Name = employee.Name;
                updateEmployee.Email = employee.Email;
                updateEmployee.Contact = employee.Contact;
                updateEmployee.Address = employee.Address;
                updateEmployee.CityId = employee.CityId;
                updateEmployee.Status = employee.Status;
                if (employee.Image != null)
                {
                    if (employee.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(env.WebRootPath + "images" + employee.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    updateEmployee.Image = ProcessUploadFile(employee);
                }
                if (updateEmployee == null)
                {
                    return NotFound();
                }
                db.Update(updateEmployee);
                await db.SaveChangesAsync();
                TempData["Message"] = "Data Updated Successfully";
                TempData["Status"] = "2";
                return RedirectToAction(nameof(Index));
            }

            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", employee.CityId);
            ViewData["DesignationId"] = new SelectList(db.Designations, "DesignationId", "Name", employee.DesignationId);
            return View(employee);
        }

        private string ProcessUploadFile(EmployeeCreateViewModels employee)
        {
            string uniqueFileName = null;
            if (employee.Image != null)
            {
                string uploadFolder = Path.Combine(env.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + employee.Image.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                employee.Image.CopyTo(new FileStream(filePath, FileMode.Create));

            }

            return uniqueFileName;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var employee = await db.Employees
                .Include(c => c.City).Include(d => d.Designation)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            EmployeeEditViewModels employeeEditViewModels = new EmployeeEditViewModels
            {
                Id = employee.EmployeeId,
                Name = employee.Name,
                Email = employee.Email,
                Contact = employee.Contact,
                Address = employee.Address,
                ExistingPhotoPath = employee.Image,
                CityId = employee.CityId,
                Status = employee.Status

            };

            ViewBag.Title = "Delete";
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", employee.CityId);
            ViewData["DesignationId"] = new SelectList(db.Designations, "DesignationId", "Name", employee.DesignationId);
            return View("Edit", employeeEditViewModels);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var employee = await db.Employees.FindAsync(id);
                db.Employees.Remove(employee);
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

        private bool EmployeeExists(int id)
        {
            return db.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
