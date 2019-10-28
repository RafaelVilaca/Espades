using Espades.Common.Containers;
using Espades.Domain.Contracts.Services;
using Espades.Domain.Entities;
using Espades.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Espades.Services.Services
{
    public class PatrimonioService : Service<Patrimonio>, IPatrimonioService
    {
        #region Ctor
        public PatrimonioService(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        protected override IOrderedQueryable<Patrimonio> MakeOrder(IQueryable<Patrimonio> query)
        {
            return query.OrderBy(x => x.Descricao);
        }
        #endregion Overrides
    }
}
