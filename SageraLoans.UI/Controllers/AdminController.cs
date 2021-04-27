using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using SageraLoans.Core;
using SageraLoans.Models;
using SageraLoans.Models.ViewModels;

namespace SageraLoans.UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AdminController(IUserRepository userRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async  Task<IActionResult> AllUsers()
        {
            var listOfUsers = _userRepository.GetAllUsers();
            var listOfUserViewModel = new List<UserForAdminViewModel>();

            foreach (var user in listOfUsers)
            {
                var viewModel = new UserForAdminViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = await _userRepository.GetRole(user)
                };
                listOfUserViewModel.Add(viewModel);

            }

            return View(listOfUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userRepository.DeleteUser(id);

            if (result.Succeeded)
            {
                return RedirectToAction("AllUsers");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("",error.Description);
            }

            return RedirectToAction(nameof(AllUsers));
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string id)
        {
            var user = await _userRepository.GetUser(id);
            var result = await _userRepository.GetRole(user);

            if (result != "Admin")
            {
                var signedInUser = await GetCurrentUserAsync();
                await _userRepository.RemoveRoleOfAdmin(signedInUser);
                var ad = await _userRepository.AddRoleOfAdmin(user);
                if (ad.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    RedirectToAction("Index", "Home");
                }
                foreach (var error in ad.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }

            }

            return RedirectToAction("AllUsers");
        }

        private Task<AppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
