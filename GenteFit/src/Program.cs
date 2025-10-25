using GenteFit.src.DAO;


namespace GenteFit.src
{
    class Program
    {

        static void Main()
        {
            var salaDao = FactoryDAO.GetSalaDAO();
            var salas = salaDao.GetAll();

            foreach (var sala in salas)
            {
                Console.WriteLine(sala);
            }
        }
        
        
    }
}