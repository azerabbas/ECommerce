using AutoMapper;
using ECommerceApi.Application.DTOs.Product;
using ECommerceApi.Domain.Entities;

namespace ECommerceApi.Application.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()       
        {
             CreateMap<Product_VM, Product>().ReverseMap();
             CreateMap<Product_Up_VM, Product>().ReverseMap();
             CreateMap<Product_Get_Vm, Product>().ReverseMap();
        }

    }
}
