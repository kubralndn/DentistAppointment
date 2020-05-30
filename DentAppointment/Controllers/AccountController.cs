using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentAppointment.Data.Entity;
using DentAppointment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DentAppointment.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager; //kullanıcı ekleme sifre olustur ekle
        private SignInManager<AppUser> _signInManager;
        private RoleManager<AppRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user =await _userManager.FindByNameAsync(model.UserName);
            if (user==null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(model);
            }

            var result =await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false); //last parameter for lock the system for a while when 2-3 fails 
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Sign-in to session is failed");
            return View(model);
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Login");
        }

        public IActionResult Denied()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            AppUser user = new AppUser()
            {
                UserName = model.UserName,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Color = model.Color,
                IsDentist = model.IsDentist
            };

            IdentityResult result = _userManager.CreateAsync(user, model.Password).Result;

            //if user created succesfully or not
            if (result.Succeeded)
            {
                bool roleCheck = model.IsDentist ? AddRole("Dentist") : AddRole("Secretary");
                if (!roleCheck)
                {
                    return View("Error");
                }
                _userManager.AddToRoleAsync(user, model.IsDentist ? "Dentist" : "Secretary").Wait();
                return RedirectToAction("Index", "Home");

            }
            return View("Error");
        }

        private bool AddRole(string roleName)
        {
            if (!_roleManager.RoleExistsAsync(roleName).Result) //if the role doesn't exist
            {
                AppRole role = new AppRole()
                {
                    Name = roleName

                };
                IdentityResult result = _roleManager.CreateAsync(role).Result;
                return result.Succeeded;
            }
            return true;
        }
    }
}
