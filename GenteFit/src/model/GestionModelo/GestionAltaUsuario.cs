using GenteFit.src.DAO;
using GenteFit.src.model.entity;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionAltaUsuario
    {
        public static void CrearUsuario(
            string username,
            string email,
            string password,
            int tipoRolId
        )
        {
            var usuarioDao = FactoryDAO.GetUsuarioDAO();

            Usuario usuario = new Usuario
            {
                Username = username,
                Email = email,
                PasswordHash = password,
                TipoRolId = tipoRolId,
                Activo = true
            };

            usuarioDao.Save(usuario);
        }
    }
}



