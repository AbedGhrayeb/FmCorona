using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Helpers;

namespace WebUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        // GET: UsersController

        public async Task<IActionResult> Index(int? pageIndex)
        {
            var queryable = _userManager.Users;

            int pageSize = 10;
            var vm = await PaginatedList<AppUser>.CreateAsync(queryable, pageIndex ?? 1, pageSize);
            return View(vm);

        }


        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // POST: UsersController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null ||(await _userManager.IsInRoleAsync(user,"admin")))
            {
                return NotFound();
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["UserFeed"] = $"Succeeded Delete User: {user.Email}";
                    return RedirectToAction(nameof(Index), _userManager.Users);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (AppUser user in users)
            {
                if (! await _userManager.IsInRoleAsync(user,"admin"))
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["UserFeed"] = "Succeeded Delete All Users";
                        return RedirectToAction(nameof(Index), _userManager.Users);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
              
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
