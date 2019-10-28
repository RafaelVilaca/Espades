using Espades.Common.Containers;
using Espades.Domain.Contracts.Services;
using Espades.Domain.Entities;
using Espades.Services.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Espades.Services.Services
{
    public class SetorService : Service<Setor>, ISetorService
    {
        #region Ctor
        public SetorService(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        protected override IOrderedQueryable<Setor> MakeOrder(IQueryable<Setor> query)
        {
            return query.OrderBy(x => x.Descricao);
        }

        protected override Task<Setor> BeforeReturnGet(object transaction, Setor entity)
        {
            using (var unit = UnitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Cargos = unit.Repository.Get<Cargo>(x => x.Id_Setor == entity.Id).ToList();
            }

            return base.BeforeReturnGet(transaction, entity);
        }

        protected override Task<bool> ShouldInactiveOnDelete(long id, RequestResult result)
        {
            bool verify = false;
            using (var unit = UnitOfWorkFactory.CreateUnitOfWork())
            {
                var cargo = unit.Repository.First<Cargo>(x => x.Id_Setor == id).Result;
                verify = cargo != null;
            }
            return Task.FromResult(verify);
        }
        #endregion Overrides
    }
}
