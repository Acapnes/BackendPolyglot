using System.Collections.Generic;

namespace API_MSSQL.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();

        void Update(T entity);
    }
}
