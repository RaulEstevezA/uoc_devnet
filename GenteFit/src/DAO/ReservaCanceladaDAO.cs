using GenteFit.src.model.entity;
using GenteFit.src.util;
using Microsoft.Data.SqlClient;

namespace GenteFit.src.DAO
{
    public class ReservaCanceladaDAO : IDao<ReservaCancelada>
    {
        public ReservaCanceladaDAO() { }

        // GET BY ID
        public ReservaCancelada? GetById(int id)
        {
            var query = @"SELECT Id, ClienteId, SesionId, FechaCancelacion, Motivo, 
                                 PosicionEnCancelacion, EstadoPrevio
                          FROM ReservaCancelada
                          WHERE Id = @Id";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                cmd.Parameters.AddWithValue("@Id", id);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new ReservaCancelada
                    {
                        Id = reader.GetInt32(0),
                        ClienteId = reader.GetInt32(1),
                        SesionId = reader.GetInt32(2),
                        FechaCancelacion = reader.GetDateTime(3),
                        Motivo = reader.IsDBNull(4) ? null : reader.GetString(4),
                        PosicionEnCancelacion = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                        EstadoPrevio = reader.GetString(6)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reserva cancelada por ID", ex);
            }

            return null;
        }

        // GET ALL
        public IEnumerable<ReservaCancelada> GetAll()
        {
            var lista = new List<ReservaCancelada>();
            var query = @"SELECT Id, ClienteId, SesionId, FechaCancelacion, Motivo, 
                                 PosicionEnCancelacion, EstadoPrevio
                          FROM ReservaCancelada";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new ReservaCancelada
                    {
                        Id = reader.GetInt32(0),
                        ClienteId = reader.GetInt32(1),
                        SesionId = reader.GetInt32(2),
                        FechaCancelacion = reader.GetDateTime(3),
                        Motivo = reader.IsDBNull(4) ? null : reader.GetString(4),
                        PosicionEnCancelacion = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                        EstadoPrevio = reader.GetString(6)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las reservas canceladas", ex);
            }

            return lista;
        }

        // INSERT (Save)
        public void Save(ReservaCancelada entity)
        {
            var query = @"INSERT INTO ReservaCancelada
                          (ClienteId, SesionId, FechaCancelacion, Motivo, PosicionEnCancelacion, EstadoPrevio)
                          VALUES (@ClienteId, @SesionId, @FechaCancelacion, @Motivo, @Posicion, @EstadoPrevio)";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@ClienteId", entity.ClienteId);
                cmd.Parameters.AddWithValue("@SesionId", entity.SesionId);
                cmd.Parameters.AddWithValue("@FechaCancelacion", entity.FechaCancelacion);
                cmd.Parameters.AddWithValue("@Motivo", (object?)entity.Motivo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Posicion", (object?)entity.PosicionEnCancelacion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EstadoPrevio", entity.EstadoPrevio);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al guardar la reserva cancelada", ex);
            }
        }

        // NOT USED
        public void Update(ReservaCancelada entity)
        {
            throw new NotImplementedException("No se actualizan reservas canceladas");
        }

        public void Delete(ReservaCancelada entity)
        {
            throw new NotImplementedException("No se eliminan reservas canceladas");
        }
    }
}



