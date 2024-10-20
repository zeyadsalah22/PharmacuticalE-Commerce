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

		public async Task<IActionResult> Index()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public async Task<IActionResult> Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[Authorize(Roles = "Admin,Moderator,HR")]
		public async Task<IActionResult> StaffManagement()
		{
			return View();
		}

		public async Task<IActionResult> About()
		{
			return View();
		}

		public async Task<IActionResult> FAQ()
		{
			return View();
		}
	}
}
