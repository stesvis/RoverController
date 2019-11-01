using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RoverController.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal DbContext _context;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            this._context = context;
            _dbSet = context.Set<TEntity>();
        }

        #region Get

        public virtual IQueryable<TEntity> Find(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
        )
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public TEntity Get(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public virtual TEntity GetFull(object id)
        {
            return Get(id);
        }

        #endregion Get

        #region Add

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        #endregion Add

        #region Update

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _context.Entry(entity).State = EntityState.Modified;

            //var oldEntity = Get(entity);
            //var oldEntry = _context.Entry(oldEntity);
            //oldEntry.CurrentValues.SetValues(entity);
        }

        #endregion Update

        #region Delete

        public void HardDelete(object id)
        {
            var entity = Get(id);

            //if (_context.Entry(entity).State == EntityState.Detached)
            //{
            //    _dbSet.Attach(entity);
            //}
            //_context.Entry(entity).State = EntityState.Deleted;

            _dbSet.Remove(entity); // Hard-delete
        }

        public void HardDelete(TEntity entity)
        {
            //if (_context.Entry(entity).State == EntityState.Detached)
            //{
            //    _dbSet.Attach(entity);
            //}
            //_context.Entry(entity).State = EntityState.Deleted;

            _dbSet.Remove(entity); // Hard-delete
        }

        public void HardDeleteRange(IEnumerable<TEntity> entities)
        {
            //foreach (var entity in entities)
            //{
            //    if (_context.Entry(entity).State == EntityState.Detached)
            //    {
            //        _dbSet.Attach(entity);
            //    }
            //    _context.Entry(entity).State = EntityState.Deleted;
            //}

            _dbSet.RemoveRange(entities); // Hard-delete
        }

        /// <summary>
        /// </summary>
        /// <param name="entity">Expected to have Status="D"</param>
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            Update(entity);
        }

        public void Map(object id, object updatedEntity)
        {
            var oldEntity = Get(id);
            var oldEntry = _context.Entry(oldEntity);
            oldEntry.CurrentValues.SetValues(updatedEntity);
        }

        //public void DeleteRange(IEnumerable<TEntity> entities)
        //{
        //    DbSet.RemoveRange(entities);
        //}

        #endregion Delete
    }
}