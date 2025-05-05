using Microsoft.AspNetCore.Mvc;

namespace BiteWiseWeb2.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
