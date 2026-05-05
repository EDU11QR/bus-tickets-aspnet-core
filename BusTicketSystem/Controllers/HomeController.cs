using BusTicketSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BusTicketSystem.Controllers
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
            // 🔥 VALIDAR LOGIN
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            // 🔥 DATOS DASHBOARD
            ViewBag.TotalRutas = 10;
            ViewBag.TotalBuses = 5;
            ViewBag.TotalHorarios = 20;

            return View();
        }

        public IActionResult Privacy()
        {
            // opcional proteger también
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}