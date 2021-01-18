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
    
    public class DepositAccountsController : Controller
    {
        private readonly DatabaseContext _context;

        public DepositAccountsController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,Accounting Manager")]
        // GET: DepositAccounts
        public async Task<IActionResult> Index()
        {
            //var depositAccounts = await _context.DepositAccounts.ToListAsync();
           
            //List<DepositAccount> depositAccountsList = new List<DepositAccount>();
            //foreach (DepositAccount depositAccount in depositAccounts)
            //{
            //    DepositAccount dpA = new DepositAccount
            //    {
            //        DepositAccountId = depositAccount.DepositAccountId,
            //        DepositCode = depositAccount.DepositCode,
            //        DepositAmount = depositAccount.DepositAmount,
            //        DepositDate = depositAccount.DepositDate,
            //        DepositType = depositAccount.DepositType,
            //        Donor = depositAccount.Donor,
            //        Balance = depositAccount.DepositAmount - _context.TransferAccounts.Where(c => c.DepositAccountId == depositAccount.DepositAccountId).Sum(s => s.TransferAmount)

            //    };
            //    depositAccountsList.Add(dpA);
            //}

            return View(await _context.DepositAccounts.ToListAsync());
        }

        [Authorize(Roles = "Admin,Accounting Manager,Management")]
        public async Task<IActionResult> DepositView()
        {
            return View(await _context.DepositAccounts.ToListAsync());
        }

        // GET: DepositAccounts/Details/5
        [Authorize(Roles = "Admin,Accounting Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depositAccount = await _context.DepositAccounts
                .FirstOrDefaultAsync(m => m.DepositAccountId == id);
            if (depositAccount == null)
            {
                return NotFound();
            }

            return View(depositAccount);
        }

        // GET: DepositAccounts/Create
        [Authorize(Roles = "Admin,Accounting Manager")]
        public IActionResult Create()
        {
            int count = _context.DepositAccounts.Count();
            count++;
            string countFormatted = count.ToString("0000");
            DepositAccount depositAccount = new DepositAccount();
            depositAccount.DepositCode = DateTime.Now.Date.ToString("ddMMyyyy") +"-"+ countFormatted;
            depositAccount.DepositDate = DateTime.Today.Date;
            return View(depositAccount);
        }

        // POST: DepositAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Accounting Manager")]
        public async Task<IActionResult> Create( DepositAccount depositAccount)
        {
            if (ModelState.IsValid)
            {
                depositAccount.Balance = depositAccount.DepositAmount;
                _context.Add(depositAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(depositAccount);
        }

        // GET: DepositAccounts/Edit/5
        [Authorize(Roles = "Admin,Accounting Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DepositAndTransferViewModel depositAndTransferViewModel = new DepositAndTransferViewModel();
            DepositAccount depositAccount = await _context.DepositAccounts.FindAsync(id);

            depositAndTransferViewModel.DepositAccountId = depositAccount.DepositAccountId;
            depositAndTransferViewModel.Donor = depositAccount.Donor;
            depositAndTransferViewModel.DepositCode = depositAccount.DepositCode;
            depositAndTransferViewModel.DepositDate = depositAccount.DepositDate;
            depositAndTransferViewModel.DepositType = depositAccount.DepositType;
            depositAndTransferViewModel.DepositAmount = depositAccount.DepositAmount;
            depositAndTransferViewModel.Balance = depositAccount.Balance;
            //depositAndTransferViewModel.Balance = depositAccount.DepositAmount - _context.TransferAccounts.Where(c=>c.DepositAccountId == depositAccount.DepositAccountId).Sum(s=>s.TransferAmount);
            depositAndTransferViewModel.TransferAccounts = 
                _context.TransferAccounts.Where(d => d.DepositAccountId == depositAccount.DepositAccountId).ToList();

            if (depositAccount == null)
            {
                return NotFound();
            }
            return View(depositAndTransferViewModel);
        }

        // POST: DepositAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Accounting Manager")]
        public async Task<IActionResult> Edit(int id,DepositAccount depositAccount)
        {
            if (id != depositAccount.DepositAccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var depositAmnt = _context.DepositAccounts.Find(id).DepositAmount;
                    if (depositAccount.DepositAmount != depositAccount.Balance)
                    {
                        depositAccount.Balance = depositAccount.DepositAmount;
                    }
                    _context.Update(depositAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepositAccountExists(depositAccount.DepositAccountId))
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
            return View(depositAccount);
        }

        // GET: DepositAccounts/Delete/5
        [Authorize(Roles = "Admin,Accounting Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depositAccount = await _context.DepositAccounts
                .FirstOrDefaultAsync(m => m.DepositAccountId == id);
            if (depositAccount == null)
            {
                return NotFound();
            }

            return View(depositAccount);
        }

        // POST: DepositAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Accounting Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depositAccount = await _context.DepositAccounts.FindAsync(id);
            _context.DepositAccounts.Remove(depositAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Accounting Manager")]
        private bool DepositAccountExists(int id)
        {
            return _context.DepositAccounts.Any(e => e.DepositAccountId == id);
        }

        //transfer account
        // GET: TransferAccounts/CreateOrEdit
        [Authorize(Roles = "Admin,Accounting Manager")]
        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            if (id > 0)
            {
                var transferAccount = await _context.TransferAccounts.FindAsync(id);
                if (transferAccount == null)
                {
                    return NotFound();
                }
                ViewData["DepositAccountId"] = new SelectList(_context.DepositAccounts, "DepositAccountId", "DepositCode", transferAccount.DepositAccountId);
                return View(transferAccount);
            }
            else
            {
                ViewData["DepositAccountId"] = new SelectList(_context.DepositAccounts, "DepositAccountId", "DepositCode");
                return View();
            }

        }


        [HttpPost]
        [Authorize(Roles = "Admin,Accounting Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(TransferAccount transferAccount)
        {
            if (ModelState.IsValid)
            {
                if (transferAccount.TransferAccountId > 0)
                {
                    try
                    {
                        _context.Update(transferAccount);

                        //await _context.SaveChangesAsync();
                        //double oldAmount = _context.TransferAccounts.Find(transferAccount.TransferAccountId).TransferAmount;
                        //double differnceAmount = transferAccount.TransferAmount - oldAmount;
                        //DepositAccount dAcount = _context.DepositAccounts.Find(transferAccount.DepositAccountId);
                        //dAcount.Balance -= differnceAmount;

                        //_context.Update(dAcount);

                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                            throw;
                    }
                    
                }
                else
                {
                    DepositAccount depositAccount1 = _context.DepositAccounts.Find(transferAccount.DepositAccountId);
                    depositAccount1.Balance -= transferAccount.TransferAmount;
                    _context.Update(depositAccount1);
                    _context.Add(transferAccount);
                    await _context.SaveChangesAsync();
                    ViewData["DepositAccountId"] = new SelectList(_context.DepositAccounts, "DepositAccountId", "DepositCode", transferAccount.DepositAccountId);
                }
                DepositAndTransferViewModel depositAndTransferViewModel = new DepositAndTransferViewModel();
                DepositAccount depositAccount = await _context.DepositAccounts.FindAsync(transferAccount.DepositAccountId);

                depositAndTransferViewModel.DepositAccountId = depositAccount.DepositAccountId;
                depositAndTransferViewModel.Donor = depositAccount.Donor;
                depositAndTransferViewModel.DepositCode = depositAccount.DepositCode;
                depositAndTransferViewModel.DepositDate = depositAccount.DepositDate;
                depositAndTransferViewModel.DepositType = depositAccount.DepositType;
                depositAndTransferViewModel.DepositAmount = depositAccount.DepositAmount;
                depositAndTransferViewModel.Balance = depositAccount.Balance;
                depositAndTransferViewModel.TransferAccounts =
                    _context.TransferAccounts.Where(d => d.DepositAccountId == depositAccount.DepositAccountId).ToList();

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_EditDeposit", depositAndTransferViewModel) });

            }
            ViewData["DepositAccountId"] = new SelectList(_context.DepositAccounts, "DepositAccountId", "DepositCode", transferAccount.DepositAccountId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEdit", transferAccount) });
        }

        //Delete Transfer
        //[HttpPost, ActionName("DeleteTransfer")]
        //[Authorize(Roles = "Admin,Accounting Manager")]
        ////[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int transferAccountId, int depositAccountId)
        //{
        //    var transferModel = await _context.TransferAccounts.FindAsync(transferAccountId);
        //    _context.TransferAccounts.Remove(transferModel);
        //    await _context.SaveChangesAsync();
        //    DepositAndTransferViewModel depositAndTransferViewModel = new DepositAndTransferViewModel();
        //    DepositAccount depositAccount = await _context.DepositAccounts.FindAsync(depositAccountId);

        //    depositAndTransferViewModel.DepositAccountId = depositAccount.DepositAccountId;
        //    depositAndTransferViewModel.Donor = depositAccount.Donor;
        //    depositAndTransferViewModel.DepositCode = depositAccount.DepositCode;
        //    depositAndTransferViewModel.DepositDate = depositAccount.DepositDate;
        //    depositAndTransferViewModel.DepositType = depositAccount.DepositType;
        //    depositAndTransferViewModel.DepositAmount = depositAccount.DepositAmount;
        //    depositAndTransferViewModel.Balance = depositAccount.Balance;
        //    depositAndTransferViewModel.TransferAccounts =
        //        _context.TransferAccounts.Where(d => d.DepositAccountId == depositAccount.DepositAccountId).ToList();

        //    return Json(new { html = Helper.RenderRazorViewToString(this, "_EditDeposit", depositAndTransferViewModel) });
        //}

        [Authorize(Roles = "Admin,Accounting Manager,Management")]
        [HttpGet]
        public async Task<IActionResult> ShowAllTransfer(int id = 0)
        {
            if (id > 0)
            {
                var transferAccount = await  _context.TransferAccounts.Where(e=>e.DepositAccountId==id).ToListAsync();
                if (transferAccount == null)
                {
                    return NotFound();
                }
                ViewData["DepositAccountId"] = new SelectList(_context.DepositAccounts, "DepositAccountId", "DepositCode");
                return View(transferAccount);
            }
            else
            {
                ViewData["DepositAccountId"] = new SelectList(_context.DepositAccounts, "DepositAccountId", "DepositCode");
                return View();
            }

        }

    }
}
