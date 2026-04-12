using Microsoft.AspNetCore.Mvc;
using BusTicketSystem.Repositories;

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
    }
}