using HomeTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeTask.Controllers
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

        public IActionResult Skills()
        {
            return View();
        }
        public IActionResult AboutMe()
        {
            return View();
        }
        public IActionResult ContactMe()
        {
            return View();
        }
        public IActionResult Projects()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
