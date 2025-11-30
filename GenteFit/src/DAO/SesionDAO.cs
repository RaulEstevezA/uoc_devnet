using GenteFit.src.model.entity;
using GenteFit.src.util;
using Microsoft.Data.SqlClient;

namespace GenteFit.src.DAO
{
    public class SesionDAO : IDao<Sesion>
    {
        public SesionDAO() { }

        // obtiene una sesion por id
        public Sesion? GetById(int id)
        {
            var query = @"SELECT Id, ActividadId, InstructorId, SalaId, FechaInicio, FechaFin
                          FROM Sesion WHERE Id = @Id";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                cmd.Parameters.AddWithValue("@Id", id);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Sesion
                    {
                        Id = reader.GetInt32(0),
                        ActividadId = reader.GetInt32(1),
                        InstructorId = reader.GetInt32(2),
                        SalaId = reader.GetInt32(3),
                        FechaInicio = reader.GetDateTime(4),
                        FechaFin = reader.GetDateTime(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener la sesion por id", ex);
            }

            return null;
        }

        // obtiene todas las sesiones
        public IEnumerable<Sesion> GetAll()
        {
            var lista = new List<Sesion>();
            var query = @"SELECT Id, ActividadId, InstructorId, SalaId, FechaInicio, FechaFin 
                          FROM Sesion";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Sesion
                    {
                        Id = reader.GetInt32(0),
                        ActividadId = reader.GetInt32(1),
                        InstructorId = reader.GetInt32(2),
                        SalaId = reader.GetInt32(3),
                        FechaInicio = reader.GetDateTime(4),
                        FechaFin = reader.GetDateTime(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener la lista de sesiones", ex);
            }

            return lista;
        }

        // obtiene las sesiones por fecha (dia exacto)
        public IEnumerable<Sesion> GetByDate(DateTime fecha)
        {
            var lista = new List<Sesion>();
            var query = @"SELECT Id, ActividadId, InstructorId, SalaId, FechaInicio, FechaFin
                          FROM Sesion
                          WHERE CONVERT(date, FechaInicio) = @Fecha";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Sesion
                    {
                        Id = reader.GetInt32(0),
                        ActividadId = reader.GetInt32(1),
                        InstructorId = reader.GetInt32(2),
                        SalaId = reader.GetInt32(3),
                        FechaInicio = reader.GetDateTime(4),
                        FechaFin = reader.GetDateTime(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener las sesiones por fecha", ex);
            }

            return lista;
        }

        // guarda una sesion
        public void Save(Sesion entity)
        {
            var query = @"INSERT INTO Sesion 
                         (ActividadId, InstructorId, SalaId, FechaInicio, FechaFin)
                         VALUES (@ActividadId, @InstructorId, @SalaId, @FechaInicio, @FechaFin)";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@ActividadId", entity.ActividadId);
                cmd.Parameters.AddWithValue("@InstructorId", entity.InstructorId);
                cmd.Parameters.AddWithValue("@SalaId", entity.SalaId);
                cmd.Parameters.AddWithValue("@FechaInicio", entity.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", entity.FechaFin);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("error al guardar la sesion", ex);
            }
        }

        // actualiza una sesion
        public void Update(Sesion entity)
        {
            var query = @"UPDATE Sesion SET
                            ActividadId = @ActividadId,
                            InstructorId = @InstructorId,
                            SalaId = @SalaId,
                            FechaInicio = @FechaInicio,
                            FechaFin = @FechaFin
                          WHERE Id = @Id";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@ActividadId", entity.ActividadId);
                cmd.Parameters.AddWithValue("@InstructorId", entity.InstructorId);
                cmd.Parameters.AddWithValue("@SalaId", entity.SalaId);
                cmd.Parameters.AddWithValue("@FechaInicio", entity.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", entity.FechaFin);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("error al actualizar la sesion", ex);
            }
        }

        // elimina una sesion
        public void Delete(Sesion entity)
        {
            var query = "DELETE FROM Sesion WHERE Id = @Id";

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
                throw new Exception("error al eliminar la sesion", ex);
            }
        }
    }
}
