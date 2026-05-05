using Microsoft.Data.SqlClient;
using System.Data;
using BusTicketSystem.Models;
using System.Collections.Generic;

namespace BusTicketSystem.Repositories
{
    public class ReservaRepository
    {
        private readonly string _connectionString;

        // 🔥 CONSTRUCTOR (IMPORTANTE)
        public ReservaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void RegistrarReserva(int idCliente, int idHorario, string asientos, decimal total)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarReserva", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                cmd.Parameters.AddWithValue("@IdHorario", idHorario);
                cmd.Parameters.AddWithValue("@Asientos", asientos);
                cmd.Parameters.AddWithValue("@Total", total);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Asiento> ObtenerAsientosPorHorario(int idHorario)
        {
            List<Asiento> lista = new List<Asiento>();

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AsientosPorHorario", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdHorario", idHorario);

                cn.Open();
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Asiento
                    {
                        IdAsiento = (int)dr["IdAsiento"],
                        NumeroAsiento = dr["NumeroAsiento"].ToString(),
                        Estado = (bool)dr["Estado"]
                    });
                }
            }

            return lista;
        }


        public List<dynamic> ListarReservas()
        {
            List<dynamic> lista = new List<dynamic>();

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ListarReservas", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new
                    {
                        IdReserva = (int)dr["IdReserva"],
                        Cliente = dr["Cliente"].ToString(),
                        Ruta = dr["Ruta"].ToString(),
                        Fecha = Convert.ToDateTime(dr["Fecha"]).ToString("dd/MM/yyyy"),
                        Total = (decimal)dr["Total"],
                        Estado = dr["Estado"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}