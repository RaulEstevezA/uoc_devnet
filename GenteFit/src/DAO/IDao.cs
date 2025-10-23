namespace GenteFit.DAO
{
    public interface IDao { }  // Constructor necesario para el FactoryDAO

    public interface IDao<T> where T : class
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();
        void Save(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}