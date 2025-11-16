using GenteFit.src.model.entity;
using GenteFit.src.model.enums;
using GenteFit.src.util;
using Microsoft.Data.SqlClient;

namespace GenteFit.src.DAO
{
    public class ReservaDAO : IDao<Reserva>
    {
        public ReservaDAO() { }

        // obtiene una reserva por id
        public Reserva? GetById(int id)
        {
            var query = @"SELECT Id, ClienteId, SesionId, EstadoReserva, FechaReserva, PosicionEspera 
                          FROM Reserva WHERE Id = @Id";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                cmd.Parameters.AddWithValue("@Id", id);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Reserva
                    {
                        Id = reader.GetInt32(0),
                        ClienteId = reader.GetInt32(1),
                        SesionId = reader.GetInt32(2),
                        EstadoReserva = (TipoEstado)reader.GetInt32(3),
                        FechaReserva = reader.GetDateTime(4),
                        PosicionEspera = reader.IsDBNull(5) ? null : reader.GetInt32(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener la reserva por id", ex);
            }

            return null;
        }

        // obtiene todas las reservas
        public IEnumerable<Reserva> GetAll()
        {
            var lista = new List<Reserva>();
            var query = @"SELECT Id, ClienteId, SesionId, EstadoReserva, FechaReserva, PosicionEspera 
                          FROM Reserva";

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Reserva
                    {
                        Id = reader.GetInt32(0),
                        ClienteId = reader.GetInt32(1),
                        SesionId = reader.GetInt32(2),
                        EstadoReserva = (TipoEstado)reader.GetInt32(3),
                        FechaReserva = reader.GetDateTime(4),
                        PosicionEspera = reader.IsDBNull(5) ? null : reader.GetInt32(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener las reservas", ex);
            }

            return lista;
        }

        // guarda una reserva
        public void Save(Reserva entity)
        {
            var query = @"INSERT INTO Reserva 
                         (ClienteId, SesionId, EstadoReserva, FechaReserva, PosicionEspera)
                         VALUES (@ClienteId, @SesionId, @EstadoReserva, @FechaReserva, @PosicionEspera)";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@ClienteId", entity.ClienteId);
                cmd.Parameters.AddWithValue("@SesionId", entity.SesionId);
                cmd.Parameters.AddWithValue("@EstadoReserva", (int)entity.EstadoReserva);
                cmd.Parameters.AddWithValue("@FechaReserva", entity.FechaReserva);
                cmd.Parameters.AddWithValue("@PosicionEspera", (object?)entity.PosicionEspera ?? DBNull.Value);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("error al guardar la reserva", ex);
            }
        }

        // actualiza una reserva
        public void Update(Reserva entity)
        {
            var query = @"UPDATE Reserva SET 
                            ClienteId = @ClienteId,
                            SesionId = @SesionId,
                            EstadoReserva = @EstadoReserva,
                            FechaReserva = @FechaReserva,
                            PosicionEspera = @PosicionEspera
                          WHERE Id = @Id";

            using var transaction = ConexionDb.Instance.Connection.BeginTransaction();

            try
            {
                using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection, transaction);

                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@ClienteId", entity.ClienteId);
                cmd.Parameters.AddWithValue("@SesionId", entity.SesionId);
                cmd.Parameters.AddWithValue("@EstadoReserva", (int)entity.EstadoReserva);
                cmd.Parameters.AddWithValue("@FechaReserva", entity.FechaReserva);
                cmd.Parameters.AddWithValue("@PosicionEspera", (object?)entity.PosicionEspera ?? DBNull.Value);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("error al actualizar la reserva", ex);
            }
        }

        // elimina una reserva
        public void Delete(Reserva entity)
        {
            var query = "DELETE FROM Reserva WHERE Id = @Id";

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
                throw new Exception("error al eliminar la reserva", ex);
            }
        }
    }
}


