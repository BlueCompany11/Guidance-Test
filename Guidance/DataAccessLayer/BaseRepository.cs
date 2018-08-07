using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.DataAccessLayer
{
    public abstract class BaseRepository<T>: IDisposable, IRepository<T> where T:class,new()
    {
        public FlashCardsEntities Context { get; } = new FlashCardsEntities();
        protected DbSet<T> Table;

        internal int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
        }

        internal async Task<int> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T GetOne(object primaryKey)
        {
            return Table.Find(primaryKey);
        }
        public Task<T> GetOneAsync(object primaryKey)
        {
            return Table.FindAsync(primaryKey);
        }

        public List<T> GetAll()
        {
            return Table.ToList();
        }

        public Task<List<T>> GetAllAsync() => Table.ToListAsync();

        public int Add(T entity)
        {
            Table.Add(entity);
            return SaveChanges();
        }
        public Task<int> AddAsync(T entity)
        {
            Table.Add(entity);
            return SaveChangesAsync();
        }

        public int AddRange(IList<T> entities)
        {
            Table.AddRange(entities);
            return SaveChanges();
        }

        public Task<int> AddRangeAsync(IList<T> entities)
        {
            Table.AddRange(entities);
            return SaveChangesAsync();
        }
        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                Context.Dispose();
            }
            disposed = true;
        }
        public int Save(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }
        public Task<int> SaveAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return SaveChangesAsync();
        }
        public int Delete(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public Task<int> DeleteAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return SaveChangesAsync();
        }
    }
}
