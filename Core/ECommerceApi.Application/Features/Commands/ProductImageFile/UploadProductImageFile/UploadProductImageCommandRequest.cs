using ECommerceApi.Application.Features.Commands.ProductImageFile.UploadImageFile;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ECommerceApi.Application.Features.Commands.ProductImageFile.UploadProductImageFile
{
    public class UploadProductImageCommandRequest : IRequest<UploadProductImageCommandResponse>
    {
        public string Id { get; set; }
        public IFormFileCollection Files { get; set; }

    }
}
