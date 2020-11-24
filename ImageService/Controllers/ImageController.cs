using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ImageService.Repositories;
using ImageService.Interfaces;
using ImageService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ImageController(IMapper mapper, IImageService imageService)
        {
            _mapper = mapper;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IEnumerable<Image>> GetAll()
        {
            var imageRepository = await _imageService.GetAll();
            return _mapper.Map<IEnumerable<Image>>(imageRepository);
        }

        [HttpGet("{id}")]
        public async Task<Image> Get(Guid id)
        {
            var imageRepository = await _imageService.Get(id);
            return _mapper.Map<Image>(imageRepository);
        }

        [HttpPost]
        public async Task Create(Image image)
        {
            var imageRepository = _mapper.Map<ImageRepository>(image);
            await _imageService.Create(imageRepository);
        }

        [HttpPut]
        public async Task Update(Image image)
        {
            var imageRepository = _mapper.Map<ImageRepository>(image);
            await _imageService.Update(imageRepository);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _imageService.Delete(id);
        }
    }
}