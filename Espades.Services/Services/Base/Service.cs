using Espades.Common;
using Espades.Common.Containers;
using Espades.Common.Enumerators;
using Espades.Domain.Contracts.Services.Base;
using Espades.Domain.Contracts.UnitOfWork;
using Espades.Domain.Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Espades.Common.Helpers;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.Extensions.Primitives;

namespace Espades.Services.Base
{
    public class Service<T> : IService<T>
        where T : BaseEntity
    {
        #region Properties
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected IUnitOfWorkFactory UnitOfWorkFactory { get; set; }
        protected ILogger _logger;
        protected IStringLocalizer _localizer;
        protected IConfiguration _configuration;

        public long LoggedCompanyId { get; set; }
        public long LoggedUserId { get; set; }
        public long VirtualUserId { get; set; }
        public long LoggedOrVirtualUserId { get; set; }
        #endregion

        #region Ctor
        public Service(IServiceProvider provider)
        {
            _logger = provider.GetService(typeof(ILogger<Service<T>>)) as ILogger<Service<T>>;
            _localizer = provider.GetService(typeof(IStringLocalizer<LanguageLocalizer>)) as IStringLocalizer<LanguageLocalizer>;
            UnitOfWorkFactory = provider.GetService(typeof(IUnitOfWorkFactory)) as IUnitOfWorkFactory;
            _httpContextAccessor = provider.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
            _configuration = provider.GetService(typeof(IConfiguration)) as IConfiguration;
            SetContextVariables();
        }
        #endregion

        public virtual async Task<RequestResult> Save(T entity)
        {
            RequestResult result = new RequestResult(StatusResult.Success);

            BeforeSave(entity);
            BeforeValidate(entity, result);

            if (result.Status != StatusResult.Success)
            {
                return result;
            }

            Validate(entity, result);

            if (result.Status != StatusResult.Success)
            {
                return result;
            }

            await AfterValidate(entity, result);

            if (result.Status != StatusResult.Success)
            {
                return result;
            }

            using (IUnitOfWork unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
            {
                BeforeSaveWithTransaction(unitOfWork.GetTransaction(), entity);
                await unitOfWork.Commit();

                SetCompanyIfExists(ref entity);

                if (entity.Id == 0)
                {
                    unitOfWork.Repository.Add(entity);
                }
                else
                {
                    await unitOfWork.Repository.Edit(entity.Id, entity);
                }

                AfterSave(entity);
                BeforeCommit(entity);
                await unitOfWork.Commit();
                AfterCommit(entity);

                result.Data = entity;
                result.Messages.Add(new Message(_localizer["EntitySaveSuccess"]));
            }

            return result;
        }

        public virtual async Task<RequestResult> Delete(long id)
        {
            RequestResult result = new RequestResult();

            ValidateDelete(id, result);

            if (result.Status != StatusResult.Success)
            {
                return result;
            }

            using (IUnitOfWork unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
            {
                bool shouldInactive = await ShouldInactiveOnDelete(id, result);

                T oldObject = await unitOfWork.Repository.Get<T>(id);

                if (oldObject == null)
                {
                    result.Status = StatusResult.Danger;
                    result.Messages.Add(new Message(_localizer["EntityNotFound"]));
                    return result;
                }

                if (result.Status != StatusResult.Success)
                {
                    return result;
                }

                if (!shouldInactive)
                {
                    BeforeDelete(unitOfWork.GetTransaction(), id);
                    await unitOfWork.Repository.Delete<T>(id);
                }
                else
                {
                    oldObject.Deleted = true;
                    unitOfWork.Repository.Edit(oldObject);
                }

                await unitOfWork.Commit();
                AfterDelete(id);

                result.Messages.Add(new Message(_localizer["EntityDeleteSuccess"]));
            }

            return result;
        }

        public virtual async Task<RequestResult> Get(long id)
        {
            RequestResult result = new RequestResult(StatusResult.Success);

            using (IUnitOfWork unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
            {
                T obj = await unitOfWork.Repository
                                    .First<T>(x => !x.Deleted
                                                && x.Id == id);

                result.Data = await BeforeReturnGet(unitOfWork.GetTransaction(), obj);

                if (result.Data == null)
                {
                    result.Status = StatusResult.Warning;
                    result.Messages.Add(new Message(_localizer["EntityNotFound"]));
                }
            }

            return result;
        }

        public virtual async Task<RequestResult> GetAll()
        {
            RequestResult result = new RequestResult(StatusResult.Success);

            using (IUnitOfWork unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
            {
                List<T> entities = unitOfWork.Repository
                                    .Get(MakeDefaultFilter(), Includes())
                                    .ToList();



                List<T> resultEntities = await BeforeReturnGetAll(entities);

                result.Data = MakeOrder(resultEntities.AsQueryable());

                if (entities == null || entities.Count == 0)
                {
                    result.Status = StatusResult.Warning;
                    result.Messages.Add(new Message(_localizer["EntitiesNotFound"]));
                }
            }

            return result;
        }

        public virtual async Task<RequestResult> GetByFilter(FilterHelper filter)
        {
            RequestResult result = new RequestResult(StatusResult.Success);

            using (IUnitOfWork unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
            {
                FilterResult<T> filterResult = new FilterResult<T>();
                Expression<Func<T, bool>> exp = filter.GetExpression<T>();

                List<T> list = unitOfWork.Repository.Get(exp, true, filter.GetIncludes())
                                              .ToList()
                                              .Where(MakeDefaultFilter().Compile())
                                              .ToList();

                List<T> resultEntities = await BeforeReturnFilter(list);

                list = MakeOrder(resultEntities.AsQueryable()).ToList();

                filterResult.Count = list.Count();

                filterResult.List = list.OrderByFilter(filter)
                                        .Skip(filter.Skip)
                                        .Take(filter.Take)
                                        .ToList();

                if (filterResult.Count > 0 && (filterResult.List == null || filterResult.List.Count() == 0))
                {
                    filter.Page = 1;
                    filterResult.List = list.OrderByFilter(filter)
                                        .Skip(filter.Skip)
                                        .Take(filter.Take)
                                        .ToList();
                }

                if (filterResult.List == null || filterResult.List.Count() == 0)
                {
                    result.Status = StatusResult.Warning;
                    result.Messages.Add(new Message(_localizer["EntitiesNotFound"]));
                }
                else
                {
                    result.Data = filterResult;
                }
            }

            return result;
        }

        public virtual Task<List<T>> BeforeReturnFilter(List<T> list)
        {
            return Task.FromResult(list);
        }

        public virtual Task<List<T>> BeforeReturnGetAll(List<T> list)
        {
            return Task.FromResult(list);
        }

        #region Private methods
        private void SetContextVariables()
        {
            Claim claimUserId = null;
            Claim claimCompanyId = null;

            if (_httpContextAccessor.HttpContext?.User?.Identity is ClaimsIdentity claimsIdentity && claimsIdentity.Claims != null)
            {
                claimUserId = claimsIdentity?.Claims?.Where(x => x.Type == "userId")?.FirstOrDefault();
                claimCompanyId = claimsIdentity?.Claims?.Where(x => x.Type == "companyId")?.FirstOrDefault();
            }

            _httpContextAccessor.HttpContext?.Request?.Headers?.TryGetValue("VirtualUserId", out StringValues virtualUserId);

            if (!string.IsNullOrEmpty(virtualUserId))
            {
                VirtualUserId = long.Parse(virtualUserId);
            }

            LoggedUserId = ExtractClaimId(claimUserId);
            LoggedCompanyId = ExtractClaimId(claimCompanyId);

            LoggedOrVirtualUserId = (VirtualUserId > 0 ? VirtualUserId : LoggedUserId);
        }

        private static long ExtractClaimId(Claim claim)
        {
            return claim != null ? long.Parse(claim.Value) : 0;
        }
        #endregion Private methods

        #region Protected Methods
        protected virtual string[] Includes()
        {
            return Array.Empty<string>();
        }

        protected virtual IOrderedQueryable<T> MakeOrder(IQueryable<T> query)
        {
            return query.OrderBy(x => x.Id);
        }

        protected virtual void BeforeSave(T entity)
        {

        }

        protected virtual void BeforeSaveWithTransaction(object transaction, T entity)
        {
        }

        protected virtual void AfterSave(T entity)
        {

        }

        protected virtual void BeforeCommit(T entity)
        {

        }

        protected virtual void AfterCommit(T entity)
        {

        }

        protected virtual void BeforeValidate(T entity, RequestResult result)
        {

        }

        protected virtual void Validate(T entity, RequestResult result)
        {

        }

        protected virtual async Task AfterValidate(T entity, RequestResult result)
        {
            await Task.CompletedTask;
        }

        protected virtual void ValidateDelete(long id, RequestResult result)
        {

        }

        protected virtual Task<bool> ShouldInactiveOnDelete(long id, RequestResult result)
        {
            return Task.FromResult(false);
        }

        protected virtual void BeforeDelete(object transaction, long id)
        {

        }

        protected virtual void AfterDelete(long id)
        {

        }

        protected virtual Task<T> BeforeReturnGet(object transaction, T entity)
        {
            return Task.FromResult(entity);
        }

        public void SetCompanyIfExists(ref T entity)
        {
            System.Reflection.PropertyInfo companyProp = typeof(T).GetProperty("CompanyId");

            if (companyProp != null
                && (companyProp.GetValue(entity) == null || (long)companyProp.GetValue(entity) == 0))
            {
                companyProp.SetValue(entity, LoggedCompanyId);
            }
        }

        private Expression<Func<T, bool>> MakeDefaultFilter()
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "arg");
            Expression exp = Expression.Equal(Expression.Property(param, "Deleted"), Expression.Constant(false));

            if (typeof(T).GetProperty("CompanyId") != null)
            {
                Expression companyExp = Expression.Equal(Expression.Property(param, "CompanyId"),
                                                            Expression.Constant(LoggedCompanyId));
                exp = Expression.And(companyExp, exp);
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }
        #endregion
    }
}
