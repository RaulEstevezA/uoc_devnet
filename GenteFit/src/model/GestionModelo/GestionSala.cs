using System.Collections.Generic;
using GenteFit.src.DAO;
using GenteFit.src.model.entity;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionSala
    {
        private static SalaDAO GetDao()
        {
            return (SalaDAO)FactoryDAO.GetSalaDAO();
        }

        // listar todas las salas
        public static IEnumerable<Sala> ObtenerSalas()
        {
            return GetDao().GetAll();
        }

        // compatibilidad con codigo antiguo que llamaba GetAll()
        public static IEnumerable<Sala> GetAll()
        {
            return ObtenerSalas();
        }

        // obtener sala por id
        public static Sala? ObtenerSalaPorId(int id)
        {
            return GetDao().GetById(id);
        }

        // crear sala
        public static void AgregarSala(Sala sala)
        {
            GetDao().Save(sala);
        }

        // actualizar sala
        public static void ActualizarSala(Sala sala)
        {
            GetDao().Update(sala);
        }

        // eliminar sala fisicamente
        public static void EliminarSala(Sala sala)
        {
            GetDao().Delete(sala);
        }

        // dar de baja (marcar como no disponible)
        public static void DarDeBaja(Sala sala)
        {
            sala.Disponible = false;
            GetDao().Update(sala);
        }

        public static void AltaSala(Sala sala)
        {
            AgregarSala(sala);
        }

        public static Sala? BuscarPorId(int id)
        {
            return ObtenerSalaPorId(id);
        }

        public static void ModificarSala(Sala sala)
        {
            ActualizarSala(sala);
        }
    }
}

