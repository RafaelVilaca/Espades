using System;

namespace Espades.Domain.Contracts.UnitOfWork
{
    public interface IUnitOfWorkFactory : IDisposable
    {
        IUnitOfWork CreateUnitOfWork();
        IUnitOfWork CreateUnitOfWork(object transaction);
    }
}
