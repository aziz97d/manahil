using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using manahil.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;


namespace manahil.Controllers
{
    //[OverridableAuthorize]
    public class DepartmentController : Controller
    {
        private readonly DatabaseContext db;

        public DepartmentController(DatabaseContext context)
        {
            
            db = context;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {

            return View(await db.Cat_Department.ToListAsync());
        }


        // GET: Department/Create
        public IActionResult Create()
        {
            ViewBag.Title = "Create";
            //ViewBag.DeptId = new SelectList(db.Cat_Department, "DeptId", "DeptName");
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("DeptId,ComId,DeptCode,DeptName,Pcname,LuserId,DeptBangla,Slno,ParentId,Buid,Buname,DptSrtName,IsStrDpt")]*/ Cat_Department department)
        {
            if (ModelState.IsValid)
            {
                //department.UserId = HttpContext.Session.GetString("userid");
                //department.ComId = HttpContext.Session.GetString("comid");
                if (department.DeptId > 0)
                {
                    db.Entry(department).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    TempData["Message"] = "Data Update Successfully";
                    TempData["Status"] = "2";
                    

                }
                else
                {
                    db.Add(department);
                    await db.SaveChangesAsync();

                    TempData["Message"] = "Data Save Successfully";
                    TempData["Status"] = "1";
                    

                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            var department = await db.Cat_Department.FindAsync(id);
            //ViewBag.DeptId = new SelectList(db.Cat_Department, "DeptId", "DeptName", department.ParentId);
            if (department == null)
            {
                return NotFound();
            }
            return View("Create", department);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await db.Cat_Department
                .FirstOrDefaultAsync(m => m.DeptId == id);

            if (department == null)
            {
                return NotFound();
            }

            ViewBag.Title = "Delete";
            //ViewBag.DeptId = new SelectList(db.Cat_Department, "DeptId", "DeptName", department.ParentId);
            return View("Create", department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var department = await db.Cat_Department.FindAsync(id);
                db.Cat_Department.Remove(department);
                db.SaveChanges();

                TempData["Message"] = "Data Delete Successfully";
                //TempData["Status"] = "3";
                

                //TempData["Message"] = "Data Delete Successfully";
                return Json(new { Success = 1, DeptId = department.DeptId, ex = TempData["Message"].ToString() });
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Success = 0, ex = ex.InnerException.InnerException.Message.ToString() });
            }
        }


        //public IActionResult IsExist(string DeptName,int DeptId)
        //{
        //    if (ViewBag.Title == "Create")
        //        return Json(!db.Cat_Department.Any(d => d.DeptName == DeptName));
        //    else
        //        return Json(!db.Cat_Department.Any(d => d.DeptName == DeptName));
        //}


        private bool DepartmentExists(int id)
        {
            return db.Cat_Department.Any(e => e.DeptId == id);
        }
    }
}