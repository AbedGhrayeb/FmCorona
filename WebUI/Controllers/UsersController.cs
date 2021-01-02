using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using WebUI.Helpers;

namespace WebUI.Controllers
{
    [Authorize(Roles="admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;

        public UsersController(UserManager<AppUser> userManager,DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        // GET: UsersController

        public async Task<IActionResult> Index(int? pageIndex)
        {
            var queryable = _userManager.Users;

            int pageSize = 10;
            var vm = await PaginatedList<AppUser>.CreateAsync(queryable, pageIndex ?? 1, pageSize);
            return View(vm);

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
                var deleteUser = await _context.Users.Include(x=>x.ExternalLogins)
                    .Include(x=>x.Records)
                .SingleOrDefaultAsync(x=>x.Id==userId);
                _context.Users.Remove(deleteUser);
                var result=await _context.SaveChangesAsync()>0;
                if (result)
                {
                    TempData["UserFeed"] = $"Succeeded Delete User: {user.Email}";
                    return RedirectToAction(nameof(Index), _userManager.Users);
                }

                return RedirectToAction(nameof(Index),_userManager.Users);
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
                    var deleteUser = await _context.Users.Include(x => x.ExternalLogins)
                   .Include(x => x.Records)
                    .SingleOrDefaultAsync(x => x.Id == user.Id);
                    _context.Users.Remove(deleteUser);
                }
                var result = await _context.SaveChangesAsync() > 0;
                if (result)
                {
                    TempData["UserFeed"] = "Succeeded Delete All Users";
                    return RedirectToAction(nameof(Index), _userManager.Users);
                }
                throw new Exception("Proplem Saving Changes");
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
