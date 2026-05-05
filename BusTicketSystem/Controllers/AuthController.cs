using BusTicketSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusTicketSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly string _cn;

        public AuthController(IConfiguration config)
        {
            _cn = config.GetConnectionString("cn");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string correo, string clave)
        {
            using (SqlConnection cn = new SqlConnection(_cn))
            {
                SqlCommand cmd = new SqlCommand("sp_Login", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Clave", clave);

                cn.Open();
                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int idUsuario = (int)dr["IdUsuario"];
                    int idCliente = (int)dr["IdCliente"];

                    // 🔥 GUARDAR SESIÓN
                    HttpContext.Session.SetInt32("IdUsuario", idUsuario);
                    HttpContext.Session.SetInt32("IdCliente", idCliente);

                    // 🔥 ESTA ES LA LÍNEA QUE TE FALTABA
                    HttpContext.Session.SetString("NombreUsuario", dr["Nombres"].ToString());

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Credenciales incorrectas";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}