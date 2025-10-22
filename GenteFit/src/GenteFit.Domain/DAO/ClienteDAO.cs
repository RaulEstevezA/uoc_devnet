using System.Collections.Generic;
using System.Linq;
using GenteFit.Domain;
using GenteFit.Infrastructure;

namespace GenteFit.Domain.DAO
{
    public class ClienteDAO : IDao<Cliente>
    {
        private readonly GenteFitDbContext _db;

        public ClienteDAO(GenteFitDbContext db)
        {
            _db = db;
        }

        public Cliente? GetById(int id)
        {
            return _db.Clientes.Find(id);
        }

        public IEnumerable<Cliente> GetAll()
        {
            return _db.Clientes.AsNoTracking().ToList();
        }

        public void Save(Cliente entity)
        {
            _db.Clientes.Add(entity);
            _db.SaveChanges();
        }

        public void Update(Cliente entity)
        {
            _db.Clientes.Update(entity);
            _db.SaveChanges();
        }

        public void Delete(Cliente entity)
        {
            _db.Clientes.Remove(entity);
            _db.SaveChanges();
        }
    }
}