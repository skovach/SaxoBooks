using System.Collections.Generic;
using System.Linq;

namespace SaxoBooks.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets a specifC entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        TEntity Get(int id);

        /// <summary>
        /// Returns IQuerable object for a specific entity
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Adds the or update.
        /// This method must be used carefully since it is making calls to the data base for every item we are adding.
        /// Use only if really necessary.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void AddOrUpdate(TEntity entity);
        
        /// <summary>
        /// Adds range of entities.
        /// </summary>
        /// <param name="entity">The entities collection.</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Removes range of entities.
        /// </summary>
        /// <param name="entity">The entities collection.</param>
        void RemoveRange(IEnumerable<TEntity> entities);

        void SaveChanges();
    }
}