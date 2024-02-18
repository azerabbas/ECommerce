using ECommerceApi.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using P = ECommerceApi.Domain.Entities;
using PI = ECommerceApi.Domain.Entities;

namespace ECommerceApi.Application.Features.Commands.ProductImageFile.RemoveProductImageFile
{

 
    public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        readonly IUoW _uow;

        public RemoveProductImageCommandHandler(IUoW uow)
        {
            _uow=uow;
        }

        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _uow.GetReadRepository<P.Product>().Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id==Guid.Parse(request.Id));

            PI.ProductImageFile? productImageFile =  product?.ProductImageFiles.FirstOrDefault(p => p.Id==Guid.Parse(request.ImageId));
            if (productImageFile!=null)
            product?.ProductImageFiles.Remove(productImageFile);
            await _uow.SaveAsync();
            return new();
        }
    }
}
