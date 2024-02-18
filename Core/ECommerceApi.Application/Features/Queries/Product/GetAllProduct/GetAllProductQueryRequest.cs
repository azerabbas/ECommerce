using MediatR;

namespace ECommerceApi.Application.Features.Queries.Product.GetAllProduct
{
    //public class GetAllProductQueryRequest : IRequest<IEnumerable<GetAllProductQueryResponse>>
    //{
      
    //}

  //  mvc ve angular ucun
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
