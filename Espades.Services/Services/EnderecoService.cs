using Espades.Domain.Contracts.Services;
using Espades.Domain.Entities;
using Espades.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Espades.Services.Services
{
    public class EnderecoService : Service<Endereco>, IEnderecoService
    {
        #region Ctor
        public EnderecoService(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        protected override IOrderedQueryable<Endereco> MakeOrder(IQueryable<Endereco> query)
        {
            return query.OrderBy(x => x.Rua);
        }
        #endregion Overrides
    }
}
