using GenteFit.src.DAO;
using GenteFit.src.model.entity;
using System.Collections.Generic;
using System.Linq;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionUsuario
    {
        private static readonly UsuarioDAO usuarioDao = (UsuarioDAO)FactoryDAO.GetUsuarioDAO();
        private static readonly IDao<Cliente> clienteDao = FactoryDAO.GetClienteDAO();
        private static readonly IDao<Instructor> instructorDao = FactoryDAO.GetInstructorDAO();


        public static List<Usuario> GetAllUsuarios()
        {
            var usuarios = usuarioDao.GetAll().ToList();
            var clientes = clienteDao.GetAll().ToList();
            var instructores = instructorDao.GetAll().ToList();

            return usuarios
                .Where(u =>
                    !clientes.Any(c => c.Id == u.Id) &&
                    !instructores.Any(i => i.Id == u.Id)
                )
                .ToList();
        }


        public static Usuario? BuscarPorId(int id)
        {
            var usuario = usuarioDao.GetAll().FirstOrDefault(u => u.Id == id);
            if (usuario == null) return null;

            bool esCliente = clienteDao.GetAll().Any(c => c.Id == id);
            bool esInstructor = instructorDao.GetAll().Any(i => i.Id == id);

            return (!esCliente && !esInstructor) ? usuario : null;
        }


        public static Usuario? BuscarPorEmail(string email)
        {
            var usuario = usuarioDao.GetAll().FirstOrDefault(u => u.Email == email);
            if (usuario == null) return null;

            bool esCliente = clienteDao.GetAll().Any(c => c.Id == usuario.Id);
            bool esInstructor = instructorDao.GetAll().Any(i => i.Id == usuario.Id);

            return (!esCliente && !esInstructor) ? usuario : null;
        }


        public static Usuario? BuscarPorUsername(string username)
        {
            var usuario = usuarioDao.GetAll().FirstOrDefault(u => u.Username == username);
            if (usuario == null) return null;

            bool esCliente = clienteDao.GetAll().Any(c => c.Id == usuario.Id);
            bool esInstructor = instructorDao.GetAll().Any(i => i.Id == usuario.Id);

            return (!esCliente && !esInstructor) ? usuario : null;
        }


        public static void CrearUsuario(string username, string email, string password, int tipoRolId)
        {
            GestionAltaUsuario.CrearUsuario(username, email, password, tipoRolId);
        }


        public static void ModificarUsuario(Usuario u)
        {
            GestionModificarUsuario.ModificarUsuario(u);
        }


        public static void DarDeBajaUsuario(Usuario u)
        {
            // extra seguridad: no permitir dar de baja administrador
            if (u.TipoRolId == 1)
                throw new Exception("no se puede dar de baja a un administrador");

            GestionBajaUsuario.BajaUsuario(u);
        }

    }
}
