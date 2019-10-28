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
    public class EstoqueService : Service<Estoque>, IEstoqueService
    {
        #region Ctor
        public EstoqueService(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        protected override IOrderedQueryable<Estoque> MakeOrder(IQueryable<Estoque> query)
        {
            return query.OrderBy(x => x.Descricao);
        }

        public override Task<List<Estoque>> BeforeReturnGetAll(List<Estoque> list)
        {
            using (var unit = UnitOfWorkFactory.CreateUnitOfWork())
            {
                var produtos = unit.Repository.Get<Produto>().ToList();
                list.ForEach(x =>
                {
                    x.Produto = produtos.Where(y => y.Id == x.Id_Produto).FirstOrDefault();
                });
            }

            return base.BeforeReturnGetAll(list);
        }
        protected override Task<Estoque> BeforeReturnGet(object transaction, Estoque entity)
        {
            using (var unit = UnitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Produto = unit.Repository.Get<Produto>(x => x.Id == entity.Id_Produto).FirstOrDefault();
            }

            return base.BeforeReturnGet(transaction, entity);
        }
        #endregion Overrides
    }
}
