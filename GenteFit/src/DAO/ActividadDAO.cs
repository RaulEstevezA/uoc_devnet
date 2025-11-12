using GenteFit.src.model.entity;
using GenteFit.src.util;
using Microsoft.Data.SqlClient;

namespace GenteFit.src.DAO
{
    public class ActividadDAO : IDao<Actividad>
    {
        public ActividadDAO() { }

        public void Delete(Actividad entity)
        {
            var query = "DELETE FROM Actividad WHERE Id = @Id";

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
                throw new Exception("error al eliminar la actividad", ex);
            }
        }
        
        public IEnumerable<Actividad> GetAll()
        {
            var actividades = new List<Actividad>();
            var query = "SELECT Id, Nombre, DuracionMin, PlazasMax FROM Actividad";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    actividades.Add(new Actividad
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        DuracionMin = reader.GetInt32(2),
                        PlazasMax = reader.GetInt32(3)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener las actividades", ex);
            }

            return actividades;
        }

        public Actividad? GetById(int id)
        {
            var query = "SELECT Id, Nombre, DuracionMin, PlazasMax FROM Actividad WHERE Id = @Id";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                cmd.Parameters.AddWithValue("@Id", id);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Actividad
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        DuracionMin = reader.GetInt32(2),
                        PlazasMax = reader.GetInt32(3)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener la actividad por id", ex);
            }

            return null;
        }

        public void Save(Actividad entity)
        {
            var query = "INSERT INTO Actividad (Nombre, DuracionMin, PlazasMax) VALUES (@Nombre, @DuracionMin, @PlazasMax)";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                cmd.Parameters.AddWithValue("@DuracionMin", entity.DuracionMin);
                cmd.Parameters.AddWithValue("@PlazasMax", entity.PlazasMax);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("error al guardar la actividad", ex);
            }
        }

        public void Update(Actividad entity)
        {
            var query = "UPDATE Actividad SET Nombre = @Nombre, DuracionMin = @DuracionMin, PlazasMax = @PlazasMax WHERE Id = @Id";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                cmd.Parameters.AddWithValue("@DuracionMin", entity.DuracionMin);
                cmd.Parameters.AddWithValue("@PlazasMax", entity.PlazasMax);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("error al actualizar la actividad", ex);
            }
        }
    }
}