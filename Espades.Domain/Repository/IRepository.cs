using Espades.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Espades.Domain.Repository
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : BaseEntity;
        void Edit<T>(T entity) where T : BaseEntity;
        Task Edit<T>(object id, T entity) where T : BaseEntity;
        Task Delete<T>(int id) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        Task<T> Get<T>(object id) where T : BaseEntity;
        IQueryable<T> Get<T>(bool noTracking = false) where T : BaseEntity;
        IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, bool noTracking = false) where T : BaseEntity;
        IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, bool noTracking = false, params string[] includes) where T : BaseEntity;
        IQueryable<T> GetWithDeleteds<T>(Expression<Func<T, bool>> expression, bool noTracking = false, params string[] includes) where T : BaseEntity;
        IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, params string[] includes) where T : BaseEntity;
        IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, int skip, int take, params string[] includes) where T : BaseEntity;
        Task<T> First<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
        Task<T> First<T>(Expression<Func<T, bool>> expression, bool noTracking = false) where T : BaseEntity;
        Task<bool> Exists<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
        Task<int> Count<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
    }
}
