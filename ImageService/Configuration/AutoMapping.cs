using AutoMapper;
using ImageService.Repositories;
using ImageService.Models;

namespace ImageService.Configuration
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ImageRepository, Image>();
            CreateMap<Image, ImageRepository>();
        }
    }
}