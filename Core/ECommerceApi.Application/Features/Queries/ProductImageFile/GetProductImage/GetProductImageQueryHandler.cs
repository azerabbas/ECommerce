using ECommerceApi.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using P = ECommerceApi.Domain.Entities;

namespace ECommerceApi.Application.Features.Queries.ProductImageFile.GetProductImage
{
    public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQueryRequest, List<GetProductImageQueryResponse>>
    {
        readonly IUoW _uow;
        readonly IConfiguration _configuration;

        public GetProductImageQueryHandler(IUoW uow, IConfiguration configuration)
        {
            _uow=uow;
            _configuration=configuration;
        }

        public async Task<List<GetProductImageQueryResponse>> Handle(GetProductImageQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _uow.GetReadRepository<P.Product>().Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id==Guid.Parse(request.Id));
            return product?.ProductImageFiles.Select(p => new GetProductImageQueryResponse
            {
                FileName = p.FileName,
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                Id = p.Id
            }).ToList();
        }
    }
}
