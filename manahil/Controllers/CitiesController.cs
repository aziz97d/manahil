using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using manahil.Models;
using Microsoft.AspNetCore.Authorization;

namespace manahil.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CitiesController : Controller
    {
        private readonly DatabaseContext db;

        public CitiesController(DatabaseContext context)
        {
            ViewBag.Heading = "City";
            db = context;
        }

       
        public async Task<IActionResult> Index()
        {
            ViewBag.Heading = "City";
            return View(await db.Cities.Include(c => c.Country).ToListAsync());
        }

     


        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.Title = "Create";
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( City city)
        {

            if (ModelState.IsValid)
            {
                if(city.CityId>0)
                {
                    db.Update(city);
                    await db.SaveChangesAsync();
                    TempData["Message"] = "Data Update Successfully";
                    TempData["Status"] = "2";
                }
                else
                {
                    try
                    {
                        db.Add(city);
                        await db.SaveChangesAsync();

                        TempData["Message"] = "Data Added Successfully";
                        TempData["Status"] = "1";
                        //ViewBag.Message = "Data Added Successfully";
                        //ViewBag.Status = "1";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CityExists(city.CityId))
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
            ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name", city.CountryId);
            return View(city);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            var city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name", city.CountryId);
            return View("Create",city);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var city = await db.Cities
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.CityId == id);
            if (city == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name", city.CountryId);
            return View("Create",city);
        }

  
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var city = await db.Cities.FindAsync(id);
                db.Cities.Remove(city);
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

        private bool CityExists(int id)
        {
            return db.Cities.Any(e => e.CityId == id);
        }
    }
}
