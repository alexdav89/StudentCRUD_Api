using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Common.Data
{
    public interface IRepository<TEntity,KType> where TEntity : class
    {
        TEntity Get(KType id);
        IEnumerable<TEntity> Get(IEnumerable<KType> ids);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(int pageIndex, int pageSize, ref long total);
        TEntity Add(TEntity entity);
        void AddMany(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(KType id);
        void RemoveMany(IEnumerable<TEntity> entities);
    }

    
}