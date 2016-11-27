using System.Collections.Generic;

namespace Tee.PerfectChannel.WebApi.Repository
{
    public interface IRepository<T>
    {
        int Insert(T item);

        IEnumerable<T> GetAll();

        T Get(int id);

        void Delete(T entity);

        void Update(T entity);
    }
}