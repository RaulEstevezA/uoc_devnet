using GenteFit.src.model.entity;
using GenteFit.src.util;
using Microsoft.Data.SqlClient;

namespace GenteFit.src.DAO
{
    public class ClienteDAO : IDao<Cliente>
    {
        public void Delete(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> GetAll()
        {
            throw new NotImplementedException();
        }

        public Cliente? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Cliente entity)
        {
            throw new NotImplementedException();
        }
    }
}

// TODO Versión simple sin control de errores, implementar try-cach donde sea necesario.