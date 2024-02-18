using ECommerceApi.Application.Abstraction.Storage;
using ECommerceApi.Application.Features.Commands.ProductImageFile.UploadImageFile;
using ECommerceApi.Application.Repositories;
using MediatR;
using P = ECommerceApi.Domain.Entities;
using PI = ECommerceApi.Domain.Entities;
namespace ECommerceApi.Application.Features.Commands.ProductImageFile.UploadProductImageFile
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IStorageService _storageService;
        readonly IUoW _uow;

        public UploadProductImageCommandHandler(IUoW uow, IStorageService storageService)
        {
            _uow=uow;
            _storageService=storageService;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", request.Files);

            P.Product product = await _uow.GetReadRepository<P.Product>().GetByIdAsync(request.Id);

            //bu aldigimiz datani productImageFile add edirik.
                await _uow.GetWriteRepository<PI.ProductImageFile>().AddRangeAsync(result.Select(r => new PI.ProductImageFile
                {
                    FileName = r.fileName,
                    Path = r.pathOrContainerName,
                    Storage = _storageService.StorageName,
                    Products = new List<P.Product> { product }
                }).ToList());

            await _uow.SaveAsync();
            return new ();
        }
    }
}
