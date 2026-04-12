using BusTicketSystem.Data;
using BusTicketSystem.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusTicketSystem.Repositories
{
    public class BusRepository
    {
        private readonly ConexionBD _conexionBD;

        public BusRepository(ConexionBD conexionBD)
        {
            _conexionBD = conexionBD;
        }

        public List<Bus> ListarBuses()
        {
            List<Bus> lista = new List<Bus>();

            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListarBuses", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bus bus = new Bus
                            {
                                IdBus = Convert.ToInt32(reader["IdBus"]),
                                Placa = reader["Placa"].ToString() ?? string.Empty,
                                Modelo = reader["Modelo"].ToString() ?? string.Empty,
                                Capacidad = Convert.ToInt32(reader["Capacidad"]),
                                Pisos = Convert.ToInt32(reader["Pisos"]),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };

                            lista.Add(bus);
                        }
                    }
                }
            }

            return lista;
        }
    }
}