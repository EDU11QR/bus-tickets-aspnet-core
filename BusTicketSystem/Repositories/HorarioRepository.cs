using BusTicketSystem.Data;
using BusTicketSystem.Models;
using Microsoft.Data.SqlClient;
using System.Data;

public class HorarioRepository
{
    private readonly string _connectionString;

    public HorarioRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    
    public List<Horario> Listar(bool verEliminados)
    {
        var lista = new List<Horario>();

        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_ListarHorarios", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VerEliminados", verEliminados);

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Horario
                {
                    IdHorario = (int)dr["IdHorario"],
                    Origen = dr["Origen"].ToString(),
                    Destino = dr["Destino"].ToString(),
                    Placa = dr["Placa"].ToString(),
                    FechaSalida = (DateTime)dr["FechaSalida"],
                    HoraSalida = (TimeSpan)dr["HoraSalida"],
                    HoraLlegada = (TimeSpan)dr["HoraLlegada"],
                    Precio = (decimal)dr["Precio"],
                    Estado = (bool)dr["Estado"]
                });
            }
        }

        return lista;
    }

    
    public void Registrar(Horario h)
    {
        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_InsertarHorario", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdRuta", h.IdRuta);
            cmd.Parameters.AddWithValue("@IdBus", h.IdBus);
            cmd.Parameters.AddWithValue("@FechaSalida", h.FechaSalida);
            cmd.Parameters.AddWithValue("@HoraSalida", h.HoraSalida);
            cmd.Parameters.AddWithValue("@HoraLlegada", h.HoraLlegada);
            cmd.Parameters.AddWithValue("@Precio", h.Precio);
            cmd.Parameters.AddWithValue("@Estado", true);

            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    
    public Horario? ObtenerPorId(int id)
    {
        Horario? h = null;

        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_ObtenerHorarioPorId", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdHorario", id);

            cn.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    h = new Horario
                    {
                        IdHorario = (int)dr["IdHorario"],
                        IdRuta = (int)dr["IdRuta"],
                        IdBus = (int)dr["IdBus"],
                        FechaSalida = (DateTime)dr["FechaSalida"],
                        HoraSalida = (TimeSpan)dr["HoraSalida"],
                        HoraLlegada = (TimeSpan)dr["HoraLlegada"],
                        Precio = (decimal)dr["Precio"],
                        Estado = (bool)dr["Estado"]
                    };
                }
            }
        }

        return h;
    }

    
    public void Actualizar(Horario h)
    {
        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_ActualizarHorario", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdHorario", h.IdHorario);
            cmd.Parameters.AddWithValue("@IdRuta", h.IdRuta);
            cmd.Parameters.AddWithValue("@IdBus", h.IdBus);
            cmd.Parameters.AddWithValue("@FechaSalida", h.FechaSalida);
            cmd.Parameters.AddWithValue("@HoraSalida", h.HoraSalida);
            cmd.Parameters.AddWithValue("@HoraLlegada", h.HoraLlegada);
            cmd.Parameters.AddWithValue("@Precio", h.Precio);
            cmd.Parameters.AddWithValue("@Estado", h.Estado);

            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    
    public void Eliminar(int id)
    {
        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_EliminarHorario", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdHorario", id);

            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public void Restaurar(int id)
    {
        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_RestaurarHorario", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdHorario", id);

            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public List<Horario> ListarPaginado(int pagina, int cantidad, bool verEliminados, DateTime? fecha)
    {
        var lista = new List<Horario>();

        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_ListarHorariosPaginado", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Pagina", pagina);
            cmd.Parameters.AddWithValue("@Cantidad", cantidad);
            cmd.Parameters.AddWithValue("@VerEliminados", verEliminados);
            cmd.Parameters.AddWithValue("@Fecha", (object)fecha ?? DBNull.Value);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Horario
                {
                    IdHorario = (int)dr["IdHorario"],
                    Origen = dr["Origen"].ToString(),
                    Destino = dr["Destino"].ToString(),
                    Placa = dr["Placa"].ToString(),
                    FechaSalida = (DateTime)dr["FechaSalida"],
                    HoraSalida = (TimeSpan)dr["HoraSalida"],
                    HoraLlegada = (TimeSpan)dr["HoraLlegada"],
                    Precio = (decimal)dr["Precio"],
                    Estado = (bool)dr["Estado"]
                });
            }
        }

        return lista;
    }
    public int TotalRegistros(bool verEliminados, DateTime? fecha)
    {
        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_TotalHorarios", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VerEliminados", verEliminados);
            cmd.Parameters.AddWithValue("@Fecha", (object)fecha ?? DBNull.Value);

            cn.Open();
            return (int)cmd.ExecuteScalar();
        }
    }
}