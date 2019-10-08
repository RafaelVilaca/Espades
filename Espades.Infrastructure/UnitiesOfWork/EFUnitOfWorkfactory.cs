using Espades.Domain.Contracts.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Espades.Infrastructure.UnitiesOfWork
{
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IConfiguration _configuration;

        public EFUnitOfWorkFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            DbContextOptionsBuilder<EspadesContext> builder = new DbContextOptionsBuilder<EspadesContext>();
            builder.UseSqlServer(_configuration.GetConnectionString("EspadesConnection"),
                                 b => b.MigrationsAssembly("Espades.Api")
                   .UseRowNumberForPaging());

            return new EFUnitOfWork(new EspadesContext(builder.Options));
        }

        public IUnitOfWork CreateUnitOfWork(object transaction)
        {
            IUnitOfWork uow = CreateUnitOfWork();
            uow.SetTransaction(transaction);
            return uow;
        }

        public void Dispose()
        {
        }
    }
}
