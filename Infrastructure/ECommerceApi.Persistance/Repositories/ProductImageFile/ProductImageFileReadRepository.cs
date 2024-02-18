using ECommerceApi.Application.Repositories.ProductImageFile;
using ECommerceApi.Persistance.Context;
using F = ECommerceApi.Domain.Entities;


namespace ECommerceApi.Persistance.Repositories.ProductImageFile
{
    public class ProductImageWriteReadRepository : WriteRepository<F.ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageWriteReadRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
