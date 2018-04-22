using CedroRestaurante.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CedroRestaurante.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DataContext context;

        /// <summary>
        /// Inicia o repositorio base
        /// </summary>
        /// <param name="context">Contexto atual do banco de dados</param>
        public BaseRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return context.Set<T>();
        }

        public IQueryable<T> Get(Func<T, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public T Find(params object[] key)
        {
            return context.Set<T>().Find(key);
        }

        public void Update(T obj)
        {
            context.Entry(obj).State = EntityState.Modified;
        }

        public void SaveAll()
        {
            context.SaveChanges();
        }

        public void Add(T obj)
        {
            context.Set<T>().Add(obj);
        }

        public void Delete(T obj)
        {
            context.Entry(obj).State = EntityState.Deleted;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
