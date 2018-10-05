using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestBackendDev.DAL.Interfaces
{
    public interface IRepository<TModel>
    {
        Task<TModel> GetAsync(Expression<Func<TModel, bool>> expression, Func<IQueryable<TModel>, IQueryable<TModel>> include);
        Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression, Func<IQueryable<TModel>, IQueryable<TModel>> include);
        /// <summary>
        /// Inserts model instance to database, returns instance id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TModel> InsertAsync(TModel model);
        /// <summary>
        /// Updates all fields of model after Save() method is called
        /// </summary>
        /// <param name="model"></param>
        void Update(TModel model);
        void Delete(TModel model);
        Task<int> SaveAsync();
    }
}