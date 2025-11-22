using GenteFit.src.DAO;
using GenteFit.src.model.entity;
using GenteFit.src.model.enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenteFit.src.model.GestionModelo
{
    public class DetalleSesionDTO
    {
        public string? Actividad { get; set; }
        public string? Instructor { get; set; }
        public string? Sala { get; set; }
        public string? Horario { get; set; }
        public int Plazas { get; set; }
        public int ReservasConfirmadas { get; set; }
        public int EnEspera { get; set; }
    }


    public static class GestionDetalleReserva
    {
        private static readonly ActividadDAO actividadDao = (ActividadDAO)FactoryDAO.GetActividadDAO();
        private static readonly InstructorDAO instructorDao = (InstructorDAO)FactoryDAO.GetInstructorDAO();
        private static readonly SalaDAO salaDao = (SalaDAO)FactoryDAO.GetSalaDAO();
        private static readonly ReservaDAO reservaDao = (ReservaDAO)FactoryDAO.GetReservaDAO();

        public static DetalleSesionDTO ConstruirDetalle(Sesion sesion)
        {
            var actividad = actividadDao.GetAll().FirstOrDefault(a => a.Id == sesion.ActividadId);
            var instructor = instructorDao.GetAll().FirstOrDefault(i => i.Id == sesion.InstructorId);
            var sala = salaDao.GetById(sesion.SalaId);

            // Obtener reservas de la sesión
            var reservasSesion = reservaDao
                .GetAll()
                .Where(r => r.SesionId == sesion.Id)
                .ToList();

            // Capacidad fija
            int capacidad = 16;

            // Confirmadas = Reservadas
            int confirmadas = reservasSesion
                .Count(r => r.EstadoReserva == TipoEstado.Reservada);

            // En espera = estado enEspera
            int enEspera = reservasSesion
                .Count(r => r.EstadoReserva == TipoEstado.EnEspera);

            return new DetalleSesionDTO
            {
                Actividad = actividad?.Nombre ?? "Sin datos",
                Instructor = instructor?.NombreCompleto ?? "Sin datos",
                Sala = sala?.Nombre ?? "Sin datos",
                Horario = $"{sesion.FechaInicio:dd/MM HH:mm} - {sesion.FechaFin:HH:mm}",
                Plazas = capacidad,
                ReservasConfirmadas = confirmadas,
                EnEspera = enEspera
            };
        }
    }

}

