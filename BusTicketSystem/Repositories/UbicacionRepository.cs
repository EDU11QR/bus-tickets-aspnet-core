using BusTicketSystem.Data;
using BusTicketSystem.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusTicketSystem.Repositories
{
    public class UbicacionRepository
    {
        private readonly ConexionBD _conexionBD;

        public UbicacionRepository(ConexionBD conexionBD)
        {
            _conexionBD = conexionBD;
        }

        public List<Provincia> ProvinciasPorDepartamento(int id)
        {
            List<Provincia> lista = new();

            using (SqlConnection cn = _conexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ProvinciasPorDepartamento", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdDepartamento", id);

                cn.Open();
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Provincia
                    {
                        IdProvincia = (int)dr["IdProvincia"],
                        Nombre = dr["Nombre"].ToString()
                    });
                }
            }

            return lista;
        }

        public List<Distrito> DistritosPorProvincia(int id)
        {
            List<Distrito> lista = new();

            using (SqlConnection cn = _conexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_DistritosPorProvincia", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProvincia", id);

                cn.Open();
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Distrito
                    {
                        IdDistrito = (int)dr["IdDistrito"],
                        Nombre = dr["Nombre"].ToString()
                    });
                }
            }

            return lista;
        }

        public List<Terminal> TerminalesPorDistrito(int id)
        {
            List<Terminal> lista = new();

            using (SqlConnection cn = _conexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_TerminalesPorDistrito", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdDistrito", id);

                cn.Open();
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Terminal
                    {
                        IdTerminal = (int)dr["IdTerminal"],
                        Nombre = dr["Nombre"].ToString()
                    });
                }
            }

            return lista;
        }
        public List<Departamento> ListarDepartamentos()
        {
            List<Departamento> lista = new();

            using (SqlConnection cn = _conexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarDepartamentos", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Departamento
                    {
                        IdDepartamento = (int)dr["IdDepartamento"],
                        Nombre = dr["Nombre"].ToString()
                    });
                }
            }

            return lista;
        }

        public Terminal ObtenerTerminalPorId(int id)
        {
            Terminal t = null;

            using (SqlConnection cn = _conexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(@"
            SELECT IdTerminal, Nombre 
            FROM Terminales 
            WHERE IdTerminal = @id", cn);

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
