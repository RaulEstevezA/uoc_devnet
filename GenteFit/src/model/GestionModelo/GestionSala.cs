using GenteFit.src.DAO;
using GenteFit.src.model.entity;

namespace GenteFit.src.model.GestionModelo
{
    public class GestionSala
    {
        // Crear (Agregar)
        public static void AgregarSala(Sala sala)
        {
            var salaDao = FactoryDAO.GetSalaDAO();
            salaDao.Save(sala);
        }

        // Leer (Obtener todas)
        public static IEnumerable<Sala> ObtenerSalas()
        {
            var salaDao = FactoryDAO.GetSalaDAO();
            return salaDao.GetAll();
        }

        // Leer (Obtener por Id)
        public static Sala? ObtenerSalaPorId(int id)
        {
            var salaDao = FactoryDAO.GetSalaDAO();
            return salaDao.GetById(id);
        }

        // Actualizar
        public static void ActualizarSala(Sala sala)
        {
            var salaDao = FactoryDAO.GetSalaDAO();
            salaDao.Update(sala);
        }

        // Eliminar
        public static void EliminarSala(Sala sala)
        {
            var salaDao = FactoryDAO.GetSalaDAO();
            salaDao.Delete(sala);
        }
    }
}