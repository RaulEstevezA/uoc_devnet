using GenteFit.src.model.entity;
using GenteFit.src.util;
using Microsoft.Data.SqlClient;

namespace GenteFit.src.DAO
{
    public class UsuarioDAO : IDao<Usuario>
    {
        public UsuarioDAO() { }

        // obtiene un usuario por id
        public Usuario? GetById(int id)
        {
            var query = @"SELECT id, username, email, passwordHash, tipoRol, activo, creadoEn 
                          FROM Usuario WHERE id = @Id";

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
                        TipoRol = reader.GetString(4),
                        Activo = reader.GetBoolean(5),
                        CreadoEn = reader.GetDateTime(6)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener el usuario por id", ex);
            }

            return null;
        }

        // obtiene todos los usuarios
        public IEnumerable<Usuario> GetAll()
        {
            var lista = new List<Usuario>();
            var query = @"SELECT id, username, email, passwordHash, tipoRol, activo, creadoEn 
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
                        TipoRol = reader.GetString(4),
                        Activo = reader.GetBoolean(5),
                        CreadoEn = reader.GetDateTime(6)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener la lista de usuarios", ex);
            }

            return lista;
        }

        // busca un usuario por username
        public Usuario? GetByUsername(string username)
        {
            var query = @"SELECT id, username, email, passwordHash, tipoRol, activo, creadoEn
                          FROM Usuario WHERE username = @Username";

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
                        TipoRol = reader.GetString(4),
                        Activo = reader.GetBoolean(5),
                        CreadoEn = reader.GetDateTime(6)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener usuario por username", ex);
            }

            return null;
        }

        // busca un usuario por email
        public Usuario? GetByEmail(string email)
        {
            var query = @"SELECT id, username, email, passwordHash, tipoRol, activo, creadoEn
                          FROM Usuario WHERE email = @Email";

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
                        TipoRol = reader.GetString(4),
                        Activo = reader.GetBoolean(5),
                        CreadoEn = reader.GetDateTime(6)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener usuario por email", ex);
            }

            return null;
        }

        // guarda un usuario
        public void Save(Usuario entity)
        {
            var query = @"INSERT INTO Usuario (username, email, passwordHash, tipoRol, activo) 
                          VALUES (@Username, @Email, @PasswordHash, @TipoRol, @Activo)";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@Username", entity.Username);
                cmd.Parameters.AddWithValue("@Email", entity.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                cmd.Parameters.AddWithValue("@TipoRol", entity.TipoRol);
                cmd.Parameters.AddWithValue("@Activo", entity.Activo);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("error al guardar el usuario", ex);
            }
        }

        // actualiza un usuario
        public void Update(Usuario entity)
        {
            var query = @"UPDATE Usuario 
                          SET username = @Username, email = @Email, passwordHash = @PasswordHash,
                              tipoRol = @TipoRol, activo = @Activo
                          WHERE id = @Id";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Username", entity.Username);
                cmd.Parameters.AddWithValue("@Email", entity.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                cmd.Parameters.AddWithValue("@TipoRol", entity.TipoRol);
                cmd.Parameters.AddWithValue("@Activo", entity.Activo);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("error al actualizar el usuario", ex);
            }
        }

        // elimina un usuario
        public void Delete(Usuario entity)
        {
            var query = "DELETE FROM Usuario WHERE id = @Id";

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
                throw new Exception("error al eliminar el usuario", ex);
            }
        }
    }
}
