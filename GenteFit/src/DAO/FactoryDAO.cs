using GenteFit.src.model.entity;

namespace GenteFit.src.DAO
{
    public static class FactoryDAO
    {
        // devuelve instancias de los daos concretos
        public static IDao<Sala> GetSalaDAO() => new SalaDAO();
        public static IDao<Actividad> GetActividadDAO() => new ActividadDAO();
        public static IDao<Instructor> GetInstructorDAO() => new InstructorDAO();
        public static IDao<Cliente> GetClienteDAO() => new ClienteDAO();
        public static IDao<Usuario> GetUsuarioDAO() => new UsuarioDAO();
        public static IDao<Reserva> GetReservaDAO() => new ReservaDAO();
        public static IDao<Sesion> GetSesionDAO() => new SesionDAO();
        public static IDao<ReservaCancelada> GetReservaCanceladaDAO() => new ReservaCanceladaDAO();

    }
}
