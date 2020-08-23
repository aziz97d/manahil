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
    public class ThanasController : Controller
    {
        private readonly DatabaseContext db;

        public ThanasController(DatabaseContext context)
        {
            db = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
             
            return View(await db.Thanas.Include(c => c.City).ToListAsync());
        }

        // GET: Cities/Details/5
     

        // GET: Cities/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name");
            ViewBag.Title = "Create";
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("thanaId,Name,CountryId")]*/ Thana thana)
        {

            if (ModelState.IsValid)
            {
                if(thana.ThanaId>0)
                {
                    db.Update(thana);
                    await db.SaveChangesAsync();
                    TempData["Message"] = "Data Update Successfully";
                    TempData["Status"] = "2";
                }
                else
                {
                    try
                    {
                        db.Add(thana);
                        await db.SaveChangesAsync();

                        TempData["Message"] = "Data Added Successfully";
                        TempData["Status"] = "1";
                        //ViewBag.Message = "Data Added Successfully";
                        //ViewBag.Status = "1";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!thanaExists(thana.ThanaId))
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
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", thana.CityId);
            return View(thana);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            var thana = await db.Thanas.FindAsync(id);
            if (thana == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", thana.CityId);
            return View("Create",thana);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("thanaId,Name,CountryId")] thana thana)
        //{
        //    if (id != thana.thanaId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            db.Update(thana);
        //            await db.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!thanaExists(thana.thanaId))
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
        //    ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name", thana.CountryId);
        //    return View(thana);
        //}

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var thana = await db.Thanas
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.ThanaId == id);
            if (thana == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", thana.CityId);
            return View("Create",thana);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var thana = await db.Thanas.FindAsync(id);
                db.Thanas.Remove(thana);
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

        private bool thanaExists(int id)
        {
            return db.Thanas.Any(e => e.CityId == id);
        }
    }
}
