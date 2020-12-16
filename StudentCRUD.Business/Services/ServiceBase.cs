using Core.Common.Data;
using Core.Common.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCRUD.Business.Services {
    public abstract class ServiceBase<RType,TEntity,KType> where RType:class, IRepository<TEntity,KType> where TEntity:class {
        protected readonly RType _baseRepository;

        protected ServiceBase(RType repository) {
            _baseRepository = repository;
        }

		public TEntity Get(KType id) {
			return _baseRepository.Get(id);
		}
		public IEnumerable<TEntity> Get(IEnumerable<KType> ids) {
			return _baseRepository.Get(ids);
		}
		public IEnumerable<TEntity> GetAll() {
			return _baseRepository.GetAll();
		}
		public Page<IEnumerable<TEntity>> GetAll(int pageIndex, int pageSize) {
			long total = 0;
			var result = _baseRepository.GetAll(pageIndex, pageSize, ref total);
			return new Page<IEnumerable<TEntity>> {
				Result = result,
				Total = total,
				CurrentPage = pageIndex,
				PageSize = pageSize
			};
		}
		public TEntity Add(TEntity entity) {
			return _baseRepository.Add(entity);
		}
		public void AddMany(IEnumerable<TEntity> entities) {
			_baseRepository.AddMany(entities);
		}
		public void Update(TEntity entity) {
			_baseRepository.Update(entity);
		}
		public void Remove(TEntity entity) {
			_baseRepository.Remove(entity);
		}
		public void Remove(KType id) {
			_baseRepository.Remove(id);
		}
		public void RemoveMany(IEnumerable<TEntity> entities) {
			_baseRepository.RemoveMany(entities);
		}


	}
}
