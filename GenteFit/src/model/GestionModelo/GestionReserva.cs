using System;
using System.Collections.Generic;
using System.Linq;
using GenteFit.src.DAO;
using GenteFit.src.model.entity;
using GenteFit.src.model.enums;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionReserva
    {
        private static readonly ReservaDAO reservaDao = new ReservaDAO();

        // Obtener todas las reservas de una sesión ordenadas por posición
        private static List<Reserva> ObtenerReservasOrdenadas(int sesionId)
        {
            return reservaDao.GetAll()
                .Where(r => r.SesionId == sesionId && r.EstadoReserva != TipoEstado.Cancelada)
                .OrderBy(r => r.PosicionEspera ?? int.MaxValue)
                .ToList();
        }

        // Cancelar reserva por ID
        public static void CancelarReserva(int reservaId)
        {
            Reserva? reserva = reservaDao.GetById(reservaId);
            if (reserva == null)
                throw new Exception("La reserva no existe.");

            int sesionId = reserva.SesionId;

            // Marcar como cancelada sin borrarla
            reserva.EstadoReserva = TipoEstado.Cancelada;
            reserva.PosicionEspera = null;
            reservaDao.Update(reserva);

            // Reordenar la cola completa de la sesión
            ReordenarCola(sesionId);
        }


        // Reordena todas las posiciones y estados tras una cancelación
        private static void ReordenarCola(int sesionId)
        {
            var reservas = ObtenerReservasOrdenadas(sesionId);

            for (int i = 0; i < reservas.Count; i++)
            {
                var r = reservas[i];

                int nuevaPos = i + 1; // Comienza en 1

                // Estado según nueva posición
                if (nuevaPos <= 16)
                {
                    r.EstadoReserva = TipoEstado.Reservada;
                    r.PosicionEspera = nuevaPos;
                }
                else
                {
                    r.EstadoReserva = TipoEstado.EnEspera;
                    r.PosicionEspera = nuevaPos;
                }

                reservaDao.Update(r);
            }
        }
    }
}
