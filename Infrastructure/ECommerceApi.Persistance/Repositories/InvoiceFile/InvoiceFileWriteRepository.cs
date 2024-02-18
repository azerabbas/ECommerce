using ECommerceApi.Application.Repositories.InvoiceFile;
using ECommerceApi.Persistance.Context;
using F = ECommerceApi.Domain.Entities;

namespace ECommerceApi.Persistance.Repositories.InvoiceFile
{
    public class InvoiceFileWriteRepository : WriteRepository<F.InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
