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
    public class BdDonationsController : Controller
    {
        private readonly DatabaseContext _context;

        public BdDonationsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: BdDonations
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.BdDonations.Include(b => b.Category).Include(p=>p.PaymentType);
            return View(await databaseContext.ToListAsync());
        }

        // GET: BdDonations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bdDonation = await _context.BdDonations
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BdDonationId == id);
            if (bdDonation == null)
            {
                return NotFound();
            }

            return View(bdDonation);
        }

        // GET: BdDonations/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "PaymentTypeId", "Name");
            return View();
        }

        // POST: BdDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( BdDonation bdDonation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bdDonation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", bdDonation.CategoryId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "PaymentTypeId", "Name");
            return View(bdDonation);
        }

        // GET: BdDonations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bdDonation = await _context.BdDonations.FindAsync(id);
            if (bdDonation == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", bdDonation.CategoryId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "PaymentTypeId", "Name");
            return View(bdDonation);
        }

        // POST: BdDonations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  BdDonation bdDonation)
        {
            if (id != bdDonation.BdDonationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bdDonation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BdDonationExists(bdDonation.BdDonationId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", bdDonation.CategoryId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "PaymentTypeId", "Name");
            return View(bdDonation);
        }

        // GET: BdDonations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bdDonation = await _context.BdDonations
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BdDonationId == id);
            if (bdDonation == null)
            {
                return NotFound();
            }

            return View(bdDonation);
        }

        // POST: BdDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bdDonation = await _context.BdDonations.FindAsync(id);
            _context.BdDonations.Remove(bdDonation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BdDonationExists(int id)
        {
            return _context.BdDonations.Any(e => e.BdDonationId == id);
        }
    }
}
