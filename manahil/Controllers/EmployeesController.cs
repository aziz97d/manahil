using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using manahil.Models;

namespace manahil.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DatabaseContext db;

        public EmployeesController(DatabaseContext context)
        {
            db = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
             
            return View(await db.Employees.Include(c => c.City).Include(d =>d.Designation).ToListAsync());
        }

        // GET: Cities/Details/5
     

        // GET: Cities/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name");
            ViewData["DesignationId"] = new SelectList(db.Designations, "DesignationId", "Name");
            ViewBag.Title = "Create";
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("CityId,Name,CityId")]*/ Employee employee)
        {

            if (ModelState.IsValid)
            {
                if(employee.EmployeeId>0)
                {
                    db.Update(employee);
                    await db.SaveChangesAsync();
                    TempData["Message"] = "Data Update Successfully";
                    TempData["Status"] = "2";
                }
                else
                {
                    try
                    {
                        db.Add(employee);
                        await db.SaveChangesAsync();

                        TempData["Message"] = "Data Added Successfully";
                        TempData["Status"] = "1";
                        //ViewBag.Message = "Data Added Successfully";
                        //ViewBag.Status = "1";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeExists(employee.EmployeeId))
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
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", employee.CityId);
            ViewData["DesignationId"] = new SelectList(db.Designations, "DesignationId", "Name");
            return View(employee);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            var employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", employee.CityId);
            ViewData["DesignationId"] = new SelectList(db.Designations, "DesignationId", "Name");
            return View("Create", employee);
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
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", employee.CityId);
            ViewData["DesignationId"] = new SelectList(db.Designations, "DesignationId", "Name");
            return View("Create", employee);
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
