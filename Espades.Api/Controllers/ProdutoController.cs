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
    public class ProdutoController : BaseController<Produto, ProdutoModel, IProdutoService>
    {
        #region Ctor
        public ProdutoController(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        #endregion Overrides
    }
}
