using System.Net.Mime;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiService.Models;
using WebApiService.Clients;

namespace WebApiService.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
     
        private readonly IImageClient _imageClient;
        private readonly IPriceClient _priceClient;

        public ProductController( IImageClient imageClient, IPriceClient priceClient)
        {
            
            _imageClient = imageClient;
            _priceClient = priceClient;
        }

        [HttpGet("/products")]
        public async Task<IEnumerable<Product>> Get()
        {
            var rng = new Random();
            var images = await _imageClient.GetAll();
            var prices = await _priceClient.GetAll();

            return Enumerable.Range(1, 5).Select(index => new Product{
                Id = Guid.NewGuid(),
                Name = $"Product{new Random().Next()}",
                Description = "Description of product",
                Image = images,
                Price = prices
            }).ToArray();
        }
    }
}
