using ECommerceApi.Application.Features.Queries.Product.GetByIdproduct;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Features.Commands.Product.DeleteProduct
{
    public class RemoveProductCommandRequest : IRequest<RemoveProductCommandResponse>
    {
        public string Id { get; set; }

        public RemoveProductCommandRequest(string id)
        {

            if (!Guid.TryParse(id, out _))
                Id = null;
            else
                Id=id;
        }
    }
}
