using ECommerceApi.Application.Repositories.ProductImageFile;
using ECommerceApi.Persistance.Context;
using F = ECommerceApi.Domain.Entities;


namespace ECommerceApi.Persistance.Repositories.ProductImageFile
{
    public class ProductImageFileReadRepository : WriteRepository<F.ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileReadRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}