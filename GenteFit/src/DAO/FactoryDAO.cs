using GenteFit.src.model.entity;

namespace GenteFit.src.DAO
{
    public static class FactoryDAO
    {
        public static IDao<Sala> GetSalaDAO() => new SalaDAO();
        public static IDao<Cliente> GetClienteDAO() => new ClienteDAO();
    }
}




// Como usar el factory:

// Creación de instancias:
// var clienteDao = FactoryDAO.GetDao("CLIENTEDB");
// var monitorDao = FactoryDAO.GetDao("MONITORDB");

// Ejemplo de uso:
// var clienteDao = FactoryDAO.GetDao("CLIENTEDB");
// var cliente = clienteDao.GetById(1);

// Como resultado, se obtiene una instancia del DAO y en este caso se llama al método GetById
// para obtener un cliente con el  ID 1.


// Nota: Los DAOs deben tener un constructor sin parámetros.