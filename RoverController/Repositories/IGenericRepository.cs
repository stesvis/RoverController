using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RoverController.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region Get

        TEntity Get(object id);

        IQueryable<TEntity> GetAll();

        TEntity GetFull(object id);

        IQueryable<TEntity> Find(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        #endregion Get

        #region Add

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        #endregion Add

        #region Update

        void Update(TEntity entityToUpdate);

        #endregion Update

        #region Delete

        void HardDelete(object id);

        void HardDelete(TEntity entity);

        void HardDeleteRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        //void DeleteRange(IEnumerable<TEntity> entities);

        #endregion Delete

        void Map(object id, object entityToUpdate);
    }
}