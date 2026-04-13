using Microsoft.AspNetCore.Mvc;
using BusTicketSystem.Repositories;
using BusTicketSystem.Models;

namespace BusTicketSystem.Controllers
{
    public class BusesController : Controller
    {
        private readonly BusRepository _busRepository;

        public BusesController(BusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        public IActionResult Index()
        {
            var buses = _busRepository.ListarBuses();
            return View(buses);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Bus bus)
        {
            if (ModelState.IsValid)
            {
                _busRepository.InsertarBus(bus);
                return RedirectToAction("Index");
            }

            return View(bus);
        }

    }
}