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
        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            return await Task.FromResult(NotFound());
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(long id)
        {
            return await Task.FromResult(NotFound());
        }

        [HttpPost]
        public override async Task<IActionResult> Post([FromBody]EnderecoModel model)
        {
            return await Task.FromResult(NotFound());
        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> Put([FromBody]EnderecoModel model)
        {
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
