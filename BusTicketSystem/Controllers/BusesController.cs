using Microsoft.AspNetCore.Mvc;
using BusTicketSystem.Repositories;
using BusTicketSystem.Models;
using BusTicketSystem.ViewModels;

namespace BusTicketSystem.Controllers
{
    public class BusesController : Controller
    {
        private readonly BusRepository _busRepository;

        public BusesController(BusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        public IActionResult Index(int pagina = 1)
        {
            int filasPorPagina = 10;
            int totalRegistros = _busRepository.ContarBuses();
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / filasPorPagina);

            var buses = _busRepository.ListarBuses(pagina, filasPorPagina);

            BusPaginadoViewModel vm = new BusPaginadoViewModel
            {
                Buses = buses,
                PaginaActual = pagina,
                TotalPaginas = totalPaginas,
                FilasPorPagina = filasPorPagina,
                TotalRegistros = totalRegistros
            };

            return View(vm);
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