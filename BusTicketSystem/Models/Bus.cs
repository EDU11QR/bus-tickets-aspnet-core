namespace BusTicketSystem.Models
{
    public class Bus
    {

        public int IdBus { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public int Pisos { get; set; }
        public bool Estado { get; set; }

    }
}
