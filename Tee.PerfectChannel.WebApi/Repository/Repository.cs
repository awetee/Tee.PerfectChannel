using Tee.PerfectChannel.WebApi.Entities;

namespace Tee.PerfectChannel.WebApi.Repository
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using Tee.PerfectChannel.WebApi.Extensions;

    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(DbContext ctx)
        {
            this.context = ctx;
            this.dbSet = this.context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return this.dbSet.ToList();
        }

        public T Get(int id)
        {
            return this.dbSet.Find(id);
        }

        public int Insert(T entity)
        {
            Guard.AgainstNull(entity, "Inserting entity");
            ValidateIsNotExistingEntity(entity);

            this.dbSet.Add(entity);
            this.context.SaveChanges();
            return entity.Id;
        }

        private void ValidateIsNotExistingEntity(T entity)
        {
            if (entity.Id != 0)
            {
                throw new InvalidDataException("Existing Data");
            }
        }

        public void Update(T entity)
        {
            Guard.AgainstNull(entity, "Updating entity");
            ValidateIsExistingEntity(entity);

            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void Delete(T entity)
        {
            Guard.AgainstNull(entity, "Deleting entity");
            ValidateIsExistingEntity(entity);

            this.context.Entry(entity).State = EntityState.Deleted;
            this.context.SaveChanges();
        }

        private void ValidateIsExistingEntity(T entity)
        {
            if (entity.Id == 0)
            {
                throw new InvalidDataException("New Data");
            }
        }
    }
}