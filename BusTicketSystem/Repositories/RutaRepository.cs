using BusTicketSystem.Data;
using BusTicketSystem.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusTicketSystem.Repositories
{
    public class RutaRepository
    {
        private readonly ConexionBD _conexionBD;

        public RutaRepository(ConexionBD conexionBD)
        {
            _conexionBD = conexionBD;
        }

        
        public List<Ruta> ListarRutas()
        {
            List<Ruta> lista = new List<Ruta>();

            using (SqlConnection cn = _conexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarRutas", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Ruta
                    {
                        IdRuta = (int)dr["IdRuta"],
                        Origen = dr["Origen"].ToString(),
                        Destino = dr["Destino"].ToString(),
                        DuracionEstimada = (TimeSpan)dr["DuracionEstimada"],
                        PrecioBase = (decimal)dr["PrecioBase"],
                        Estado = (bool)dr["Estado"]
                    });
                }
            }

            return lista;
        }


        public void Insertar(Ruta r)
        {
            using (SqlConnection cn = _conexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarRuta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Origen", r.Origen);
                cmd.Parameters.AddWithValue("@Destino", r.Destino);
                cmd.Parameters.AddWithValue("@DuracionEstimada", r.DuracionEstimada);
                cmd.Parameters.AddWithValue("@PrecioBase", r.PrecioBase);
                cmd.Parameters.AddWithValue("@Estado", true);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public Ruta? ObtenerPorId(int id)
        {
            Ruta? r = null;

            using (SqlConnection cn = _conexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerRutaPorId", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdRuta", id);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    r = new Ruta
                    {
                        IdRuta = (int)dr["IdRuta"],
                        Origen = dr["Origen"].ToString(),
                        Destino = dr["Destino"].ToString(),
                        DuracionEstimada = (TimeSpan)dr["DuracionEstimada"],
                        PrecioBase = (decimal)dr["PrecioBase"],
                        Estado = (bool)dr["Estado"]
                    };
                }
            }

            return r;
        }


        public void Actualizar(Ruta r)
        {
            using (SqlConnection cn = _conexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarRuta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdRuta", r.IdRuta);
                cmd.Parameters.AddWithValue("@Origen", r.Origen);
                cmd.Parameters.AddWithValue("@Destino", r.Destino);
                cmd.Parameters.AddWithValue("@DuracionEstimada", r.DuracionEstimada);
                cmd.Parameters.AddWithValue("@PrecioBase", r.PrecioBase);
                cmd.Parameters.AddWithValue("@Estado", r.Estado);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void Eliminar(int id)
        {
            using (SqlConnection cn = _conexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarRuta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdRuta", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Terminal ObtenerTerminalPorId(int id)
        {
            Terminal t = null;

            using (SqlConnection cn = _conexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT IdTerminal, Nombre FROM Terminales WHERE IdTerminal = @id", cn);
                cmd.Parameters.AddWithValue("@id", id);

                cn.Open();
                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    t = new Terminal
                    {
                        IdTerminal = (int)dr["IdTerminal"],
                        Nombre = dr["Nombre"].ToString()
                    };
                }
            }

            return t;
        }
    }

   
}