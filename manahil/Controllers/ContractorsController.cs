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
    public class ContractorsController : Controller
    {
        private readonly DatabaseContext db;

        public ContractorsController(DatabaseContext context)
        {
            db = context;
        }

        // GET: Contractors
        public async Task<IActionResult> Index()
        {
             
            return View(await db.Contractors.Include(c => c.City).ToListAsync());
        }

        // GET: Contractors/Details/5
     

        // GET: Contractors/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name");
            ViewBag.Title = "Create";
            return View();
        }

        // POST: Contractors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("CityId,Name,CityId")]*/ Contractor contractor)
        {

            if (ModelState.IsValid)
            {
                if(contractor.ContractorId>0)
                {
                    db.Update(contractor);
                    await db.SaveChangesAsync();
                    TempData["Message"] = "Data Update Successfully";
                    TempData["Status"] = "2";
                }
                else
                {
                    try
                    {
                        db.Add(contractor);
                        await db.SaveChangesAsync();

                        TempData["Message"] = "Data Added Successfully";
                        TempData["Status"] = "1";
                        //ViewBag.Message = "Data Added Successfully";
                        //ViewBag.Status = "1";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ContractorExists(contractor.ContractorId))
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
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", contractor.CityId);
            return View(contractor);
        }

        // GET: Contractors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            var contractor = await db.Contractors.FindAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", contractor.CityId);
            return View("Create", contractor);
        }
    

        // GET: Contractors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var contractor = await db.Contractors
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.ContractorId == id);
            if (contractor == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", contractor.CityId);
            return View("Create", contractor);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var contractor = await db.Contractors.FindAsync(id);
                db.Contractors.Remove(contractor);
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

        private bool ContractorExists(int id)
        {
            return db.Contractors.Any(e => e.CityId == id);
        }
    }
}
