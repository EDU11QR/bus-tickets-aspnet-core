using System.ComponentModel.DataAnnotations;

namespace BusTicketSystem.Models
{
    public class Horario
    {
        public int IdHorario { get; set; }

        [Required(ErrorMessage = "Seleccione una ruta")]
        public int IdRuta { get; set; }

        [Required(ErrorMessage = "Seleccione un bus")]
        public int IdBus { get; set; }

        [Required(ErrorMessage = "Ingrese la fecha")]
        [DataType(DataType.Date)]
        public DateTime FechaSalida { get; set; }

        [Required(ErrorMessage = "Ingrese hora de salida")]
        public TimeSpan HoraSalida { get; set; }

        [Required(ErrorMessage = "Ingrese hora de llegada")]
        public TimeSpan HoraLlegada { get; set; }

        [Range(1, 1000, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        public bool Estado { get; set; }

        public string? Origen { get; set; }
        public string? Destino { get; set; }
        public string? Placa { get; set; }
    }
}