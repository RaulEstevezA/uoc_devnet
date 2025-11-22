using GenteFit.src.DAO;
using GenteFit.src.model.entity;
using System.Linq;
using System;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionLogin
    {
        private static readonly UsuarioDAO usuarioDao = (UsuarioDAO)FactoryDAO.GetUsuarioDAO();

        public static Usuario? ValidarLogin(string username, string password)
        {
            // Por ahora la contraseña se compara en texto plano
            return usuarioDao.GetAll()
                             .FirstOrDefault(u =>
                                    u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                                    && u.PasswordHash == password
                                    && u.Activo == true);
        }
    }
}


