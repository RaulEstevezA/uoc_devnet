using System;
using System.Collections.Generic;
using GenteFit.src.DAO;
using GenteFit.src.model.entity;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionSesion
    {
        private static SesionDAO GetDao()
        {
            return (SesionDAO)FactoryDAO.GetSesionDAO();
        }

        // obtener todas
        public static IEnumerable<Sesion> ObtenerSesiones()
        {
            return GetDao().GetAll();
        }

        // obtener por id
        public static Sesion? ObtenerSesionPorId(int id)
        {
            return GetDao().GetById(id);
        }

        // obtener sesiones por fecha exacta (opcional)
        public static IEnumerable<Sesion> ObtenerSesionesPorFecha(DateTime fecha)
        {
            return GetDao().GetByDate(fecha);
        }

        // crear sesion
        public static void CrearSesion(Sesion sesion)
        {
            GetDao().Save(sesion);
        }

        // actualizar sesion
        public static void ModificarSesion(Sesion sesion)
        {
            GetDao().Update(sesion);
        }

        // eliminar sesion
        public static void EliminarSesion(Sesion sesion)
        {
            GetDao().Delete(sesion);
        }
    }
}
