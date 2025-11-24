using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GenteFit.src.util
{
    public class ConexionDb
    {
        private static ConexionDb? _instance;
        private readonly SqlConnection _connection;
        private static string? _connectionString;

        private ConexionDb()
        {
            if (_connectionString == null)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                _connectionString = config.GetConnectionString("GenteFitDb");
            }
            _connection = new SqlConnection(_connectionString);
        }

        public static ConexionDb Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConexionDb();

                return _instance;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                if (_connection.State == System.Data.ConnectionState.Closed)
                    _connection.Open();
                return _connection;
            }
        }

        public void CloseConnection()
        {
            if (_connection.State != System.Data.ConnectionState.Closed)
                _connection.Close();
        }
    }
}

// Como se usa?

// var  = ConixionDb.Instance.Connection; // Obtiene la conexión SQL
// // Usar la conexión...
// ConixionDb.Instance.CloseConnection(); // Cierra la conexi�n cuando ya no se necesite