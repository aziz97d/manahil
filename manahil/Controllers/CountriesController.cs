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
    public class CountriesController : Controller
    {
        private readonly DatabaseContext db;

        public CountriesController(DatabaseContext context)
        {
            db = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await db.Countries.ToListAsync());
        }

        // GET: Countries/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var city = await db.Countries
        //        .Include(c => c.Country)
        //        .FirstOrDefaultAsync(m => m.CityId == id);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(city);
        //}

        // GET: Countries/Create
        public IActionResult Create()
        {
            ViewBag.Title = "Create";
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("CityId,Name,CountryId")]*/ Country country)
        {

            if (ModelState.IsValid)
            {
                if (country.CountryId > 0)
                {
                    db.Update(country);
                    await db.SaveChangesAsync();
                    TempData["Message"] = "Data Update Successfully";
                    TempData["Status"] = "2";
                }
                else
                {
                    try
                    {
                        db.Add(country);
                        await db.SaveChangesAsync();

                        ViewBag.Status = "Data Added Successfully";
                        ViewBag.Message = "1";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CityExists(country.CountryId))
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
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            var country = await db.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            
            return View("Create", country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("CityId,Name,CountryId")] City city)
        //{
        //    if (id != city.CityId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            db.Update(city);
        //            await db.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CityExists(city.CityId))
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
        //    ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name", city.CountryId);
        //    return View(city);
        //}

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await db.Countries
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (country == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            return View("Create", country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var country = await db.Countries.FindAsync(id);
                db.Countries.Remove(country);
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
            return db.Countries.Any(e => e.CountryId == id);
        }
    }
}
