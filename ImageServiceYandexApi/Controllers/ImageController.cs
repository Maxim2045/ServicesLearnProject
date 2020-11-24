using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageServiceYandexApi.Interfaces;
using ImageServiceYandexApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageServiceYandexApi.Controllers
{
    [ApiController]
    [Route("api/cImages")]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService ?? throw new ArgumentException(null, nameof(imageService));
        }

        [HttpGet]
        public async Task<IEnumerable<Image>> GetAll()
        {
            return await _imageService.GetAll();
        }

        [HttpGet("GetImageMetaInfo/{id}")]
        public async Task<Image> GetImageMetaInfo(Guid id)
        {
            return await _imageService.GetImageMetaInfo(id);
        }

        [HttpGet("GetImageUrl/{id}")]
        public async Task<string> GetImageUrl(Guid id)
        {
            return await _imageService.GetImageUrl(id);
        }

        [HttpPost]
        public async Task Upload(string imageUrl)
        {
            await _imageService.Upload(imageUrl);
        }
    }
}