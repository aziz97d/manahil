using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using manahil.Models;
using manahil.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace manahil.Controllers
{
    public class AccountController : Controller
    {
        public readonly DatabaseContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(DatabaseContext context,UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            db = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public DatabaseContext Context { get; }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModels model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.UserCategory == "Employee")
                    {
                        Employee employee = new Employee
                        {
                            Name = model.Name,
                            Email = model.Email
                        };
                        await db.Employees.AddAsync(employee);
                        await db.SaveChangesAsync();
                    }
                    else if (model.UserCategory == "Donor")
                    {
                        Donor donor = new Donor
                        {
                            Name = model.Name,
                            Email = model.Email
                        };
                        await db.Donors.AddAsync(donor);
                        await db.SaveChangesAsync();
                        
                    }
                    else if (model.UserCategory == "Contractor")
                    {
                        Contractor contractor = new Contractor
                        {
                            Name = model.Name,
                            Email = model.Email
                        };
                        await db.Contractors.AddAsync(contractor);
                        await db.SaveChangesAsync();
                    }
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUser","Administration");
                    }
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
  
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (Url.IsLocalUrl(ViewBag.ReturnUrl))
                    return Redirect(ViewBag.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModels model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    CookieOptions option1 = new CookieOptions();

                    int userIdForCookie=0;
                    string userEmailForCookie = model.Email;

                    if (db.Employees.Any(e => e.Email == model.Email))
                    {
                        userIdForCookie = db.Employees.Where(e => e.Email == model.Email)
                            .Select(e => e.EmployeeId).SingleOrDefault();
                    }
                    else if (db.Donors.Any(e => e.Email == model.Email))
                    {
                        userIdForCookie = db.Donors.Where(e => e.Email == model.Email)
                            .Select(e => e.DonorId).SingleOrDefault();
                    }
                    else if (db.Contractors.Any(e => e.Email == model.Email))
                    {
                        userIdForCookie = db.Contractors.Where(e => e.Email == model.Email)
                            .Select(e => e.ContractorId).SingleOrDefault();
                    }

                    if (model.RememberMe)
                    {   
                        option1.Expires = DateTime.Now.AddDays(7);
                    }
                    else
                    {
                        option1.Expires = DateTime.Now.AddDays(1);
                    }
                    
                    
                    //option1.Expires = TimeSpan.FromHours(12);

                    Response.Cookies.Append("UserId", userIdForCookie.ToString(), option1);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(ViewBag.ReturnUrl))
                    {

                            return Redirect(ViewBag.ReturnUrl);

                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                    
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            string userId = Request.Cookies["UserId"];
            await signInManager.SignOutAsync();
            Response.Cookies.Delete("UserId");
            return RedirectToAction("index","home");
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);
                    ViewBag.PasswordResetLink = passwordLink;
                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if(token==null || email == null)
            {
                ModelState.AddModelError("", "Invalid Attempt");
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View(model);
                }
            }
            
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }


}
