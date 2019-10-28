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
    public class ReservaService : Service<Reserva>, IReservaService
    {
        #region Ctor
        public ReservaService(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        protected override IOrderedQueryable<Reserva> MakeOrder(IQueryable<Reserva> query)
        {
            return query.OrderBy(x => x.Cliente.Nome);
        }

        public override Task<List<Reserva>> BeforeReturnGetAll(List<Reserva> list)
        {
            using (var unit = UnitOfWorkFactory.CreateUnitOfWork())
            {
                var produtos = unit.Repository.Get<Produto>().ToList();
                var clientes = unit.Repository.Get<Cliente>().ToList();
                list.ForEach(x =>
                {
                    x.Produto = produtos.Where(y => y.Id == x.Id_Produto).FirstOrDefault();
                    x.Cliente = clientes.Where(y => y.Id == x.Id_Cliente).FirstOrDefault();
                });
            }

            return base.BeforeReturnGetAll(list);
        }
        protected override Task<Reserva> BeforeReturnGet(object transaction, Reserva entity)
        {
            using (var unit = UnitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Cliente = unit.Repository.Get<Cliente>(x => x.Id == entity.Id_Cliente).FirstOrDefault();
                entity.Produto = unit.Repository.Get<Produto>(x => x.Id == entity.Id_Produto).FirstOrDefault();
            }

            return base.BeforeReturnGet(transaction, entity);
        }
        #endregion Overrides
    }
}
