using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceApi.Application.Features.Queries.Product.GetByIdproduct
{
    public class GetByIdpProductQueryRequest : IRequest<GetByIdProductQueryResponse>
    {
        public string Id { get; set; }

        public GetByIdpProductQueryRequest(string id)
        {
            if (!Guid.TryParse(id, out _))
                Id = null;
            else
                Id=id;

        }
    }


}

