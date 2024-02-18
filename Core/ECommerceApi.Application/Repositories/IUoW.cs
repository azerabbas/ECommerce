using ECommerceApi.Domain.Entities.Common;

namespace ECommerceApi.Application.Repositories
{
    public interface IUoW
    {
        IReadRepositories<T> GetReadRepository<T>() where T : BaseEntity;
        IWriteRepositories<T> GetWriteRepository<T>() where T : BaseEntity;
       Task<int> SaveAsync();
    }
}
