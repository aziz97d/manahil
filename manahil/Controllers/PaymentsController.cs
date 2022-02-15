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
    public class PaymentsController : Controller
    {
        private readonly DatabaseContext _context;

        public PaymentsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Payments.Include(p => p.Contractor);
            return View(await databaseContext.ToListAsync());
        }

        //GET Projects by donor Contractor and Category
        [HttpPost]
        public JsonResult GetProjectsByDonorContractorAndCategory(int donorId = 0, int contractorId = 0, int categoryId = 0)
        {
            try
            {
                var projects = _context.Projects.Where(d => d.DonorId == donorId && d.ContractorId == contractorId && d.CategoryId == categoryId).Include(d => d.Donor).Include(c => c.Contractor).Include(c => c.Category);


                return Json(new { Data = projects });
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Data = 0, ex = ex.InnerException.InnerException.Message.ToString() });
            }

        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Contractor)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            var contractorIdList = _context.Projects.Where(p => p.PaymentStatus == false && p.ContractorId != null).Select(c => c.ContractorId).Distinct().ToList();
            List<Contractor> contractors = new List<Contractor>();
            foreach(var cid in contractorIdList)
            {
                contractors.Add(_context.Contractors.Where(p => p.ContractorId == cid).FirstOrDefault());
            }
            ViewData["ContractorId"] = new SelectList(contractors, "ContractorId", "Name");
            return View();
        }

        public JsonResult GetDonorsByContractorId(int contractorId)
        {
            var donorList = _context.Projects.Where(p => p.PaymentStatus == false && p.ContractorId == contractorId).Select(c => c.DonorId).Distinct().ToList();
            List<Donor> donors = new List<Donor>();
            foreach (var dId in donorList)
            {
                donors.Add(_context.Donors.Where(p => p.DonorId == dId).FirstOrDefault());
            }

            return Json(new SelectList(donors, "DonorId", "Name"));
        }

        public JsonResult GetCategoryByDonorIdAndContractorId(int donorId, int contractorId)
        {
            var categoryList = _context.Projects.Where(p => p.PaymentStatus == false && p.DonorId == donorId && p.ContractorId == contractorId).Select(c => c.CategoryId).Distinct().ToList();
            List<Category> categories = new List<Category>();
            foreach (var dId in categoryList)
            {
                categories.Add(_context.Categories.Where(p => p.CategoryId == dId).FirstOrDefault());
            }

            return Json(new SelectList(categories, "CategoryId", "Name"));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,Invoice,PaymentDate,ContractorId,TotalAmount,Discount")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractorId"] = new SelectList(_context.Contractors, "ContractorId", "Name", payment.ContractorId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["ContractorId"] = new SelectList(_context.Contractors, "ContractorId", "Name", payment.ContractorId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,Invoice,PaymentDate,ContractorId,TotalAmount,Discount")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["ContractorId"] = new SelectList(_context.Contractors, "ContractorId", "Name", payment.ContractorId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Contractor)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }
}
