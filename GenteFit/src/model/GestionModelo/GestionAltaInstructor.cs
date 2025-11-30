using GenteFit.src.DAO;
using GenteFit.src.model.entity;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionAltaInstructor
    {
        public static void CrearInstructor(
            string username,
            string emailUsuario,
            string password,
            string nombre,
            string apellido1,
            string apellido2
        )
        {
            // DAO necesarios
            var usuarioDao = (UsuarioDAO)FactoryDAO.GetUsuarioDAO();
            var instructorDao = FactoryDAO.GetInstructorDAO();

            // 1. crear usuario con rol INSTRUCTOR (id 5)
            GestionAltaUsuario.CrearUsuario(
                username,
                emailUsuario,
                password,
                tipoRolId: 5
            );

            // 2. recuperar el usuario recién creado
            var usuarios = usuarioDao.GetAll();
            var usuarioCreado = usuarios.FirstOrDefault(u => u.Username == username);

            if (usuarioCreado is null)
                throw new Exception("No se pudo recuperar el usuario tras crearlo.");

            // 3. crear instructor con el mismo ID del usuario
            Instructor instructor = new Instructor
            {
                Id = usuarioCreado.Id,
                Nombre = nombre,
                Apellido1 = apellido1,
                Apellido2 = apellido2
            };

            // 4. guardar instructor
            instructorDao.Save(instructor);
        }
    }
}
