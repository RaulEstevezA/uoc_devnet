using GenteFit.src.model.entity;
using GenteFit.src.util;
using Microsoft.Data.SqlClient;

namespace GenteFit.src.DAO
{
    public class SalaDAO : IDao<Sala>
    {
        public SalaDAO() { }

        public void Delete(Sala entity)
        {
            var query = "DELETE FROM Sala WHERE Id = @Id";
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
                // Aquí puedes registrar el error (log) o relanzar la excepción
                throw new Exception("Error al eliminar la sala.", ex);
            }
        }

        public IEnumerable<Sala> GetAll()
        {
            var salas = new List<Sala>();
            var query = "SELECT Id, Nombre, AforoMax, Disponible FROM Sala";
            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    salas.Add(new Sala
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        AforoMax = reader.GetInt32(2),
                        Disponible = reader.GetBoolean(3)
                    });
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error (log) o relanzar la excepción
                throw new Exception("Error al obtener las salas.", ex);
            }
            return salas;
        }

        public Sala? GetById(int id)
        {
            var query = "SELECT Id, Nombre, AforoMax, Disponible FROM Sala WHERE Id = @Id";
            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                cmd.Parameters.AddWithValue("@Id", id);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Sala
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        AforoMax = reader.GetInt32(2),
                        Disponible = reader.GetBoolean(3)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la sala por ID.", ex);
            }
            return null;
        }

        public void Save(Sala entity)
        {
            var query = "INSERT INTO Sala (Nombre, AforoMax, Disponible) VALUES (@Nombre, @AforoMax, @Disponible)";
            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();
            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                cmd.Parameters.AddWithValue("@AforoMax", entity.AforoMax);
                cmd.Parameters.AddWithValue("@Disponible", entity.Disponible);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al guardar la sala.", ex);
            }
        }

        public void Update(Sala entity)
        {
            var query = "UPDATE Sala SET Nombre = @Nombre, AforoMax = @AforoMax, Disponible = @Disponible WHERE Id = @Id";
            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();
            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                cmd.Parameters.AddWithValue("@AforoMax", entity.AforoMax);
                cmd.Parameters.AddWithValue("@Disponible", entity.Disponible);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al actualizar la sala.", ex);
            }
        }
    }
}

