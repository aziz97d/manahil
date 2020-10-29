using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using manahil.Models;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace manahil.Controllers
{
    
    public class ManageDrivesController : Controller
    {
        private readonly DatabaseContext db;


        public ManageDrivesController(DatabaseContext context)
        {
            db = context;
        }

        // GET: ManageDrives
        public async Task<IActionResult> Index()
        {
             
            return View(await db.ManageDrives.Include(d => d.Donor).ToListAsync());
        }

        // GET: ManageDrives/Details/5


        // GET: ManageDrives/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {

            ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
            ViewBag.Title = "Create";
            return View(new ManageDrive());
        }

        // POST: ManageDrives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("manageDriveId,Name,DonorId")]*/ ManageDrive manageDrive)
        {
            string link = manageDrive.DriveLink;
            manageDrive.DriveLink = "http://"+link;

            if (ModelState.IsValid)
            {
                if(manageDrive.ManageDriveId>0)
                {
                    db.Update(manageDrive);
                    await db.SaveChangesAsync();
                    TempData["Message"] = "Data Update Successfully";
                    TempData["Status"] = "2";
                }
                else
                {
                    try
                    {
                        db.Add(manageDrive);
                        await db.SaveChangesAsync();

                        TempData["Message"] = "Data Added Successfully";
                        TempData["Status"] = "1";
                        //ViewBag.Message = "Data Added Successfully";
                        //ViewBag.Status = "1";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!manageDriveExists(manageDrive.ManageDriveId))
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
            ViewData["DonorId"] = new SelectList(db.ManageDrives, "DonorId", "Name", manageDrive.DonorId);
            return View(manageDrive);
        }

        // GET: ManageDrives/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            var manageDrive = await db.ManageDrives.FindAsync(id);
            if (manageDrive == null)
            {
                return NotFound();
            }
            ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
            return View("Create",manageDrive);
        }

        // POST: ManageDrives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("manageDriveId,Name,DonorId")] manageDrive manageDrive)
        //{
        //    if (id != manageDrive.manageDriveId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            db.Update(manageDrive);
        //            await db.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!manageDriveExists(manageDrive.manageDriveId))
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
        //    ViewData["DonorId"] = new SelectList(db.ManageDrives, "DonorId", "Name", manageDrive.DonorId);
        //    return View(manageDrive);
        //}

        // GET: ManageDrives/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var manageDrive = await db.ManageDrives
                .Include(d => d.Donor)
                .FirstOrDefaultAsync(m => m.ManageDriveId == id);
            if (manageDrive == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
            return View("Create",manageDrive);
        }

        // POST: ManageDrives/Delete/5
        [HttpPost, ActionName("Delete")]
        //  [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var manageDrive = await db.ManageDrives.FindAsync(id);
                db.ManageDrives.Remove(manageDrive);
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

        private bool manageDriveExists(int id)
        {
            return db.ManageDrives.Any(e => e.ManageDriveId == id);
        }
    }
}
