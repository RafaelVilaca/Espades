using Espades.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace Espades.Domain.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository Repository { get; set; }
        object GetTransaction();
        void SetTransaction(object context);
        Task<int> Commit();
        void Rollback();
    }
}
