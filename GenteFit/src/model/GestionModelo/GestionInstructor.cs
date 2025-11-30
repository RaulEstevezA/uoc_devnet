using System.Collections.Generic;
using System.Linq;
using GenteFit.src.DAO;
using GenteFit.src.model.entity;

namespace GenteFit.src.model.GestionModelo
{
    public static class GestionInstructor
    {
        private static readonly InstructorDAO instructorDao = new InstructorDAO();

        // devolver instructores con Nombre + Apellido ya formateados
        public static IEnumerable<Instructor> ObtenerInstructores()
        {
            return instructorDao.GetAll()
                .Select(i =>
                {
                    // clonamos el registro original
                    var inst = new Instructor
                    {
                        Id = i.Id,
                        Nombre = $"{i.Nombre} {i.Apellido1}", // <-- nombre formateado
                        Apellido1 = i.Apellido1,
                        Apellido2 = i.Apellido2
                    };

                    return inst;
                })
                .ToList();
        }

        public static Instructor? BuscarPorId(int id)
        {
            return ObtenerInstructores().FirstOrDefault(i => i.Id == id);
        }
    }
}


