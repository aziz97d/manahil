using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using manahil.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace manahil.Controllers
{
    public class ApprovalProjectsController : Controller
    {
        private readonly DatabaseContext _context;

        public ApprovalProjectsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: ApprovalProjects
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.ApprovalProjects;
            return View(await databaseContext.ToListAsync());
        }

        public async Task<IActionResult> ApprovalProjectsList()
        {
            var databaseContext = _context.ApprovalProjects;
            return View(await databaseContext.ToListAsync());
        }

        //Aopproval project view
        [HttpGet]
        public async Task<IActionResult> ViewApprovalProject(int id = 0)
        {

                var approvalProjects = await _context.ApprovalProjects.FindAsync(id);
                if (approvalProjects == null)
                {
                    return NotFound();
                }
                
                return View(approvalProjects);

        }

        // GET: ApprovalProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalProjects = await _context.ApprovalProjects
                .FirstOrDefaultAsync(m => m.ApprovalProjectId == id);
            if (approvalProjects == null)
            {
                return NotFound();
            }

            return View(approvalProjects);
        }

        // GET: ApprovalProjects/Create
        public IActionResult Create()
        {
            ViewData["DonorId"] = new SelectList(_context.Donors, "DonorId", "Name");
            return View();
        }

        // POST: ApprovalProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApprovalProjects approvalProjects)
        {
            //var err = ModelState.Where(s=>s.Value.Errors.Any()).Select(x => new { x.Key, x.Value.Errors });
            if (ModelState.IsValid)
            {
                _context.Add(approvalProjects);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(approvalProjects);
        }

        // GET: ApprovalProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalProjects = await _context.ApprovalProjects.FindAsync(id);
            if (approvalProjects == null)
            {
                return NotFound();
            }
            
            return View(approvalProjects);
        }

        // POST: ApprovalProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApprovalProjectId,SerialNo,ProjectName,MonumentalNo,ApprovalDate,StartDate,EndtDate,DonorId,AuditStatus,CertificateStatus,ApprovedMoney,CurrencyRate,TotalAcceptedMoney,ExpenseAmount,ProjectImplementAddress")] ApprovalProjects approvalProjects)
        {
            if (id != approvalProjects.ApprovalProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(approvalProjects);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApprovalProjectsExists(approvalProjects.ApprovalProjectId))
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
            
            return View(approvalProjects);
        }

        // GET: ApprovalProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalProjects = await _context.ApprovalProjects
                .FirstOrDefaultAsync(m => m.ApprovalProjectId == id);
            if (approvalProjects == null)
            {
                return NotFound();
            }

            return View(approvalProjects);
        }

        // POST: ApprovalProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var approvalProjects = await _context.ApprovalProjects.FindAsync(id);
            _context.ApprovalProjects.Remove(approvalProjects);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApprovalProjectsExists(int id)
        {
            return _context.ApprovalProjects.Any(e => e.ApprovalProjectId == id);
        }
    }
}
