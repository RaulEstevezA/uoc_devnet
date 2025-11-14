using GenteFit.src.DAO;
using GenteFit.src.model.entity;
using System.Linq;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionAltaCliente
    {
        public static void CrearCliente(
            string username,
            string emailUsuario,
            string password,
            string nombre,
            string apellido1,
            string apellido2,
            string dni,
            string emailCliente
        )
        {
            // dao específico (lo necesitamos para GetAll)
            var usuarioDao = (UsuarioDAO)FactoryDAO.GetUsuarioDAO();
            var clienteDao = FactoryDAO.GetClienteDAO();

            // 1. crear usuario
            GestionAltaUsuario.CrearUsuario(
                username,
                emailUsuario,
                password,
                tipoRolId: 4
            );

            // 2. recuperar el usuario recién creado
            var usuarios = usuarioDao.GetAll();
            var usuarioCreado = usuarios.FirstOrDefault(u => u.Username == username);

            if (usuarioCreado == null)
                throw new Exception("no se pudo recuperar el usuario tras crearlo");

            // 3. crear cliente usando el ID del usuario
            Cliente cliente = new Cliente
            {
                Id = usuarioCreado.Id,
                Nombre = nombre,
                Apellido1 = apellido1,
                Apellido2 = apellido2,
                Dni = dni,
                Email = emailCliente
            };

            // 4. guardar cliente
            clienteDao.Save(cliente);
        }
    }
}



