using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

public class ReportesController : Controller
{
    private readonly string _cn;

    public ReportesController(IConfiguration config)
    {
        _cn = config.GetConnectionString("cn");
    }

    public IActionResult Index()
    {
        using (SqlConnection cn = new SqlConnection(_cn))
        {
            cn.Open();

            // 🔥 KPIs
            SqlCommand cmd = new SqlCommand("sp_DashboardKPIs", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                ViewBag.TotalRutas = dr["TotalRutas"];
                ViewBag.TotalBuses = dr["TotalBuses"];
                ViewBag.TotalHorarios = dr["TotalHorarios"];
                ViewBag.TotalReservas = dr["TotalReservas"];
                ViewBag.TotalIngresos = dr["TotalIngresos"];
            }
            dr.Close();

            // 📈 Reservas por día
            List<string> fechas = new();
            List<int> reservas = new();

            cmd = new SqlCommand("sp_ReservasPorDia", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                fechas.Add(Convert.ToDateTime(dr["Fecha"]).ToString("dd/MM"));
                reservas.Add((int)dr["Total"]);
            }
            dr.Close();

            ViewBag.Fechas = fechas;
            ViewBag.Reservas = reservas;

            // 💰 Ingresos
            List<string> meses = new();
            List<decimal> ingresos = new();

            cmd = new SqlCommand("sp_IngresosPorMes", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                meses.Add(dr["Mes"].ToString());
                ingresos.Add((decimal)dr["Total"]);
            }

            ViewBag.Meses = meses;
            ViewBag.Ingresos = ingresos;
        }

        return View();
    }
}