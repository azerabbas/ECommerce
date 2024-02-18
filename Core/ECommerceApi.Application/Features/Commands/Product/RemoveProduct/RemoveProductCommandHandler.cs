using ECommerceApi.Application.Repositories;
using MediatR;
using P = ECommerceApi.Domain.Entities;
namespace ECommerceApi.Application.Features.Commands.Product.DeleteProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {

        readonly IUoW _uow;

        public RemoveProductCommandHandler(IUoW uow)
        {
            _uow=uow;
        }

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _uow.GetWriteRepository<P.Product>().RemoveAsync(request.Id);
            if (product == null)
                return new() { Message = "Product tapilmadi" };
            //_uow.GetWriteRepository<P.Product>().Remove(product);
            await _uow.SaveAsync();
            return new() { Message = "Product ugurla silinmisdir" };
        }
    }
}
