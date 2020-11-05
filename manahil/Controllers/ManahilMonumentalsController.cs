using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using manahil.Models;
using Microsoft.AspNetCore.Authorization;

namespace manahil.Controllers
{
    
    public class ManahilMonumentalsController : Controller
    {
        private readonly DatabaseContext _context;

        public ManahilMonumentalsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: ManahilMonumentals
        [Authorize(Roles = "Admin,Accounting Manager")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ManahilMonumentals.ToListAsync());
        }
        [Authorize(Roles = "Admin,Accounting Manager,Management")]
        public async Task<IActionResult> ViewMonumental()
        {
            return View(await _context.ManahilMonumentals.ToListAsync());
        }

        // GET: ManahilMonumentals/Details/5
        [Authorize(Roles = "Admin,Accounting Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manahilMonumental = await _context.ManahilMonumentals
                .FirstOrDefaultAsync(m => m.ManahilMonumentalId == id);
            if (manahilMonumental == null)
            {
                return NotFound();
            }

            return View(manahilMonumental);
        }

        // GET: ManahilMonumentals/Create
        [Authorize(Roles = "Admin,Accounting Manager")]
        public async Task<IActionResult> CreateOrEdit(int? id)
        {
            if (id>0)
            {
                var manahilMonumental = await _context.ManahilMonumentals.FindAsync(id);
                if (manahilMonumental == null)
                {
                    return NotFound();
                }
                return View(manahilMonumental);
            }
            else
            {
                string monumentalDateCode = DateTime.Now.ToString("MMyy");
                int todayYear = DateTime.Now.Year;
                int thisyearMonumental = _context.ManahilMonumentals.Count(d => d.Date.Year == todayYear);
                ManahilMonumental manahilMonumental = new ManahilMonumental();
                manahilMonumental.MonumentalNo = "আল মানাহিল/প্রকা/"+ monumentalDateCode+"/"+ (thisyearMonumental+1);
                manahilMonumental.Date = DateTime.Now;
                return View(manahilMonumental);
            }
            
        }

        // POST: ManahilMonumentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Accounting Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(int id, ManahilMonumental manahilMonumental)
        {
            if (ModelState.IsValid)
            {
                if (id>0)
                {
                    if (id != manahilMonumental.ManahilMonumentalId)
                    {
                        return NotFound();
                    }
                    try
                    {
                        _context.Update(manahilMonumental);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ManahilMonumentalExists(manahilMonumental.ManahilMonumentalId))
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
                else
                {
                    _context.Add(manahilMonumental);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return View(manahilMonumental);
        }




        // GET: ManahilMonumentals/Delete/5
        [Authorize(Roles = "Admin,Accounting Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manahilMonumental = await _context.ManahilMonumentals
                .FirstOrDefaultAsync(m => m.ManahilMonumentalId == id);
            if (manahilMonumental == null)
            {
                return NotFound();
            }

            return View(manahilMonumental);
        }

        // POST: ManahilMonumentals/Delete/5
        [Authorize(Roles = "Admin,Accounting Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manahilMonumental = await _context.ManahilMonumentals.FindAsync(id);
            _context.ManahilMonumentals.Remove(manahilMonumental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManahilMonumentalExists(int id)
        {
            return _context.ManahilMonumentals.Any(e => e.ManahilMonumentalId == id);
        }
    }
}
