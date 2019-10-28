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
    public class ProdutoService : Service<Produto>, IProdutoService
    {
        #region Ctor
        public ProdutoService(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        protected override IOrderedQueryable<Produto> MakeOrder(IQueryable<Produto> query)
        {
            return query.OrderBy(x => x.Descricao);
        }
        #endregion Overrides
    }
}
