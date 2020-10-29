using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using manahil.Models;
using manahil.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace manahil.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DonorsController : Controller
    {
        private readonly DatabaseContext db;

        public DonorsController(DatabaseContext context)
        {
            db = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            List<DonorViewModel> donorViewModels = new List<DonorViewModel>();
            List<Donor> donors = await db.Donors.ToListAsync();
            foreach(Donor donor in donors)
            {
                DonorViewModel donorModel = new DonorViewModel { };
                donorModel.DonorId = donor.DonorId;
                donorModel.Name = donor.Name;
                if(donor.CountryId!=null)
                    donorModel.Country = db.Countries.Find(donor.CountryId).Name;
                donorModel.Image = donor.Image;
                donorModel.TotalProject = db.Projects.Count(d => d.DonorId == donor.DonorId);
                donorModel.DeliveredProject = db.Projects.Count(d => d.DonorId == donor.DonorId && d.EmployeeId != null);

                donorViewModels.Add(donorModel);
            }

            //return View(await db.Donors.Include(c => c.Country).Include(d=>d.Projects).ToListAsync());
            //return View(await db.Donors.Include(c => c.Country).ToListAsync());
            return View(donorViewModels);
        }

        // GET: Cities/Details/5
     

        // GET: Cities/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.Title = "Create";
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Donor donor)
        {

            if (ModelState.IsValid)
            {
                if(donor.DonorId>0)
                {
                    db.Update(donor);
                    await db.SaveChangesAsync();
                    TempData["Message"] = "Data Update Successfully";
                    TempData["Status"] = "2";
                }
                else
                {
                    try
                    {
                        db.Add(donor);
                        await db.SaveChangesAsync();

                        TempData["Message"] = "Data Added Successfully";
                        TempData["Status"] = "1";
                        //ViewBag.Message = "Data Added Successfully";
                        //ViewBag.Status = "1";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!donorExists(donor.DonorId))
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
            ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name", donor.CountryId);
            return View(donor);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            var donor = await db.Donors.FindAsync(id);
            if (donor == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name", donor.CountryId);
            return View("Create",donor);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("donorId,Name,CountryId")] donor donor)
        //{
        //    if (id != donor.donorId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            db.Update(donor);
        //            await db.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!donorExists(donor.donorId))
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
        //    ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name", donor.CountryId);
        //    return View(donor);
        //}

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var donor = await db.Donors
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.DonorId == id);
            if (donor == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name", donor.CountryId);
            return View("Create",donor);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var donor = await db.Donors.FindAsync(id);
                db.Donors.Remove(donor);
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

        private bool donorExists(int id)
        {
            return db.Donors.Any(e => e.DonorId == id);
        }
    }
}
