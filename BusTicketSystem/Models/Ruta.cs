using System.ComponentModel.DataAnnotations;

namespace BusTicketSystem.Models;

public class Ruta
{
    public int IdRuta { get; set; }

    public string? Origen { get; set; }
    public string? Destino { get; set; }

    [Required]
    public TimeSpan DuracionEstimada { get; set; }

    [Range(1, 1000)]
    public decimal PrecioBase { get; set; }

    public bool Estado { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione terminal origen")]
    public int IdTerminalOrigen { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione terminal destino")]
    public int IdTerminalDestino { get; set; }
}