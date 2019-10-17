using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocialNetwork.Models;
using SimpleSocialNetwork.ViewModels;

namespace SimpleSocialNetwork.Controllers
{
    public class RolesController : Controller
    {
        UserManager<User> _userManager;
        public RolesController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> SetAdmin(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, "admin");
            await _userManager.AddToRoleAsync(user, "moderator");

            return RedirectToAction("MainPage", "User", new { userId });
        }

        public async Task<IActionResult> SetModerator(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, "moderator");
            return RedirectToAction("MainPage", "User", new { userId });
        }

        public async Task<IActionResult> RemoveModerator(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            await _userManager.RemoveFromRoleAsync(user, "moderator");
            return RedirectToAction("MainPage", "User", new { userId });
        }
    }
}
