using Microsoft.Data.SqlClient;

// Lo usamos para probar conexiones a SQL Server


namespace GenteFit.Database
{
    public static class SqlServerHelper
    {
        public static async Task<bool> TestConnectionAsync(string connectionString, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("La cadena de conexi�n est� vac�a.", nameof(connectionString));

            try
            {
                await using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync(ct);

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT 1";
                var result = await cmd.ExecuteScalarAsync(ct);

                return Convert.ToInt32(result) == 1;
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"SqlException: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }
    }
}