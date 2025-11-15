using GenteFit.src.DAO;
using GenteFit.src.model.entity;
using System.Collections.Generic;
using System.Linq;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionCliente
    {
        private static readonly IDao<Cliente> clienteDao = FactoryDAO.GetClienteDAO();
        private static readonly UsuarioDAO usuarioDao = (UsuarioDAO)FactoryDAO.GetUsuarioDAO();


        // GET ALL CLIENTES (con Usuario cargado)
        public static List<Cliente> GetAllClientes()
        {
            var clientes = clienteDao.GetAll().ToList();
            var usuarios = usuarioDao.GetAll();

            // unir cliente + usuario por ID (1–1)
            foreach (var c in clientes)
            {
                c.Usuario = usuarios.FirstOrDefault(u => u.Id == c.Id);
            }

            return clientes;
        }


        // BUSCAR POR ID (correcto: buscar en Usuario)
        public static Cliente? BuscarPorId(int id)
        {
            var usuario = usuarioDao.GetAll().FirstOrDefault(u => u.Id == id);
            if (usuario == null) return null;

            var cliente = clienteDao.GetAll().FirstOrDefault(c => c.Id == id);
            if (cliente == null) return null;

            cliente.Usuario = usuario;
            return cliente;
        }


        // BUSCAR POR EMAIL DE USUARIO (login)
        public static Cliente? BuscarPorEmailUsuario(string emailUsuario)
        {
            var usuario = usuarioDao.GetAll()
                                    .FirstOrDefault(u => u.Email == emailUsuario);

            if (usuario == null) return null;

            var cliente = clienteDao.GetAll().FirstOrDefault(c => c.Id == usuario.Id);
            if (cliente == null) return null;

            cliente.Usuario = usuario;
            return cliente;
        }


        // BUSCAR POR USERNAME DE USUARIO
        public static Cliente? BuscarPorUsername(string username)
        {
            var usuario = usuarioDao.GetAll()
                                    .FirstOrDefault(u => u.Username == username);

            if (usuario == null) return null;

            var cliente = clienteDao.GetAll().FirstOrDefault(c => c.Id == usuario.Id);
            if (cliente == null) return null;

            cliente.Usuario = usuario;
            return cliente;
        }


        // LLAMADAS A LAS GESTIONES ESPECÍFICAS
        public static void ModificarCliente(Cliente c)
        {
            GestionModificarCliente.ModificarCliente(c);
        }

        public static void ModificarUsuario(Usuario u)
        {
            GestionModificarUsuario.ModificarUsuario(u);
        }

        public static void DarDeBajaUsuario(Usuario u)
        {
            GestionBajaUsuario.BajaUsuario(u);
        }
    }
}

