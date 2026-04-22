using BusTicketSystem.Models;
using BusTicketSystem.Data;
using BusTicketSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

public class RutasController : Controller
{
    private readonly RutaRepository repo;
    private readonly UbicacionRepository repoUbicacion;
    public RutasController(IConfiguration config)
    {
        var conexion = new ConexionBD(config);

        repo = new RutaRepository(conexion);

       
        repoUbicacion = new UbicacionRepository(conexion);
    }


    public IActionResult Index()
    {
        var lista = repo.ListarRutas();
        return View(lista);
    }


    public IActionResult Crear()
    {
        ViewBag.Departamentos = repoUbicacion.ListarDepartamentos();
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Ruta r)
    {
        if (r.IdTerminalOrigen == 0 || r.IdTerminalDestino == 0)
        {
            ModelState.AddModelError("", "Seleccione terminales");
        }

        if (r.IdTerminalOrigen == r.IdTerminalDestino)
        {
            ModelState.AddModelError("", "Origen y destino no pueden ser iguales");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Departamentos = repoUbicacion.ListarDepartamentos();
            return View(r);
        }

        
        var origen = repoUbicacion.ObtenerTerminalPorId(r.IdTerminalOrigen);
        var destino = repoUbicacion.ObtenerTerminalPorId(r.IdTerminalDestino);

        r.Origen = origen.Nombre;
        r.Destino = destino.Nombre;

        repo.Insertar(r);

        return RedirectToAction("Index");
    }

    public IActionResult Editar(int id)
    {
        var r = repo.ObtenerPorId(id);
        return View(r);
    }


    [HttpPost]
    public IActionResult Editar(Ruta r)
    {
        if (r.IdTerminalOrigen == r.IdTerminalDestino)
        {
            ModelState.AddModelError("", "Origen y destino no pueden ser iguales");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Departamentos = repoUbicacion.ListarDepartamentos();
            return View(r);
        }

        repo.Actualizar(r);
        return RedirectToAction("Index");
    }


    public IActionResult Eliminar(int id)
    {
        repo.Eliminar(id);
        return RedirectToAction("Index");
    }
}