using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BiteWiseWeb2.Models;
using Microsoft.AspNetCore.Identity;
using static BiteWiseWeb2.Models.BiteWiseDbContext;

namespace BiteWiseWeb2.Controllers;

    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
