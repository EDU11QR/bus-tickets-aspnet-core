using BusTicketSystem.Data;
using BusTicketSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketSystem.Controllers
{
    public class UbicacionController : Controller
    {
        private readonly UbicacionRepository repo;

        public UbicacionController(IConfiguration config)
        {
            var conexion = new ConexionBD(config);
            repo = new UbicacionRepository(conexion);
        }

        public JsonResult Provincias(int id)
        {
            return Json(repo.ProvinciasPorDepartamento(id));
        }

        public JsonResult Distritos(int id)
        {
            return Json(repo.DistritosPorProvincia(id));
        }

        public JsonResult Terminales(int id)
        {
            return Json(repo.TerminalesPorDistrito(id));
        }
    }
}
