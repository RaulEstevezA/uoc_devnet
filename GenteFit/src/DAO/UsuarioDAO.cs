using GenteFit.src.model.entity;
using GenteFit.src.util;
using Microsoft.Data.SqlClient;

namespace GenteFit.src.DAO
{
    public class UsuarioDAO : IDao<Usuario>
    {
        public UsuarioDAO() { }


        // obtener usuario por id
        public Usuario? GetById(int id)
        {
            string query = @"
                SELECT Id, Username, Email, PasswordHash, TipoRolId, Activo, CreadoEn 
                FROM Usuario 
                WHERE Id = @Id";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                cmd.Parameters.AddWithValue("@Id", id);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Usuario
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Email = reader.GetString(2),
                        PasswordHash = reader.GetString(3),
                        TipoRolId = reader.GetInt32(4),
                        Activo = reader.GetBoolean(5),
                        CreadoEn = reader.GetDateTime(6)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario por ID", ex);
            }

            return null;
        }

        // obtener todos usuarios
        public IEnumerable<Usuario> GetAll()
        {
            var lista = new List<Usuario>();

            string query = @"
                SELECT Id, Username, Email, PasswordHash, TipoRolId, Activo, CreadoEn 
                FROM Usuario";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Usuario
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Email = reader.GetString(2),
                        PasswordHash = reader.GetString(3),
                        TipoRolId = reader.GetInt32(4),
                        Activo = reader.GetBoolean(5),
                        CreadoEn = reader.GetDateTime(6)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de usuarios", ex);
            }

            return lista;
        }

        // Buscar por nombre de usuarui
        public Usuario? GetByUsername(string username)
        {
            string query = @"
                SELECT Id, Username, Email, PasswordHash, TipoRolId, Activo, CreadoEn
                FROM Usuario 
                WHERE Username = @Username";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                cmd.Parameters.AddWithValue("@Username", username);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Usuario
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Email = reader.GetString(2),
                        PasswordHash = reader.GetString(3),
                        TipoRolId = reader.GetInt32(4),
                        Activo = reader.GetBoolean(5),
                        CreadoEn = reader.GetDateTime(6)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por Username", ex);
            }

            return null;
        }

        // buscar por email
        public Usuario? GetByEmail(string email)
        {
            string query = @"
                SELECT Id, Username, Email, PasswordHash, TipoRolId, Activo, CreadoEn
                FROM Usuario 
                WHERE Email = @Email";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                cmd.Parameters.AddWithValue("@Email", email);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Usuario
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Email = reader.GetString(2),
                        PasswordHash = reader.GetString(3),
                        TipoRolId = reader.GetInt32(4),
                        Activo = reader.GetBoolean(5),
                        CreadoEn = reader.GetDateTime(6)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por Email", ex);
            }

            return null;
        }

        // guardar usuario
        public void Save(Usuario entity)
        {
            string query = @"
                INSERT INTO Usuario (Username, Email, PasswordHash, TipoRolId, Activo)
                VALUES (@Username, @Email, @PasswordHash, @TipoRolId, @Activo)";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@Username", entity.Username);
                cmd.Parameters.AddWithValue("@Email", entity.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                cmd.Parameters.AddWithValue("@TipoRolId", entity.TipoRolId);
                cmd.Parameters.AddWithValue("@Activo", entity.Activo);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al guardar usuario", ex);
            }
        }

        // actualizar usuario
        public void Update(Usuario entity)
        {
            string query = @"
                UPDATE Usuario
                SET Username = @Username,
                    Email = @Email,
                    PasswordHash = @PasswordHash,
                    TipoRolId = @TipoRolId,
                    Activo = @Activo
                WHERE Id = @Id";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Username", entity.Username);
                cmd.Parameters.AddWithValue("@Email", entity.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                cmd.Parameters.AddWithValue("@TipoRolId", entity.TipoRolId);
                cmd.Parameters.AddWithValue("@Activo", entity.Activo);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al actualizar usuario", ex);
            }
        }

        // eliminar usuario
        public void Delete(Usuario entity)
        {
            string query = "DELETE FROM Usuario WHERE Id = @Id";

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
                throw new Exception("Error al eliminar usuario", ex);
            }
        }
    }
}

