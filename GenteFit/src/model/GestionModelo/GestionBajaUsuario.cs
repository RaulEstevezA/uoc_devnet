using GenteFit.src.DAO;
using GenteFit.src.model.entity;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionBajaUsuario
    {
        public static void BajaUsuario(Usuario usuario)
        {
            var usuarioDao = FactoryDAO.GetUsuarioDAO();

            usuario.Activo = false;    // marcamos inactivo

            usuarioDao.Update(usuario);  // update lógico
        }
    }
}
