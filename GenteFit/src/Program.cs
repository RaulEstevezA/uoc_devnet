using GenteFit.src.model.GestionModelo;


namespace GenteFit.src
{
    class Program
    {

        static void Main()
        {
            // Ejemplo de uso de GestionSala para obtener y mostrar todas las salas
            // Este metodo será llamado por el controlador de la vista correspondiente
            var salas = GestionSala.ObtenerSalas();

            foreach (var sala in salas)
            {
                Console.WriteLine(sala);
            }

            //Ejemplo para agregar una nueva sala
            var nuevaSala = new model.entity.Sala
            {
                Nombre = "Sala CrossFit",
                AforoMax = 20,
                Disponible = true
            };

            Console.WriteLine("\nAgregando nueva sala:");
            Console.WriteLine(nuevaSala);   // El ID se asignará al guardar en la base de datos

            GestionSala.AgregarSala(nuevaSala);


            // Recuperar la sala con Id 3 que el la última agrgregada a la BBDD
            var salaRecuperada = GestionSala.ObtenerSalaPorId(3);
            Console.WriteLine("\nSala recuperada por ID:");
            Console.WriteLine(salaRecuperada);



            Console.WriteLine("\nModificando AforoMax de la sala con Id 3 a 16.");

            // Modificar campo AforoMax de la sala con Id 3
            salaRecuperada!.AforoMax = 16;

        }


    }
}