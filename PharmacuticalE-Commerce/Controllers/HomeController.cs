using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacuticalE_Commerce.Models;
using System.Diagnostics;

namespace PharmacuticalE_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Ecommerce()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Moderator,HR")]
        public IActionResult StaffManagement()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
		}
		public IActionResult FAQ()
		{
			return View();
		}
	}
}
