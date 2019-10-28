using Espades.Domain.Contracts.Services;
using Espades.Domain.Entities;
using Espades.Services.Base;
using System;
using System.Linq;

namespace Espades.Services.Services
{
    public class DespesaService : Service<Despesa>, IDespesaService
    {
        #region Ctor
        public DespesaService(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        protected override IOrderedQueryable<Despesa> MakeOrder(IQueryable<Despesa> query)
        {
            return query.OrderBy(x => x.Descricao);
        }
        #endregion Overrides
    }
}
