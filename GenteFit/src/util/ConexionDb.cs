using Microsoft.Data.SqlClient;

namespace GenteFit.src.util
{
    public class ConexionDb
    {
        private static ConexionDb? _instance;
        private readonly SqlConnection _connection;

        // Cambia la cadena de conexi�n seg�n tu configuraci�n
        private const string ConnectionString = "Server=DESKTOP-F7NO4ST\\LOCALHOST;Database=GenteFit;User Id=sa;Password=Passw0rd!;Encrypt=True;TrustServerCertificate=True";

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

// var  = ConixionDb.Instance.Connection; // Obtiene la conexi�n SQL
// // Usar la conexi�n...
// ConixionDb.Instance.CloseConnection(); // Cierra la conexi�n cuando ya no se necesite