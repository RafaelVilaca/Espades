using System;
using System.Threading.Tasks;
using Espades.Api.Models;
using Espades.Domain.Contracts.Services;
using Espades.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Espades.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : BaseController<Estoque, EstoqueModel, IEstoqueService>
    {
        #region Ctor
        public EstoqueController(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        #endregion Overrides
    }
}
