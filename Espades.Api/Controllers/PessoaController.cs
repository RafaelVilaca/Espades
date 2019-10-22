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
    public class PessoaController : BaseController<Pessoa, PessoaModel, IPessoaService>
    {
        #region Ctor
        public PessoaController(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        [HttpPost]
        public override async Task<IActionResult> Post([FromBody]PessoaModel model)
        {
            return await Task.FromResult(NotFound());
        }

        [HttpPut]
        public override async Task<IActionResult> Put([FromBody]PessoaModel model)
        {
            model.Enderecos = null;
            return await Task.FromResult(NotFound());
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            return await Task.FromResult(NotFound());
        }
        #endregion Overrides
    }
}
