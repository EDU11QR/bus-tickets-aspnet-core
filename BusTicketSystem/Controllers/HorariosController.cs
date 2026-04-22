using BusTicketSystem.Models;
using BusTicketSystem.Data;
using BusTicketSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

public class HorariosController : Controller
{
    private readonly HorarioRepository repo;
    private readonly RutaRepository repoRuta;
    private readonly BusRepository repoBus;
    private readonly ConexionBD _conexionBD;

    public HorariosController(IConfiguration config)
    {
        _conexionBD = new ConexionBD(config);

        
        string cn = config.GetConnectionString("cn");

        repo = new HorarioRepository(cn);
        repoRuta = new RutaRepository(_conexionBD);
        repoBus = new BusRepository(_conexionBD);
    }

    public IActionResult Index(int pagina = 1, bool verEliminados = false, DateTime? fecha = null)
    {
        int cantidad = 8;

        var lista = repo.ListarPaginado(pagina, cantidad, verEliminados, fecha);
        int total = repo.TotalRegistros(verEliminados, fecha);

        ViewBag.PaginaActual = pagina;
        ViewBag.TotalPaginas = (int)Math.Ceiling((double)total / cantidad);
        ViewBag.VerEliminados = verEliminados;
        ViewBag.Fecha = fecha;

        return View(lista);
    }

    [HttpPost]
    public IActionResult Restaurar(int id)
    {
        repo.Restaurar(id);
        return Ok();
    }
    public IActionResult Crear()
    {
        ViewBag.Rutas = repoRuta.ListarRutas();
        ViewBag.Buses = repoBus.ListarBusesCombo();

        return View();
    }

    [HttpPost]
    public IActionResult Crear(Horario h)
    {
        

        if (h.FechaSalida < DateTime.Today)
        {
            ModelState.AddModelError("", "No puedes registrar fechas pasadas");
        }

        if (h.HoraSalida == TimeSpan.Zero)
        {
            ModelState.AddModelError("", "Ingrese una hora válida");
        }

        // 🔹 OBTENER RUTA
        var ruta = repoRuta.ObtenerPorId(h.IdRuta);

        if (ruta == null)
        {
            ModelState.AddModelError("", "Ruta no válida");
        }
        else
        {
            try
            {
                TimeSpan duracion = ruta.DuracionEstimada;
                DateTime salida = h.FechaSalida.Add(h.HoraSalida);
                DateTime llegada = salida.Add(duracion);

                h.HoraLlegada = llegada.TimeOfDay;
            }
            catch
            {
                ModelState.AddModelError("", "Formato de duración inválido");
            }
        }

        
        if (!ModelState.IsValid)
        {
            ViewBag.Rutas = repoRuta.ListarRutas();
            ViewBag.Buses = repoBus.ListarBusesCombo();
            return View(h);
        }

        
        repo.Registrar(h);
        return RedirectToAction("Index");
    }
    public IActionResult Editar(int id)
    {
        var h = repo.ObtenerPorId(id);

        ViewBag.Rutas = repoRuta.ListarRutas();
        ViewBag.Buses = repoBus.ListarBusesCombo();

        return View(h);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Horario h)
    {
        if (h.HoraSalida == TimeSpan.Zero)
        {
            ModelState.AddModelError("", "Ingrese una hora válida");
        }

        if (h.FechaSalida < DateTime.Today)
        {
            ModelState.AddModelError("", "No puedes registrar fechas pasadas");
        }

        var ruta = repoRuta.ObtenerPorId(h.IdRuta);

        if (ruta == null)
        {
            ModelState.AddModelError("", "Ruta no válida");
        }
        else
        {
            TimeSpan duracion = ruta.DuracionEstimada;

            DateTime salida = h.FechaSalida.Add(h.HoraSalida);
            DateTime llegada = salida.Add(duracion);

            h.HoraLlegada = llegada.TimeOfDay;
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Rutas = repoRuta.ListarRutas();
            ViewBag.Buses = repoBus.ListarBusesCombo();
            return View(h);
        }

        var original = repo.ObtenerPorId(h.IdHorario);

        if (original == null)
        {
            return NotFound();
        }

        h.Estado = original.Estado;

        repo.Actualizar(h);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        repo.Eliminar(id);
        return Ok(); 
    }
}