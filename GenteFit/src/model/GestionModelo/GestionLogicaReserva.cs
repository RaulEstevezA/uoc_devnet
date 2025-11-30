using System;
using System.Linq;
using GenteFit.src.DAO;
using GenteFit.src.model.entity;
using GenteFit.src.model.enums;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionLogicaReserva
    {
        private static readonly ReservaDAO reservaDao =
            (ReservaDAO)FactoryDAO.GetReservaDAO();

        private static readonly ReservaCanceladaDAO reservaCanceladaDao =
            (ReservaCanceladaDAO)FactoryDAO.GetReservaCanceladaDAO();

        private const int CAPACIDAD_MAX = 16;

        // 🟩 Crear nueva reserva o lista de espera
        public static string Reservar(int clienteId, int sesionId)
        {
            // todas las reservas de esta sesión
            var reservasSesion = reservaDao.GetAll()
                                           .Where(r => r.SesionId == sesionId)
                                           .OrderBy(r => r.PosicionEspera ?? int.MaxValue)
                                           .ToList();

            // siguiente posición en la cola
            int nuevaPos = reservasSesion.Count + 1;

            var nuevaReserva = new Reserva
            {
                ClienteId = clienteId,
                SesionId = sesionId,
                FechaReserva = DateTime.Now,
                PosicionEspera = nuevaPos,
                EstadoReserva = (nuevaPos <= CAPACIDAD_MAX)
                                ? TipoEstado.Reservada
                                : TipoEstado.EnEspera
            };

            reservaDao.Save(nuevaReserva);

            if (nuevaPos <= CAPACIDAD_MAX)
                return "Reserva confirmada. Tienes plaza en la sesión.";
            else
                return $"Añadido a la lista de espera. Posición: {nuevaPos - CAPACIDAD_MAX}.";
        }

        // 🟥 Cancelar reserva + pasar a ReservaCancelada + reordenar cola
        public static string Cancelar(int reservaId)
        {
            var reserva = reservaDao.GetById(reservaId);
            if (reserva == null)
                return "No se ha encontrado la reserva a cancelar.";

            // 1️⃣ Guardar en histórico
            var historico = new ReservaCancelada
            {
                ClienteId = reserva.ClienteId,
                SesionId = reserva.SesionId,
                FechaCancelacion = DateTime.Now,
                Motivo = "Cancelado por usuario",
                PosicionEnCancelacion = reserva.PosicionEspera,
                EstadoPrevio = reserva.EstadoReserva.ToString()
            };

            // OJO: aquí usamos Save, que es lo que tienes en tu DAO
            reservaCanceladaDao.Save(historico);

            // 2️⃣ Eliminar de Reserva
            reservaDao.Delete(reserva);

            // 3️⃣ Reordenar posiciones y estados de los que quedan
            var restantes = reservaDao.GetAll()
                                      .Where(r => r.SesionId == reserva.SesionId)
                                      .OrderBy(r => r.PosicionEspera)
                                      .ToList();

            int pos = 1;
            foreach (var r in restantes)
            {
                r.PosicionEspera = pos;
                r.EstadoReserva = (pos <= CAPACIDAD_MAX)
                                  ? TipoEstado.Reservada
                                  : TipoEstado.EnEspera;

                reservaDao.Update(r);
                pos++;
            }

            return "Reserva cancelada correctamente.";
        }
    }
}



