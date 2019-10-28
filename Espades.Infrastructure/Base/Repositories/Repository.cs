using Espades.Domain.Entities.Base;
using Espades.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Espades.Infrastructure.Base.Repositories
{
    public class Repository : IRepository
    {
        #region Properties
        private EspadesContext _context { get; set; }

        public Repository(EspadesContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public async void Add<T>(T entity) where T : BaseEntity
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Edit<T>(T entity) where T : BaseEntity
        {
            Edit(entity.Id, entity).Wait();
        }

        public async Task Edit<T>(object id, T entity) where T : BaseEntity
        {
            var oldEntity = await Get<T>(id);
            _context.Entry(oldEntity).State = EntityState.Modified;
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public async Task Delete<T>(int id) where T : BaseEntity
        {
            Delete(await Get<T>(id));
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task<int> Count<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return await _context.Set<T>().Where(expression).CountAsync();
        }

        public async Task<bool> Exists<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task<T> First<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return await _context.Set<T>().Where(x => !x.Deleted).FirstOrDefaultAsync(expression);
        }

        public async Task<T> First<T>(Expression<Func<T, bool>> expression, bool noTracking = false) where T : BaseEntity
        {
            if (noTracking)
                return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
            else
                return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<T> Get<T>(object id) where T : BaseEntity
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> Get<T>(bool noTracking = false) where T : BaseEntity
        {
            if (noTracking)
                return _context.Set<T>().Where(x => !x.Deleted).AsNoTracking();
            else
                return _context.Set<T>().Where(x => !x.Deleted);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, bool noTracking = false) where T : BaseEntity
        {
            if (noTracking)
                return _context.Set<T>().Where(expression).AsNoTracking();
            else
                return _context.Set<T>().Where(expression);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, int skip, int take, params string[] includes) where T : BaseEntity
        {
            var query = _context.Set<T>().Where(x => !x.Deleted).AsQueryable();

            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include).AsQueryable();
                }
            }

            return query.Where(expression).Skip(skip).Take(take);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, params string[] includes) where T : BaseEntity
        {
            var query = _context.Set<T>().AsQueryable();

            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include).AsQueryable();
                }
            }

            return query.Where(expression);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, bool noTracking = false, params string[] includes) where T : BaseEntity
        {
            if (noTracking)
            {
                var query = _context.Set<T>().Where(x => !x.Deleted).AsNoTracking().AsQueryable();
                query = MakeIncludes<T>(query, includes);
                return query.Where(expression);
            }
            else
            {
                var query = _context.Set<T>().Where(x => !x.Deleted).AsQueryable();
                query = MakeIncludes<T>(query, includes);
                return query.Where(expression);
            }
        }

        public IQueryable<T> GetWithDeleteds<T>(Expression<Func<T, bool>> expression, bool noTracking = false, params string[] includes) where T : BaseEntity
        {
            if (noTracking)
            {
                var query = _context.Set<T>().AsNoTracking().AsQueryable();
                query = MakeIncludes<T>(query, includes);
                return query.Where(expression);
            }
            else
            {
                var query = _context.Set<T>().AsQueryable();
                query = MakeIncludes<T>(query, includes);
                return query.Where(expression);
            }
        }

        private IQueryable<T> MakeIncludes<T>(IQueryable<T> query, string[] includes) where T : BaseEntity
        {
            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include).AsQueryable();
                }
            }

            return query;
        }
        #endregion
    }
}
