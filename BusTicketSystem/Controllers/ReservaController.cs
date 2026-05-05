using BusTicketSystem.Repositories;
using BusTicketSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketSystem.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ReservaRepository repo;

        public ReservasController(IConfiguration config)
        {
            string cn = config.GetConnectionString("cn");
            repo = new ReservaRepository(cn);
        }

        // 🔥 LISTAR RESERVAS (LO QUE TE FALTABA)
        public IActionResult Index()
        {
            var lista = repo.ListarReservas();
            return View(lista);
        }

        // 🔥 GET
        public IActionResult Crear(int idHorario)
        {
            var asientos = repo.ObtenerAsientosPorHorario(idHorario);

            ViewBag.IdHorario = idHorario;
            ViewBag.Asientos = asientos;

            return View();
        }

        // 🔥 POST
        [HttpPost]
        public IActionResult Crear(int idHorario, List<int> asientosSeleccionados)
        {
            if (asientosSeleccionados == null || !asientosSeleccionados.Any())
            {
                ModelState.AddModelError("", "Debe seleccionar al menos un asiento");

                ViewBag.Asientos = repo.ObtenerAsientosPorHorario(idHorario);
                ViewBag.IdHorario = idHorario;

                return View();
            }

            string asientos = string.Join(",", asientosSeleccionados);

            int idCliente = HttpContext.Session.GetInt32("IdCliente") ?? 0;

            if (idCliente == 0)
            {
                return RedirectToAction("Login", "Auth");
            }
            decimal total = asientosSeleccionados.Count * 50;

            try
            {
                repo.RegistrarReserva(idCliente, idHorario, asientos, total);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                ViewBag.Asientos = repo.ObtenerAsientosPorHorario(idHorario);
                ViewBag.IdHorario = idHorario;

                return View();
            }

            // 🔥 AQUÍ CAMBIAMOS (ANTES IBAS A HORARIOS)
            return RedirectToAction("Index");
        }
    }
}