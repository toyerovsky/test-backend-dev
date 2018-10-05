using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestBackendDev.DAL.Interfaces;

namespace TestBackendDev.DAL.Repositories
{
    public class BaseRepository<TContext, TModel> : IRepository<TModel>
        where TContext : DbContext
        where TModel : class, IEntity
    {
        protected readonly TContext Context;

        protected BaseRepository(TContext context)
        {
            Context = context ?? throw new ArgumentException(nameof(Context));
        }

        protected IQueryable<TModel> PrepareResults(IQueryable<TModel> result, Func<IQueryable<TModel>, IQueryable<TModel>> include = null)
        {
            if (include != null)
            {
                result = include(result);
            }

            return result;
        }

        public virtual async Task<TModel> GetAsync(Expression<Func<TModel, bool>> expression,
            Func<IQueryable<TModel>, IQueryable<TModel>> include = null)
        {
            IQueryable<TModel> result = expression != null ?
                Context.Set<TModel>().Where(expression) :
                Context.Set<TModel>();

            return await PrepareResults(result, include).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression,
            Func<IQueryable<TModel>, IQueryable<TModel>> include = null)
        {
            IQueryable<TModel> result = expression != null ?
                Context.Set<TModel>().Where(expression) :
                Context.Set<TModel>();

            return await PrepareResults(result, include).ToListAsync();
        }

        public virtual async Task<TModel> InsertAsync(TModel model)
        {
            await Context.Set<TModel>().AddAsync(model);
            return model;
        }

        public virtual void Update(TModel model)
        {
            Context.Set<TModel>().Update(model);
        }

        public virtual void Delete(TModel model)
        {
            Context.Set<TModel>().Remove(model);
        }

        public virtual async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}