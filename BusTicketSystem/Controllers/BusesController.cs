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

        // Metodo para Editar
        public IActionResult Editar(int id)
        {
            var bus = _busRepository.ObtenerBusPorId(id);

            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        [HttpPost]
        public IActionResult Editar(Bus bus)
        {
            if (ModelState.IsValid)
            {
                _busRepository.ActualizarBus(bus);
                return RedirectToAction("Index");
            }

            return View(bus);
        }

    }
}