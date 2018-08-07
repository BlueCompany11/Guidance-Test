using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.DataAccessLayer
{
    interface IRepository<T>
    {
        int Add(T entity);
        Task<int> AddAsync(T entity);
        int AddRange(IList<T> entities);
        Task<int> AddRangeAsync(IList<T> entities);
        int Save(T entity);
        Task<int> SaveAsync(T entity);
        int Delete(T entity);
        Task<int> DeleteAsync(T entity);
        T GetOne(object primaryKey);
        Task<T> GetOneAsync(object primaryKey);
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        //IQueryable<T> GetEntities();
    }
}
