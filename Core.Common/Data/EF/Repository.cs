using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.Common.Data.EF {
    public class Repository<TEntity,KType> : IRepository<TEntity,KType> where TEntity : class, IBaseEntity<KType> {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _entities;

        public Repository(DbContext context) {
            Context = context;
            this._entities = Context.Set<TEntity>();            
        }

        #region CRUD
        public TEntity Get(KType id) {
            return _entities.Find(id);
        }


        public IEnumerable<TEntity> Get(IEnumerable<KType> ids) {
            return _entities.Where(entity => ids.Contains(entity.Id)).ToList();
        }

        public IEnumerable<TEntity> GetAll() {
            return _entities.ToList();
        }

        public IEnumerable<TEntity> GetAll(int pageIndex, int pageSize, ref long total) {
            total = _entities.Count();
            return _entities.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
        }

        public TEntity Add(TEntity entity) {
            var newEntity = _entities.Add(entity);
            Context.SaveChanges();
            return newEntity.Entity;
        }

        public void Update(TEntity entity) {
            _entities.Update(entity);
            Context.SaveChanges();
        }

        public void AddMany(IEnumerable<TEntity> entities) {
            _entities.AddRange(entities);
            Context.SaveChanges();
        }

        public void Remove(TEntity entity) {
            _entities.Remove(entity);
            Context.SaveChanges();
        }

        public void Remove(KType id) {
            var entity = _entities.Find(id);
            _entities.Remove(entity);
            Context.SaveChanges();
        }

        public void RemoveMany(IEnumerable<TEntity> entities) {
            _entities.RemoveRange(entities);
            Context.SaveChanges();
        } 
        #endregion
    }
}
