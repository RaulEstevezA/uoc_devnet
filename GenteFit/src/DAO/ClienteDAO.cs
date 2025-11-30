using GenteFit.src.model.entity;
using GenteFit.src.util;
using Microsoft.Data.SqlClient;

namespace GenteFit.src.DAO
{
    public class ClienteDAO : IDao<Cliente>
    {
        public ClienteDAO() { }

        // obtiene un cliente por id
        public Cliente? GetById(int id)
        {
            var query = "SELECT id, dni, nombre, apellido1, apellido2, email FROM Cliente WHERE id = @Id";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                cmd.Parameters.AddWithValue("@Id", id);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Cliente
                    {
                        Id = reader.GetInt32(0),
                        Dni = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        Apellido1 = reader.GetString(3),
                        Apellido2 = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Email = reader.IsDBNull(5) ? null : reader.GetString(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener el cliente por id", ex);
            }

            return null;
        }

        // obtiene todos los clientes
        public IEnumerable<Cliente> GetAll()
        {
            var lista = new List<Cliente>();
            var query = "SELECT id, dni, nombre, apellido1, apellido2, email FROM Cliente";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Cliente
                    {
                        Id = reader.GetInt32(0),
                        Dni = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        Apellido1 = reader.GetString(3),
                        Apellido2 = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Email = reader.IsDBNull(5) ? null : reader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener la lista de clientes", ex);
            }

            return lista;
        }

        // guarda un cliente
        public void Save(Cliente entity)
        {
            var query = @"INSERT INTO Cliente (id, dni, nombre, apellido1, apellido2, email)
                          VALUES (@Id, @Dni, @Nombre, @Apellido1, @Apellido2, @Email)";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Dni", entity.Dni);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                cmd.Parameters.AddWithValue("@Apellido1", entity.Apellido1);
                cmd.Parameters.AddWithValue("@Apellido2", (object?)entity.Apellido2 ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object?)entity.Email ?? DBNull.Value);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("error al guardar el cliente", ex);
            }
        }

        // actualiza un cliente
        public void Update(Cliente entity)
        {
            var query = @"UPDATE Cliente 
                          SET dni = @Dni, nombre = @Nombre, apellido1 = @Apellido1, 
                              apellido2 = @Apellido2, email = @Email
                          WHERE id = @Id";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Dni", entity.Dni);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                cmd.Parameters.AddWithValue("@Apellido1", entity.Apellido1);
                cmd.Parameters.AddWithValue("@Apellido2", (object?)entity.Apellido2 ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object?)entity.Email ?? DBNull.Value);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("error al actualizar el cliente", ex);
            }
        }

        // elimina un cliente
        public void Delete(Cliente entity)
        {
            var query = "UPDATE Cliente SET activo = 0 WHERE id = @Id";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);
                cmd.Parameters.AddWithValue("@Id", entity.Id);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("error al dar de baja el cliente", ex);
            }
        }
    }
}


// TODO Versi�n simple sin control de errores, implementar try-cach donde sea necesario.