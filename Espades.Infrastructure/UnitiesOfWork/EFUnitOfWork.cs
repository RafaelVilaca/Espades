using Espades.Domain.Contracts.UnitOfWork;
using Espades.Domain.Repository;
using Espades.Infrastructure.Base.Repositories;
using System;
using System.Threading.Tasks;

namespace Espades.Infrastructure.UnitiesOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private EspadesContext _dbContext;

        public IRepository Repository { get; set; }

        public EFUnitOfWork(EspadesContext dbContext)
        {
            _dbContext = dbContext;
            Repository = new Repository(_dbContext);
        }

        public object GetTransaction()
        {
            return _dbContext;
        }

        public void SetTransaction(object context)
        {
            _dbContext = (EspadesContext)context;
            Repository = new Repository(_dbContext);
        }

        public async Task<int> Commit()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Rollback()
        {
            try
            {
                _dbContext.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
        }
    }
}
