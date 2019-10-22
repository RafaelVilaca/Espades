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
    public class PessoaService : Service<Pessoa>, IPessoaService
    {
        #region Ctor
        public PessoaService(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        protected override IOrderedQueryable<Pessoa> MakeOrder(IQueryable<Pessoa> query)
        {
            return query.OrderBy(x => x.Nome);
        }

        protected override Task<Pessoa> BeforeReturnGet(object transaction, Pessoa entity)
        {
            using (var unit = UnitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Enderecos = unit.Repository.Get<Endereco>(x => x.Id_Pessoa == entity.Id).ToList();
            }

            return base.BeforeReturnGet(transaction, entity);
        }
        #endregion Overrides
    }
}
