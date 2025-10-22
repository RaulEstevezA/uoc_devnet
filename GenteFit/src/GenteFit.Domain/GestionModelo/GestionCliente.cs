// Estas clases representan la gestión de clientes en el dominio de GenteFit.
// Nos servirá para implementar la lógica de negocio relacionada con los clientes.
// Hacen uso de factoryDAO y DAOs para interactuar con la base de datos.
// Si es necesario harán uso de factory para acceder a XML u otros servicios.

namespace GenteFit.Domain.GestionModelo
{
    public class GestionCliente
    {
        // Implementación de la gestión de clientes

        // Ejemplo de método para agregar un cliente
        public void AgregarCliente(Cliente cliente)
        {
            // Lógica para agregar un cliente
            var clienteDao = DAO.FactoryDAO.GetDao("CLIENTEDB");
            clienteDao.Save(cliente);
        }

        // Así con las diferentes funciones que necesitemos
    }
}


// Esta clase será llamada desde el controlador de clientes en la capa de presentación.