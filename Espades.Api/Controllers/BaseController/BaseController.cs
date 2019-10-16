using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Espades.Common;
using Espades.Common.Containers;
using Espades.Common.Enumerators;
using Espades.Common.Exceptions;
using Espades.Common.Helpers;
using Espades.Domain.Contracts.Services.Base;
using Espades.Domain.Entities.Base;
using AutoMapper;

namespace TrouwNutrition.ESales.Api.Controllers
{
    #region Base class
    public abstract class BaseController : Controller
    {
    }
    #endregion Base class

    public abstract class BaseController<TEntity, TModel, TService> : BaseController
        where TEntity : BaseEntity
        where TService : IService<TEntity>
    {
        #region Public variables
        public TService _service { get; set; }
        public readonly ILogger _logger;
        public readonly IStringLocalizer _localizer;
        #endregion Public variables

        #region Ctor
        public BaseController(IServiceProvider provider)
        {
            _service = (TService)provider.GetService(typeof(TService));
            _logger = provider.GetService(typeof(ILogger<BaseController<TEntity, TModel, TService>>)) as ILogger<BaseController<TEntity, TModel, TService>>;
            _localizer = provider.GetService(typeof(IStringLocalizer<LanguageLocalizer>)) as IStringLocalizer<LanguageLocalizer>;
        }
        #endregion

        #region Virtual methods
        //[Authorize("Bearer")]
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (BusinessException ex)
            {
                return Ok(ex.ExceptionResult);
            }
            catch (Exception ex)
            {
                return Ok(new RequestResult(StatusResult.Danger, new Message(string.Format(_localizer["UnexpectedError"], ex.Message))));
            }
        }

        //[Authorize("Bearer")]
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(long id)
        {
            try
            {
                return Ok(await _service.Get(id));
            }
            catch (BusinessException ex)
            {
                return Ok(ex.ExceptionResult);
            }
            catch (Exception ex)
            {
                return Ok(new RequestResult(StatusResult.Danger, new Message(string.Format(_localizer["UnexpectedError"], ex.Message))));
            }
        }

        //[Authorize("Bearer")]
        [HttpPost("filter/{filters}"), Route("filter")]
        public virtual async Task<IActionResult> Filter([FromBody] FilterHelper filters)
        {
            try
            {
                return Ok(await _service.GetByFilter(filters));
            }
            catch (BusinessException ex)
            {
                return Ok(ex.ExceptionResult);
            }
            catch (Exception ex)
            {
                return Ok(new RequestResult(StatusResult.Danger, new Message(string.Format(_localizer["UnexpectedError"], ex.Message))));
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]TModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new RequestResult(StatusResult.Warning, ModelStateHelper.GetModelStateErrors(ModelState, _localizer)));
                }

                TEntity entity = Mapper.Map<TModel, TEntity>(model);
                return Ok(await _service.Save(entity));
            }
            catch (BusinessException ex)
            {
                return Ok(ex.ExceptionResult);
            }
            catch (Exception ex)
            {
                return Ok(new RequestResult(StatusResult.Danger, new Message(string.Format(_localizer["UnexpectedError"], ex.Message))));
            }
        }

        //[Authorize("Bearer")]
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromBody]TModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new RequestResult(StatusResult.Warning, ModelStateHelper.GetModelStateErrors(ModelState, _localizer)));
                }

                TEntity entity = Mapper.Map<TModel, TEntity>(model);
                return Ok(await _service.Save(entity));
            }
            catch (BusinessException ex)
            {
                return Ok(ex.ExceptionResult);
            }
            catch (Exception ex)
            {
                return Ok(new RequestResult(StatusResult.Danger, new Message(string.Format(_localizer["UnexpectedError"], ex.Message))));
            }
        }

        //[Authorize("Bearer")]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (BusinessException ex)
            {
                return Ok(ex.ExceptionResult);
            }
            catch (Exception ex)
            {
                return Ok(new RequestResult(StatusResult.Danger, new Message(string.Format(_localizer["UnexpectedError"], ex.Message))));
            }
        }
        #endregion Virtual methods
    }
}
