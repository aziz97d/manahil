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
    [Authorize(Roles = "Admin,Accounting Manager")]
    public class TransferAccountsController : Controller
    {
        private readonly DatabaseContext _context;

        public TransferAccountsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TransferAccounts
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.TransferAccounts.Include(t => t.DepositAccount);
            return View(await databaseContext.ToListAsync());
        }

        //// GET: TransferAccounts/CreateOrEdit
        //[HttpGet]
        //public async Task<IActionResult> CreateOrEdit(int id=0)
        //{
        //    if (id>0)
        //    {
        //        var transferAccount = await _context.TransferAccounts.FindAsync(id);
        //        if (transferAccount == null)
        //        {
        //            return NotFound();
        //        }
        //        ViewData["DepositAccountId"] = new SelectList(_context.DepositAccounts, "DepositAccountId", "DepositCode", transferAccount.DepositAccountId);
        //        return View(transferAccount);
        //    }
        //    else
        //    {
        //        ViewData["DepositAccountId"] = new SelectList(_context.DepositAccounts, "DepositAccountId", "DepositCode");
        //        return View();
        //    }
            
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateOrEdit(TransferAccount transferAccount)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (transferAccount.TransferAccountId>0)
        //        {
        //            try
        //            {
        //                _context.Update(transferAccount);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!TransferAccountExists(transferAccount.TransferAccountId))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index", _context.TransferAccounts.ToList()) });
                    
        //        }
        //        else
        //        {
        //            DepositAccount depositAccount = _context.DepositAccounts.Find(transferAccount.DepositAccountId);
        //            depositAccount.Balance -= transferAccount.TransferAmount;
        //            _context.Update(depositAccount);
        //            _context.Add(transferAccount);
        //            await _context.SaveChangesAsync();
        //            ViewData["DepositAccountId"] = new SelectList(_context.DepositAccounts, "DepositAccountId", "DepositCode", transferAccount.DepositAccountId);
                    
        //        }
                
        //    }
        //    ViewData["DepositAccountId"] = new SelectList(_context.DepositAccounts, "DepositAccountId", "DepositCode", transferAccount.DepositAccountId);
        //    return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEdit", transferAccount) });
        //}

        

        // GET: TransferAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferAccount = await _context.TransferAccounts
                .Include(t => t.DepositAccount)
                .FirstOrDefaultAsync(m => m.TransferAccountId == id);
            if (transferAccount == null)
            {
                return NotFound();
            }

            return View(transferAccount);
        }

        // POST: TransferAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transferAccount = await _context.TransferAccounts.FindAsync(id);
            _context.TransferAccounts.Remove(transferAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransferAccountExists(int id)
        {
            return _context.TransferAccounts.Any(e => e.TransferAccountId == id);
        }
    }
}
