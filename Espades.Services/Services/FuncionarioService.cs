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
    public class FuncionarioService : Service<Funcionario>, IFuncionarioService
    {
        #region Ctor
        public FuncionarioService(IServiceProvider provider)
            : base(provider)
        {
        }
        #endregion Ctor

        #region Overrides
        protected override IOrderedQueryable<Funcionario> MakeOrder(IQueryable<Funcionario> query)
        {
            return query.OrderBy(x => x.Pessoa.Nome);
        }

        public override Task<List<Funcionario>> BeforeReturnGetAll(List<Funcionario> list)
        {
            using (var unit = UnitOfWorkFactory.CreateUnitOfWork())
            {
                var pessoas = unit.Repository.Get<Pessoa>().ToList();
                var cargos = unit.Repository.Get<Cargo>().ToList();

                list.ForEach(x =>
                {
                    x.Cargo = cargos.Where(y => y.Id == x.Id_Cargo).FirstOrDefault();
                    x.Pessoa = pessoas.Where(y => y.Id == x.Id_Pessoa).FirstOrDefault();
                });
            }

            return base.BeforeReturnGetAll(list);
        }

        protected override Task<Funcionario> BeforeReturnGet(object transaction, Funcionario entity)
        {
            using (var unit = UnitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Pessoa = unit.Repository.Get<Pessoa>(x => x.Id == entity.Id_Pessoa).FirstOrDefault();
                entity.Cargo = unit.Repository.Get<Cargo>(x => x.Id == entity.Id_Cargo).FirstOrDefault();
                entity.Cargo.Setor = unit.Repository.Get<Setor>(x => x.Id == entity.Cargo.Id_Setor).FirstOrDefault();
            }

            return base.BeforeReturnGet(transaction, entity);
        }
        #endregion Overrides
    }
}
