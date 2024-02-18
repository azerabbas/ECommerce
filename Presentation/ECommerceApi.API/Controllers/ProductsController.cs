using ECommerceApi.Application.Features.Commands.CreateProduct;
using ECommerceApi.Application.Features.Commands.Product.DeleteProduct;
using ECommerceApi.Application.Features.Commands.Product.UpdateProduct;
using ECommerceApi.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage;
using ECommerceApi.Application.Features.Commands.ProductImageFile.RemoveProductImageFile;
using ECommerceApi.Application.Features.Commands.ProductImageFile.UploadImageFile;
using ECommerceApi.Application.Features.Commands.ProductImageFile.UploadProductImageFile;
using ECommerceApi.Application.Features.Queries.Product.GetAllProduct;
using ECommerceApi.Application.Features.Queries.Product.GetByIdproduct;
using ECommerceApi.Application.Features.Queries.ProductImageFile.GetProductImage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
           
        readonly IMediator _mediator;
        readonly ILogger<ProductsController> _logger;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator=mediator;
            _logger=logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            
            var response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);

        }


        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetByIdpProductQueryRequest((id)));
            if (result != null)
                return Ok(result);
            else
                return NotFound(new { message = "bu id movcud deyil" });
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse result = await _mediator.Send(request);
            return Ok(result);


        }

        [HttpPut]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);

        }

        [HttpDelete]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            RemoveProductCommandResponse result = await _mediator.Send(new RemoveProductCommandRequest(id));
            return Ok(result);
        }

        [HttpPost("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest request)
        {
            request.Files = Request.Form.Files;
            UploadProductImageCommandResponse response = await _mediator.Send(request);
            return Ok();
        }

        [HttpPost("[action]/{id}")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetProdutImage([FromRoute] GetProductImageQueryRequest request)
        {
           List<GetProductImageQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteProductImage( [ FromRoute] RemoveProductImageCommandRequest request, [FromQuery] string imageId)
        {
            request.ImageId = imageId;
            RemoveProductImageCommandResponse response = await _mediator.Send(request);
            return Ok();

        }

        [HttpGet("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest request)
        {
            ChangeShowcaseImageCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
