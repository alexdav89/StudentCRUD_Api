using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCRUD.Business.Services;
using Core.Common.Misc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentCRUD.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CrudControllerBase<TEntity,KType> : BaseController where TEntity : class
    {
        private readonly IServiceBase<TEntity, KType> _baseService;

        public CrudControllerBase(IServiceBase<TEntity,KType> baseService) {
            _baseService = baseService;
        }

        /// <summary>
        /// Gets a single entity given its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("get-one/{id}")]
        [HttpGet]
        public TEntity Get(KType id) {
            return _baseService.Get(id);
        }

        /// <summary>
        /// Gets a list of entities given a specific set of Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Route("get-list")]
        [HttpPost]
        public IEnumerable<TEntity> Get(IEnumerable<KType> ids) {
            return _baseService.Get(ids);
        }

        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns></returns>
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<TEntity> GetAll() {
            var result =  _baseService.GetAll();
            return result;
        }

        /// <summary>
        /// Gets a page of all entities
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("get-all/{pageIndex}/{pageSize}")]
        [HttpGet]
        public Page<IEnumerable<TEntity>> GetAll(int pageIndex, int pageSize) {
            return _baseService.GetAll(pageIndex, pageSize);
        }

        /// <summary>
        /// Adds a new single entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Route("add-one")]
        [HttpPost]
        public TEntity Add(TEntity entity) {
            return _baseService.Add(entity);
        }

        /// <summary>
        /// Bulk adds a new set of entities
        /// </summary>
        /// <param name="entities"></param>
        [Route("add-list")]
        [HttpPost]
        public void AddMany(IEnumerable<TEntity> entities) {
            _baseService.AddMany(entities);
        }

        /// <summary>
        /// Updates the given entity
        /// </summary>
        /// <param name="entity"></param>
        [Route("update-one")]
        [HttpPut]
        public void Update(TEntity entity) {
            _baseService.Update(entity);
        }

        /// <summary>
        /// Deletes an entity given its Id
        /// </summary>
        /// <param name="id"></param>
        [Route("delete-one")]
        [HttpDelete]
        public void Remove(KType id) {
            _baseService.Remove(id);
        }       
    }
}