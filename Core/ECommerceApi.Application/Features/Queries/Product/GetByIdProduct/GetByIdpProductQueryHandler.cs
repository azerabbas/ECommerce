using ECommerceApi.Application.Repositories;
using MediatR;
using P = ECommerceApi.Domain.Entities;
namespace ECommerceApi.Application.Features.Queries.Product.GetByIdproduct
{
    public class GetByIdpProductQueryHandler : IRequestHandler<GetByIdpProductQueryRequest, GetByIdProductQueryResponse>
    {
        readonly IUoW _uow;

        public GetByIdpProductQueryHandler(IUoW uow)
        {
            _uow=uow;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdpProductQueryRequest request, CancellationToken cancellationToken)
        {

            var product = await _uow.GetReadRepository<P.Product>().GetByIdAsync(request.Id, false);
            if (product != null)
            {
                return new()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock
                };
            }

            return null;
        }
    }
}
