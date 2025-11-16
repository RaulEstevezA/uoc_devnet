using System.Collections.Generic;
using System.Linq;
using GenteFit.src.DAO;
using GenteFit.src.model.entity;
using GenteFit.src.model.enums;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionMisReservas
    {
        private static readonly ReservaDAO reservaDao = (ReservaDAO)FactoryDAO.GetReservaDAO();

        // Obtiene reservas activas o en espera ordenadas por fecha de reserva
        public static IEnumerable<Reserva> ObtenerReservasActivasOrdenadas(int clienteId)
        {
            return reservaDao.GetAll()
                             .Where(r => r.ClienteId == clienteId &&
                                    (r.EstadoReserva == TipoEstado.Reservada || r.EstadoReserva == TipoEstado.EnEspera))
                             .OrderBy(r => r.FechaReserva)
                             .ToList();
        }

        // Nuevo método requerido: obtener reserva concreta usuario-sesión
        public static Reserva? ObtenerReservaPorUsuarioYSesion(int clienteId, int sesionId)
        {
            return reservaDao.GetAll()
                             .FirstOrDefault(r => r.ClienteId == clienteId && r.SesionId == sesionId);
        }
    }
}


