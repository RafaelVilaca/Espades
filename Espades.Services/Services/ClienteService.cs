using Espades.Domain.Contracts.Services;
using Espades.Domain.Entities;
using Espades.Services.Base;
using System;
using System.Linq;

namespace Espades.Services.Services
{
    public class ClienteService : Service<Cliente>, IClienteService
    {
        #region Ctor
        public ClienteService(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        protected override IOrderedQueryable<Cliente> MakeOrder(IQueryable<Cliente> query)
        {
            return query.OrderBy(x => x.Nome);
        }
        #endregion Overrides
    }
}
