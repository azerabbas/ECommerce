using ECommerceApi.Application.Repositories.File;
using ECommerceApi.Persistance.Context;
using F = ECommerceApi.Domain.Entities;

namespace ECommerceApi.Persistance.Repositories.File
{
    public class FileReadRepository : ReadRepository<F.File>, IFileReadRepository
    {
        public FileReadRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
