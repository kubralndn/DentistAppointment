using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentAppointment.Data.Entity;
using DentAppointment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DentAppointment.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<AppUser> _userManager;
        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            AppUser user = _userManager.Users.SingleOrDefault(x => x.UserName == HttpContext.User.Identity.Name); //take value from cookie and compare it with db value
            if (user == null)
            {
                return View("Error");
            }
            var dentists = _userManager.Users.Where(x => x.IsDentist);
            if (_userManager.IsInRoleAsync(user, "Secretary").Result)
            {
                SecretaryViewModel model = new SecretaryViewModel()
                {
                    User = user,
                    Dentists = dentists,
                    DentistsSelectList = dentists.Select(n => new SelectListItem
                    {
                        Value = n.Id,
                        Text = $"{n.Name} {n.Surname}"
                    }).ToList()

                };
                return View("Secretary",model);
            }
            else
            {
                return View("Dentist");
            }
        }
        public IActionResult Secretary()
        {
            return View();
        }
        public IActionResult Dentist()
        {
            return View();
        }
    }
}
