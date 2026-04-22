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

        public List<Bus> ListarBuses(int pagina, int filasPorPagina)
        {
            List<Bus> lista = new List<Bus>();

            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListarBuses", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Pagina", pagina);
                    cmd.Parameters.AddWithValue("@FilasPorPagina", filasPorPagina);

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

        public int ContarBuses()
        {
            int total = 0;

            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ContarBuses", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    object resultado = cmd.ExecuteScalar()!;

                    if (resultado != null)
                    {
                        total = Convert.ToInt32(resultado);
                    }
                }
            }

            return total;
        }


        public List<Bus> ListarBusesCombo()
        {
            List<Bus> lista = new List<Bus>();

            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListarBusesCombo", conexion))
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



        // Metodo para crear un Bus //

        public void InsertarBus(Bus bus)
        {
            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertarBus", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Placa", bus.Placa);
                    cmd.Parameters.AddWithValue("@Modelo", bus.Modelo);
                    cmd.Parameters.AddWithValue("@Capacidad", bus.Capacidad);
                    cmd.Parameters.AddWithValue("@Pisos", bus.Pisos);
                    cmd.Parameters.AddWithValue("@Estado", bus.Estado);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Metodo para obtener bus por id
        public Bus? ObtenerBusPorId(int id)
        {
            Bus? bus = null;

            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerBusPorId", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdBus", id);

                    conexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bus = new Bus
                            {
                                IdBus = Convert.ToInt32(reader["IdBus"]),
                                Placa = reader["Placa"].ToString() ?? string.Empty,
                                Modelo = reader["Modelo"].ToString() ?? string.Empty,
                                Capacidad = Convert.ToInt32(reader["Capacidad"]),
                                Pisos = Convert.ToInt32(reader["Pisos"]),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                        }
                    }
                }
            }

            return bus;
        }

        //Metodo para actualizar bus
        public void ActualizarBus(Bus bus)
        {
            using (SqlConnection conexion = _conexionBD.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarBus", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdBus", bus.IdBus);
                    cmd.Parameters.AddWithValue("@Placa", bus.Placa);
                    cmd.Parameters.AddWithValue("@Modelo", bus.Modelo);
                    cmd.Parameters.AddWithValue("@Capacidad", bus.Capacidad);
                    cmd.Parameters.AddWithValue("@Pisos", bus.Pisos);
                    cmd.Parameters.AddWithValue("@Estado", bus.Estado);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}