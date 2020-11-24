using System.Net.Mime;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductService.Models;
using ProductService.Clients;
using Microsoft.EntityFrameworkCore;
using ProductService.Interfaces;
using AutoMapper;
using ProductService.Repositories;

namespace ProductService.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            var productDb = await _productService.GetAll();
            return _mapper.Map<IEnumerable<Product>>(productDb);
        }

        [HttpGet("{id}")]
        public async Task<Product> Get(Guid id)
        {
            var productDb = await _productService.Get(id);
            return _mapper.Map<Product>(productDb);
        }

        [HttpPost]
        public async Task Create(Product product)
        {
            var productDb = _mapper.Map<ProductDb>(product);
            await _productService.Create(productDb);
        }

        [HttpPut]
        public async Task Update(Product product)
        {
            var productDb = _mapper.Map<ProductDb>(product);
            await _productService.Update(productDb);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _productService.Delete(id);
        }
    }
}
