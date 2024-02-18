using AutoMapper;
using ECommerceApi.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using P = ECommerceApi.Domain.Entities;


namespace ECommerceApi.Application.Features.Queries.Product.GetAllProduct
{
    public class GelAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IUoW _uow;
        readonly IMapper _mapper;
        readonly ILogger<GelAllProductQueryHandler> _logger;

        public GelAllProductQueryHandler(IMapper mapper, IUoW uow, ILogger<GelAllProductQueryHandler> logger)
        {
            _mapper=mapper;
            _uow=uow;
            _logger=logger;
        }



        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Get all products");
            int totalProductCount = _uow.GetReadRepository<P.Product>().GetAll(false).Count();
            IQueryable product = _uow.GetReadRepository<P.Product>()
                .GetAll(false)
                .Include(p => p.ProductImageFiles)
                .Select(p => new
            {
                p.Name,
                p.Price,
                p.Stock,
                p.ProductImageFiles

            }); /*/*.Take(request.Size * request.Page).Skip(request.Size);*GetAlldan sinra yazilacaq/*/

            return new()
            {
                Products = product,
                TotalProductCount = totalProductCount
            };


        }
    }

}


