using Microsoft.Data.SqlClient;

namespace GenteFit.src.util
{
    public class ConexionDb
    {
        private static ConexionDb? _instance;
        private readonly SqlConnection _connection;

        // Cambia la cadena de conexión según tu configuración
        private const string ConnectionString = "Server=localhost;Database=GenteFit;User Id=sa;Password=Passw0rd!;Encrypt=True;TrustServerCertificate=True";

        private ConexionDb()
        {
            _connection = new SqlConnection(ConnectionString);
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
// ConixionDb.Instance.CloseConnection(); // Cierra la conexión cuando ya no se necesite