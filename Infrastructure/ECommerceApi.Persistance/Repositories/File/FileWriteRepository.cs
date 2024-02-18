using ECommerceApi.Application.Repositories.File;
using ECommerceApi.Persistance.Context;
using F = ECommerceApi.Domain.Entities;



namespace ECommerceApi.Persistance.Repositories.File
{
    public class FileWriteRepository : WriteRepository<F.File>, IFileWriteRepository
    {
        public FileWriteRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
