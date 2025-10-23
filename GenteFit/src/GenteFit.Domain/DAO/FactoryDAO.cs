using System;
using Microsoft.Extensions.DependencyInjection;

namespace GenteFit.Domain.DAO
{
    public static class FactoryDAO
    {
        public static IDao GetDao(string daoType)
        {
            switch (daoType.Trim().ToUpperInvariant())
            {
                case "CLIENTEDB":
                    return new ClienteDAO();
                //case "MONITORDB":
                //    return new MonitorDAO();
                default:
                    throw new ArgumentException($"Tipo de DAO desconocido: {daoType}");
            }
        }
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