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
    public class ProjectPricingsController : Controller
    {
        private readonly DatabaseContext db;

        public ProjectPricingsController(DatabaseContext context)
        {
            db = context;
        }

        // GET: ProjectPricings
        public async Task<IActionResult> Index()
        {
            var databaseContext = db.ProjectPricings.Include(p => p.Category).Include(p => p.Contractor).Include(p => p.Donor);
            return View(await databaseContext.ToListAsync());
        }

        // GET: ProjectPricings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectPricing = await db.ProjectPricings
                .Include(p => p.Category)
                .Include(p => p.Contractor)
                .Include(p => p.Donor)
                .FirstOrDefaultAsync(m => m.ProectPricingId == id);
            if (projectPricing == null)
            {
                return NotFound();
            }

            return View(projectPricing);
        }

        // GET: ProjectPricings/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name");
            ViewData["ContractorId"] = new SelectList(db.Contractors, "ContractorId", "Name");
            ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name");
            return View();
        }

        // POST: ProjectPricings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProectPricingId,DonorId,CategoryId,ContractorId,Price")] ProjectPricing projectPricing)
        {
            if (ModelState.IsValid)
            {
                db.Add(projectPricing);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name", projectPricing.CategoryId);
            ViewData["ContractorId"] = new SelectList(db.Contractors, "ContractorId", "Name", projectPricing.ContractorId);
            ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name", projectPricing.DonorId);
            return View(projectPricing);
        }

        // GET: ProjectPricings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectPricing = await db.ProjectPricings.FindAsync(id);
            if (projectPricing == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name", projectPricing.CategoryId);
            ViewData["ContractorId"] = new SelectList(db.Contractors, "ContractorId", "Name", projectPricing.ContractorId);
            ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name", projectPricing.DonorId);
            return View(projectPricing);
        }

        // POST: ProjectPricings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ProectPricingId,DonorId,CategoryId,ContractorId,Price")] ProjectPricing projectPricing)
        {
            if (id != projectPricing.ProectPricingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(projectPricing);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectPricingExists(projectPricing.ProectPricingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "Name", projectPricing.CategoryId);
            ViewData["ContractorId"] = new SelectList(db.Contractors, "ContractorId", "Name", projectPricing.ContractorId);
            ViewData["DonorId"] = new SelectList(db.Donors, "DonorId", "Name", projectPricing.DonorId);
            return View(projectPricing);
        }

        // GET: ProjectPricings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectPricing = await db.ProjectPricings
                .Include(p => p.Category)
                .Include(p => p.Contractor)
                .Include(p => p.Donor)
                .FirstOrDefaultAsync(m => m.ProectPricingId == id);
            if (projectPricing == null)
            {
                return NotFound();
            }

            return View(projectPricing);
        }

        // POST: ProjectPricings/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectPricing = await db.ProjectPricings.FindAsync(id);
            db.ProjectPricings.Remove(projectPricing);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectPricingExists(int id)
        {
            return db.ProjectPricings.Any(e => e.ProectPricingId == id);
        }


        [HttpPost]
        public IActionResult GetPriceByCategoryIdAndDonorId(int categoryId, int donorId, int? contractorId)
        {
            try
            {
                var projectPrice = db.ProjectPricings.Where(p => p.CategoryId == categoryId && p.DonorId == donorId && p.ContractorId == contractorId).Select(p => p.Price);
                if(projectPrice==null)
                {
                    projectPrice = db.ProjectPricings.Where(p => p.CategoryId == categoryId && p.DonorId == donorId).Select(p => p.Price); 
                }
                return Json(new {Success = projectPrice });
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Success = 0, ex = ex.InnerException.InnerException.Message.ToString() });
            }
            
        }
    }
}
