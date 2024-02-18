
using ECommerceApi.Application.Repositories.InvoiceFile;
using ECommerceApi.Persistance.Context;
using F = ECommerceApi.Domain.Entities;


namespace ECommerceApi.Persistance.Repositories.InvoiceFile
{
    public class InvoiceFileReadRepository : ReadRepository<F.InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
