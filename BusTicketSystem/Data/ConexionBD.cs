using Microsoft.Data.SqlClient;

namespace BusTicketSystem.Data
{
    public class ConexionBD
    {
        private readonly string _cadenaConexion;

        public ConexionBD(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("cn")!;
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }
    }
}