using ECommerceApi.Application.Abstraction.Hubs;
using ECommerceApi.Application.Repositories;
using MediatR;
using P = ECommerceApi.Domain.Entities;

namespace ECommerceApi.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IUoW _uow;
        readonly IProductHubService _productHubService;

        public CreateProductCommandHandler(IUoW uow, IProductHubService productHubService)
        {
            _uow=uow;
            _productHubService=productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _uow.GetWriteRepository<P.Product>().AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            });

            await _uow.SaveAsync();
            await _productHubService.ProductAddedMessageAsync($"{request.Name} adina product add olunmusdur");
            return new()
            {
                Message = "Product ugurla add edilmishdir"
            };

        }
    }
}
