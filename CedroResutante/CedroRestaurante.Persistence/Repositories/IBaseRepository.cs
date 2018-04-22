using System;
using System.Linq;

namespace CedroRestaurante.Persistence.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get();
        IQueryable<T> Get(Func<T, bool> predicate);
        T Find(params object[] key);
        void Update(T obj);
        void SaveAll();
        void Add(T obj);
        void Delete(T obj);
    }
}
