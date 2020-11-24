using AutoMapper;
using ProductService.Repositories;
using ProductService.Models;

namespace ProductService.Configuration
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ProductDb, Product>();
            CreateMap<Product, ProductDb>();
        }
    }
}
