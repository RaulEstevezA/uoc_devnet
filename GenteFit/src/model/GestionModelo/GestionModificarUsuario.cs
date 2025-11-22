using GenteFit.src.DAO;
using GenteFit.src.model.entity;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionModificarUsuario
    {
        public static void ModificarUsuario(Usuario usuario)
        {
            var usuarioDao = FactoryDAO.GetUsuarioDAO();
            usuarioDao.Update(usuario);
        }
    }
}
