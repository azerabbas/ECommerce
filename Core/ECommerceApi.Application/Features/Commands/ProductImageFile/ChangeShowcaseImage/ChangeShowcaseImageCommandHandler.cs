using ECommerceApi.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PI = ECommerceApi.Domain.Entities;

namespace ECommerceApi.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
    {
        readonly IUoW _uow;

        public ChangeShowcaseImageCommandHandler(IUoW uow)
        {
            _uow=uow;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _uow.GetReadRepository<PI.ProductImageFile>().Table
                  .Include(p => p.Products)
                  .SelectMany(p => p.Products, (pif, p) => new
                  {
                      pif,
                      p
                  });

            var data = await query.FirstOrDefaultAsync(p => p.p.Id== Guid.Parse(request.ProductId) && p.pif.Showcase);
            if (data !=null)
                data.pif.Showcase = false;

            var image = await query.FirstOrDefaultAsync(p => p.pif.Id==Guid.Parse(request.ImageId));
            if (image  != null)
                image.pif.Showcase = true;

            await _uow.SaveAsync();
            return new();



        }
    }
}
