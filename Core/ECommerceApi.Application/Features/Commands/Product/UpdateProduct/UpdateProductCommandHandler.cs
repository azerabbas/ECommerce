using ECommerceApi.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using P = ECommerceApi.Domain.Entities;

namespace ECommerceApi.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IUoW _uow;
        readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IUoW uow, ILogger<UpdateProductCommandHandler> logger)
        {
            _uow=uow;
            _logger=logger;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
          P.Product product =   await _uow.GetReadRepository<P.Product>().GetByIdAsync(request.Id);
            if(product != null)
            {
                product.Name = request.Name;
                product.Price = request.Price;
                product.Stock = request.Stock;
                await _uow.SaveAsync();
                return new()
                {
                    Message = "Data ugurla update edilmishdir"
                };
            }
            _logger.LogInformation("Product update olundu");
            return new() { Message = "Data tapilmamishdir" };
        
     
        }
    }
}
