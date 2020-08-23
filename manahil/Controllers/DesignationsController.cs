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
    public class DesignationsController : Controller
    {
        private readonly DatabaseContext db;

        public DesignationsController(DatabaseContext context)
        {
            db = context;
        }

        // GET: Designations
        public async Task<IActionResult> Index()
        {
            return View(await db.Designations.ToListAsync());
        }

        // GET: Designations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await db.Designations
                .FirstOrDefaultAsync(m => m.DesignationId == id);
            if (designation == null)
            {
                return NotFound();
            }

            return View(designation);
        }

        // GET: Designations/Create
        public IActionResult Create()
        {
            ViewBag.Title = "Create";
            return View();
        }

        // POST: Designations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("DesignationId,Name")]*/ Designation designation)
        {
            if (ModelState.IsValid)
            {
                if (designation.DesignationId > 0)
                {
                    db.Update(designation);
                    await db.SaveChangesAsync();
                    TempData["Message"] = "Data Update Successfully";
                    TempData["Status"] = "2";
                }
                else
                {
                    try
                    {
                        db.Update(designation);
                        await db.SaveChangesAsync();

                        TempData["Message"] = "Data Added Successfully";
                        TempData["Status"] = "1";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DesignationExists(designation.DesignationId))
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
            return View(designation);
        }

        // GET: Designations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            var designation = await db.Designations.FindAsync(id);
            if (designation == null)
            {
                return NotFound();
            }
            return View("Create",designation);
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("DesignationId,Name")] Designation designation)
        //{
        //    if (id != designation.DesignationId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            db.Update(designation);
        //            await db.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!DesignationExists(designation.DesignationId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(designation);
        //}

        // GET: Designations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await db.Designations
                .FirstOrDefaultAsync(m => m.DesignationId == id);
            if (designation == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            return View("Create", designation);
        }

        // POST: Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var designation = await db.Designations.FindAsync(id);
                db.Designations.Remove(designation);
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

        private bool DesignationExists(int id)
        {
            return db.Designations.Any(e => e.DesignationId == id);
        }
    }
}
