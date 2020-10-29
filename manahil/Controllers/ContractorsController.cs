using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using manahil.Models;
using manahil.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace manahil.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContractorsController : Controller
    {
        private readonly DatabaseContext db;
        private readonly IWebHostEnvironment env;

        public ContractorsController(DatabaseContext context, IWebHostEnvironment env)
        {
            db = context;
            this.env = env;
        }

        // GET: Contractors
        public async Task<IActionResult> Index()
        {

            List<ContractorOnlyViewModel> ContractorOnlyViewModels = new List<ContractorOnlyViewModel>();
            List<Contractor> contractors = await db.Contractors.ToListAsync();
            foreach (Contractor contractor in contractors)
            {
                ContractorOnlyViewModel ctrModel = new ContractorOnlyViewModel { };
                ctrModel.ContractorId = contractor.ContractorId;
                ctrModel.Name = contractor.Name;
                if (contractor.CityId != null)
                    ctrModel.City = db.Cities.Find(contractor.CityId).Name;
                ctrModel.Image = contractor.Image;
                ctrModel.TotalProject = db.Projects.Count(d => d.ContractorId == contractor.ContractorId);
                ctrModel.DeliveredProject = db.Projects.Count(d => d.ContractorId == contractor.ContractorId && d.EmployeeId != null);

                ContractorOnlyViewModels.Add(ctrModel);
            }
            return View(ContractorOnlyViewModels);
        }

     

        //// GET: Contractors/Create
        //public IActionResult Create()
        //{
        //    ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name");
        //    ViewBag.Title = "Create";
        //    return View();
        //}

        //// POST: Contractors/Create
        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create( ContractorViewModels contractor)
        //{

        //    //if (ModelState.IsValid)
        //    //{
        //    //    if(contractor.ContractorId>0)
        //    //    {
        //    //        db.Update(contractor);
        //    //        await db.SaveChangesAsync();
        //    //        TempData["Message"] = "Data Update Successfully";
        //    //        TempData["Status"] = "2";
        //    //    }
        //    //    else
        //    //    {
        //    //        try
        //    //        {
        //    //            db.Add(contractor);
        //    //            await db.SaveChangesAsync();

        //    //            TempData["Message"] = "Data Added Successfully";
        //    //            TempData["Status"] = "1";
        //    //            //ViewBag.Message = "Data Added Successfully";
        //    //            //ViewBag.Status = "1";
        //    //        }
        //    //        catch (DbUpdateConcurrencyException)
        //    //        {
        //    //            if (!ContractorExists(contractor.ContractorId))
        //    //            {
        //    //                return NotFound();
        //    //            }
        //    //            else
        //    //            {
        //    //                throw;
        //    //            }
        //    //        }

        //    //    }

        //    //    return RedirectToAction(nameof(Index));
        //    //}
        //    if (ModelState.IsValid)
        //    {
        //        string uniqueFileName = ProcessUploadFile(contractor);
        //        Contractor newContractor = new Contractor
        //        {
        //            Name = contractor.Name,
        //            Email = contractor.Email,
        //            PrintEmail = contractor.PrintEmail,
        //            Contact = contractor.Contact,
        //            Address = contractor.Address,
        //            Image = uniqueFileName,
        //            CityId = contractor.CityId
                    
        //        };
        //        db.Add(newContractor);
        //        await db.SaveChangesAsync();
        //        TempData["Message"] = "Data Added Successfully";
        //        TempData["Status"] = "1";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", contractor.CityId);
        //    return View(contractor);
        //}

        // GET: Contractors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await db.Contractors.FindAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }
            ContractorEditViewModels contractorEdit = new ContractorEditViewModels
            {
                Id = contractor.ContractorId,
                Name = contractor.Name,
                Email = contractor.Email,
                PrintEmail = contractor.PrintEmail,
                Contact = contractor.Contact,
                Address = contractor.Address,
                CityId = contractor.CityId,
                ExistingPhotoPath = contractor.Image                
            };
            
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", contractor.CityId);
            return View("Edit", contractorEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( ContractorEditViewModels contractor)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(contractor);
                Contractor updateContractor = await db.Contractors.FindAsync(contractor.Id);
                updateContractor.Name = contractor.Name;
                updateContractor.Email = contractor.Email;
                updateContractor.PrintEmail = contractor.PrintEmail;
                updateContractor.Contact = contractor.Contact;
                updateContractor.Address = contractor.Address;
                updateContractor.CityId = contractor.CityId;
                if(contractor.Image != null)
                {
                    if(contractor.ExistingPhotoPath != null)
                    {
                        string filePath=Path.Combine(env.WebRootPath+"images"+contractor.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    updateContractor.Image = ProcessUploadFile(contractor);
                }
             
                db.Update(updateContractor);
                await db.SaveChangesAsync();
                TempData["Message"] = "Data Updated Successfully";
                TempData["Status"] = "2";
                return RedirectToAction(nameof(Index));
            }

            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", contractor.CityId);
            return View(contractor);
        }

        //Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await db.Contractors.FindAsync(id);
            
            return View(contractor);
        }

        private string ProcessUploadFile(ContractorViewModels contractor)
        {
            string uniqueFileName = null;
            if (contractor.Image != null)
            {
                string uploadFolder = Path.Combine(env.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + contractor.Image.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                contractor.Image.CopyTo(new FileStream(filePath, FileMode.Create));

            }

            return uniqueFileName;
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
            ContractorEditViewModels contractorEditView = new ContractorEditViewModels
            {
                Id = contractor.ContractorId,
                Name=contractor.Name,
                Email=contractor.Email,
                PrintEmail = contractor.PrintEmail,
                Contact = contractor.Contact,
                Address =contractor.Address,
                ExistingPhotoPath = contractor.Image,
                CityId = contractor.CityId

            };

            ViewBag.Title = "Delete";
            ViewData["CityId"] = new SelectList(db.Cities, "CityId", "Name", contractor.CityId);
            return View("Edit", contractorEditView);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                
                var contractor = await db.Contractors.FindAsync(id);
                if (contractor == null)
                {
                    return NotFound();
                }
                db.Contractors.Remove(contractor);
                db.SaveChanges();

                ///image remove code remainig
                //if (imgPath != null)
                //{
                //    string filePath = Path.Combine(env.WebRootPath + "images" + imgPath);
                //    System.IO.File.Delete(filePath);
                //}

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
