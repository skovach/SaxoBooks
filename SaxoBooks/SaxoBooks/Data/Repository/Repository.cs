using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SaxoBooks.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private SaxoBooksContext Context;
        protected DbSet<TEntity> Set { get; }

        public Repository(SaxoBooksContext context)
        {
            Context = context;
            Set = context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            Set.Add(entity);
        }

        public void AddOrUpdate(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Set.AddOrUpdate(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Set.Attach(entity);
            }
            Set.Remove(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            Set.AddRange(entities);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var entity in entities)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    Set.Attach(entity);
                }
            }

            Set.RemoveRange(entities);
        }

        public IQueryable<TEntity> Query()
        {
            return Set.AsQueryable();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}