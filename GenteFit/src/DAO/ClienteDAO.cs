using GenteFit.src.model.entity;

namespace GenteFit.src.DAO
{

    // Importar los espacios de nombres necesarios
    
    // IMPORTANTE, ESTO ESTABA CREADO EN ANTERIOR PROYECTO, PENDIENTE ADAPTAR


    // DAO para la entidad Cliente

    public class ClienteDAO : IDao<Cliente>
    {
        public ClienteDAO() { }

        
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

// TODO Versión simple sin control de errores, implementar try-cach donde sea necesario.