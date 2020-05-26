using DentAppointment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DentAppointment.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UserRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserRegistration(UserRegistrationViewModel userRegistrationViewModel, string register, string cancel)
        {
            return View();
        }


    }
}
