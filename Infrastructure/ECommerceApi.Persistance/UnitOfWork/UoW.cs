using ECommerceApi.Application.Repositories;
using ECommerceApi.Domain.Entities.Common;
using ECommerceApi.Persistance.Context;
using ECommerceApi.Persistance.Repositories;
namespace ECommerceApi.Persistance.UnitOfWork
{
    public class UoW : IUoW
    {
        readonly ECommerceApiDbContext _context;

        public UoW(ECommerceApiDbContext context)
        {
            _context=context;
        }

        public IReadRepositories<T> GetReadRepository<T>() where T : BaseEntity
        {
            return new ReadRepository<T>(_context);
        }

        public IWriteRepositories<T> GetWriteRepository<T>() where T : BaseEntity
        {
            return new WriteRepository<T>(_context);
        }
        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}
