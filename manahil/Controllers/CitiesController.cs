﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using manahil.Models;

namespace manahil.Controllers
{
    public class CitiesController : Controller
    {
        private readonly DatabaseContext db;

        public CitiesController(DatabaseContext context)
        {
            db = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
             
            return View(await db.Cities.Include(c => c.Country).ToListAsync());
        }

        // GET: Cities/Details/5
     

        // GET: Cities/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.Title = "Create";
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("CityId,Name,CountryId")]*/ City city)
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

        // GET: Cities/Edit/5
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

        // POST: Cities/Edit/5
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

        // GET: Cities/Delete/5
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

        // POST: Cities/Delete/5
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
