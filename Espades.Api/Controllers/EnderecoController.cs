using Espades.Api.Models;
using Espades.Domain.Contracts.Services;
using Espades.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Espades.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : BaseController<Endereco, EnderecoModel, IEnderecoService>
    {
        #region Ctor
        public EnderecoController(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        #endregion Overrides
    }
}
