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

        protected override Task<bool> ShouldInactiveOnDelete(long id, RequestResult result)
        {
            bool verify = false;
            using (var unit = UnitOfWorkFactory.CreateUnitOfWork())
            {
                var endereco = unit.Repository.First<Endereco>(x => x.Id_Pessoa == id).Result;
                verify = endereco != null;

                var funcionario = unit.Repository.First<Funcionario>(x => x.Id_Pessoa == id).Result;
                verify = funcionario != null;
            }
            return Task.FromResult(verify);
        }
        #endregion Overrides
    }
}
