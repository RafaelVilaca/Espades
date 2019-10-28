using Espades.Common.Containers;
using Espades.Common.Helpers;
using Espades.Domain.Entities.Base;
using System.Threading.Tasks;

namespace Espades.Domain.Contracts.Services.Base
{
    public interface IService<TEntity>
        where TEntity : BaseEntity
    {
        Task<RequestResult> Save(TEntity entity);
        Task<RequestResult> Delete(int id);
        Task<RequestResult> Get(int id);
        Task<RequestResult> GetAll();
        Task<RequestResult> GetByFilter(FilterHelper filter);
    }
}
