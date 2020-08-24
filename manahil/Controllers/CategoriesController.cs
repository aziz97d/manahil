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
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext db;

        public CategoriesController(DatabaseContext context)
        {
            db = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
             
            return View(await db.Categories.ToListAsync());
        }

        // GET: Cities/Details/5
     

        // GET: Cities/Create
        public IActionResult Create()
        {
           
            ViewBag.Title = "Create";
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("categoryId,Name,CountryId")]*/ Category category)
        {

            if (ModelState.IsValid)
            {
                if(category.CategoryId>0)
                {
                    db.Update(category);
                    await db.SaveChangesAsync();
                    TempData["Message"] = "Data Update Successfully";
                    TempData["Status"] = "2";
                }
                else
                {
                    try
                    {
                        db.Add(category);
                        await db.SaveChangesAsync();

                        TempData["Message"] = "Data Added Successfully";
                        TempData["Status"] = "1";
                        //ViewBag.Message = "Data Added Successfully";
                        //ViewBag.Status = "1";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!categoryExists(category.CategoryId))
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

            return View(category);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            var category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View("Create",category);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("categoryId,Name,CountryId")] category category)
        //{
        //    if (id != category.categoryId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            db.Update(category);
        //            await db.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!categoryExists(category.categoryId))
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
        //    ViewData["CountryId"] = new SelectList(db.Countries, "CountryId", "Name", category.CountryId);
        //    return View(category);
        //}

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var category = await db.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete";
            return View("Create",category);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var category = await db.Categories.FindAsync(id);
                db.Categories.Remove(category);
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

        private bool categoryExists(int id)
        {
            return db.Categories.Any(e => e.CategoryId == id);
        }
    }
}
