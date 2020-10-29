using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using manahil.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;

namespace manahil.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministrationController : Controller
    {
        public RoleManager<IdentityRole> RoleManager { get; }
        public UserManager<IdentityUser> UserManager { get; }
        public ILogger<AdministrationController> Logger { get; }

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                        UserManager<IdentityUser> userManager, ILogger<AdministrationController> logger)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            Logger = logger;
        }
        public IActionResult Index()
        {
            var roleList = RoleManager.Roles;
            return View(roleList);
        }

        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModels model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await RoleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role Not Found, Id={id}";
                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = await RoleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View("Index");
                    }
                }
                catch (DbUpdateException ex)
                {
                    Logger.LogError($"Error deleting role {ex}");
                    ViewBag.ErrorTitle = $"{role.Name} Role in use.";
                    ViewBag.ErrorMessage = $"{role.Name} cannot be deleted there are users in this role. If" +
                        $"you want to delete this role. Please remove the users from this role.";
                    return View("Error");
                }
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {id} not found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = id,
                RoleName = role.Name,
            };

            foreach (var user in UserManager.Users)
            {
                if(await UserManager.IsInRoleAsync(user, model.RoleName))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {model.Id} not found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await RoleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            
            
        }
        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string id)
        {
            ViewBag.roleId = id;
            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {id} not found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();
            foreach(var user in UserManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                if (await UserManager.IsInRoleAsync(user,role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;
            var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User, id = {userId} not found";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();
            foreach (var role in RoleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await UserManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }
                model.Add(userRolesViewModel);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User, id = {userId} not found";
                return View("NotFound");
            }

            var roles = await UserManager.GetRolesAsync(user);
            var result = await UserManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("","Cannot Remover User Add Existing Roles");
                return View(model);
            }

            result = await UserManager.AddToRolesAsync(user, model.Where(x=>x.IsSelected).Select(r=>r.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot Selected Roles to User");
                return View(model);
            }

            return RedirectToAction("EditUser", new {Id=userId });

        }


        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string id)
        {
            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {id} not found";
                return View("NotFound");
            }
            else
            {
                for (int i = 0; i < model.Count; i++)
                {
                    var user = await UserManager.FindByIdAsync(model[i].UserId);

                    IdentityResult result=null;
                    if (model[i].IsSelected && !(await UserManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await UserManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!(model[i].IsSelected) && await UserManager.IsInRoleAsync(user, role.Name))
                    {
                        result = await UserManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else
                    {
                        continue;
                    }

                    if (i < (model.Count - 1))
                        continue;
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = id });
                    }

                }

                return RedirectToAction("EditRole",new {Id=id });
            }
                
        }

        [HttpGet]
        public IActionResult ListUser()
        {
            var users = UserManager.Users;
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User Not Found, Id={id}";
                return View("NotFound");
            }
            else
            {
                var result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUser");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                    return View("ListUser");
                }
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user==null)
            {
                ViewBag.ErrorMessage = $"User Not Found, Id={id}";
                return View("NotFound");
            }
            else
            {
                var userRoles =await UserManager.GetRolesAsync(user);
                var userClaims = await UserManager.GetClaimsAsync(user);
                var model = new EditUserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = userRoles,
                    Claims = userClaims.Select(c => c.Value).ToList()
                };

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await UserManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User Not Found, Id={model.Id}";
                return View("NotFound");
            }
            else
            {
                user.UserName = model.UserName;
                user.Email = model.Email;

                var result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUser");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }

                return View(model);
            }
        }
    }
}

