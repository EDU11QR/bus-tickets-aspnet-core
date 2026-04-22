using BusTicketSystem.Models;

namespace BusTicketSystem.ViewModels
{
    public class BusPaginadoViewModel
    {
        public List<Bus> Buses { get; set; } = new List<Bus>();

        public int PaginaActual { get; set; }

        public int TotalPaginas { get; set; }

        public int FilasPorPagina { get; set; }

        public int TotalRegistros { get; set; }
    }
}