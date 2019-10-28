using Espades.Common.Containers;
using Espades.Domain.Contracts.Services;
using Espades.Domain.Entities;
using Espades.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Espades.Services.Services
{
    public class CargoService : Service<Cargo>, ICargoService
    {
        #region Ctor
        public CargoService(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        protected override IOrderedQueryable<Cargo> MakeOrder(IQueryable<Cargo> query)
        {
            return query.OrderBy(x => x.Descricao);
        }

        public override Task<List<Cargo>> BeforeReturnGetAll(List<Cargo> list)
        {
            using (var unit = UnitOfWorkFactory.CreateUnitOfWork())
            {
                var setores = unit.Repository.Get<Setor>().ToList();
                list.ForEach(x =>
                {
                    x.Setor = setores.Where(y => y.Id == x.Id_Setor).FirstOrDefault();
                });                
            }

            return base.BeforeReturnGetAll(list);
        }

        protected override Task<bool> ShouldInactiveOnDelete(long id, RequestResult result)
        {
            bool verify = false;
            using (var unit = UnitOfWorkFactory.CreateUnitOfWork())
            {
                var funcionario = unit.Repository.First<Funcionario>(x => x.Id_Cargo == id).Result;
                verify = funcionario != null;
            }
            return Task.FromResult(verify);
        }
        #endregion Overrides
    }
}
