using System.Collections.Generic;

namespace CedroRestaurante.ApplicationService.Services
{
    public interface IService<T> where T : class
    {
        List<T> Get();
        T Get(string id);
        T Add(T entity);
        T Update(string id, T entity);
        bool Delete(string id);
    }
}
