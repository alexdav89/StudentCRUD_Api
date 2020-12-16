using Core.Common.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCRUD.Business.Services {
    public interface IServiceBase<TEntity,KType> {
        TEntity Get(KType id);
        IEnumerable<TEntity> Get(IEnumerable<KType> ids);
        IEnumerable<TEntity> GetAll();
        Page<IEnumerable<TEntity>> GetAll(int pageIndex, int pageSize);
        TEntity Add(TEntity entity);
        void AddMany(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(KType id);
        void RemoveMany(IEnumerable<TEntity> entities);
    }
}
