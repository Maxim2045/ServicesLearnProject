using AutoMapper;
using PriceService.Models;
using PriceService.Repository;

namespace PriceService.Configuration
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<PriceDbModel, Price>().ReverseMap();
        }  
    }
}