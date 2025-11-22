using System.Collections.Generic;
using System.Linq;
using GenteFit.src.DAO;
using GenteFit.src.model.entity;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionActividad
    {
        private static readonly ActividadDAO actividadDao = (ActividadDAO)FactoryDAO.GetActividadDAO();

        // get all
        public static List<Actividad> GetAll()
        {
            return actividadDao.GetAll().ToList();
        }

        // get by id
        public static Actividad? BuscarPorId(int id)
        {
            return actividadDao.GetAll().FirstOrDefault(a => a.Id == id);
        }

        // crear actividad
        public static void CrearActividad(string nombre, int duracion, int plazas)
        {
            var nueva = new Actividad
            {
                Nombre = nombre,
                DuracionMin = duracion,
                PlazasMax = plazas
            };

            actividadDao.Save(nueva);
        }

        // modificar
        public static void ModificarActividad(Actividad actividad)
        {
            actividadDao.Update(actividad);
        }

        // dar de baja o eliminar
        // tu base de datos no tiene campo "activo", asi que borramos de verdad
        public static void EliminarActividad(Actividad actividad)
        {
            actividadDao.Delete(actividad);
        }
    }
}
