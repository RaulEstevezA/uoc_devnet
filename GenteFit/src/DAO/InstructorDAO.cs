using GenteFit.src.model.entity;
using GenteFit.src.util;
using Microsoft.Data.SqlClient;

namespace GenteFit.src.DAO
{
    public class InstructorDAO : IDao<Instructor>
    {
        public InstructorDAO() { }

        // elimina un instructor
        public void Delete(Instructor entity)
        {
            var query = "DELETE FROM Instructor WHERE id = @Id";

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
                throw new Exception("error al eliminar el instructor", ex);
            }
        }

        // obtiene un instructor por id
        public Instructor? GetById(int id)
        {
            var query = "SELECT id, nombre, apellido1, apellido2, email FROM Instructor WHERE id = @Id";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                cmd.Parameters.AddWithValue("@Id", id);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Instructor
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido1 = reader.GetString(2),
                        Apellido2 = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Email = reader.IsDBNull(4) ? null : reader.GetString(4)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener el instructor por id", ex);
            }

            return null;
        }

        // obtiene todos los instructores
        public IEnumerable<Instructor> GetAll()
        {
            var lista = new List<Instructor>();
            var query = "SELECT id, nombre, apellido1, apellido2 FROM Instructor";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Instructor
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido1 = reader.GetString(2),
                        Apellido2 = reader.IsDBNull(3) ? null : reader.GetString(3),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener la lista de instructores", ex);
            }

            return lista;
        }

        // guarda un instructor
        public void Save(Instructor entity)
        {
            var query = @"INSERT INTO Instructor (id, nombre, apellido1, apellido2, email)
                          VALUES (@Id, @Nombre, @Apellido1, @Apellido2, @Email)";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@Id", entity.Id);
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
                throw new Exception("error al guardar el instructor", ex);
            }
        }

        // actualiza un instructor
        public void Update(Instructor entity)
        {
            var query = @"UPDATE Instructor 
                          SET nombre = @Nombre, apellido1 = @Apellido1, apellido2 = @Apellido2, email = @Email
                          WHERE id = @Id";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@Id", entity.Id);
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
                throw new Exception("error al actualizar el instructor", ex);
            }
        }
    }
}
