using System.Collections.Generic;

namespace GenteFit.Domain.DAO
{
    public interface IDao<T> where T : class
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();
        void Save(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}