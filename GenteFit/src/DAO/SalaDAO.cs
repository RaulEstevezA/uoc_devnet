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
            using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Sala> GetAll()
        {
            var salas = new List<Sala>();
            var query = "SELECT Id, Nombre, AforoMax, Disponible FROM Sala";
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
            return salas;
        }

        public Sala? GetById(int id)
        {
            var query = "SELECT Id, Nombre, AforoMax, Disponible FROM Sala WHERE Id = @Id";
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
            return null;
        }

        public void Save(Sala entity)
        {
            var query = "INSERT INTO Sala (Nombre, AforoMax, Disponible) VALUES (@Nombre, @AforoMax, @Disponible)";
            using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
            cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
            cmd.Parameters.AddWithValue("@AforoMax", entity.AforoMax);
            cmd.Parameters.AddWithValue("@Disponible", entity.Disponible);
            cmd.ExecuteNonQuery();
        }

        public void Update(Sala entity)
        {
            var query = "UPDATE Sala SET Nombre = @Nombre, AforoMax = @AforoMax, Disponible = @Disponible WHERE Id = @Id";
            using var cmd = new SqlCommand(query, ConexionDb.Instance.Connection);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
            cmd.Parameters.AddWithValue("@AforoMax", entity.AforoMax);
            cmd.Parameters.AddWithValue("@Disponible", entity.Disponible);
            cmd.ExecuteNonQuery();
        }
    }







}

