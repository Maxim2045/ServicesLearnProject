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

namespace ProductService.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        /*   private readonly IImageClient _imageClient;
           private readonly IPriceClient _priceClient;

           public ProductController( IImageClient imageClient, IPriceClient priceClient)
           {

               _imageClient = imageClient;
               _priceClient = priceClient;
           }*/

        /* [HttpGet("/products")]
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
         }*/
        private readonly ProductContext db;
        public ProductController(ProductContext context)
        {
            db = context;
            if (!db.Products.Any())
            {
                db.Products.Add(new Product { Name = "Tesla", Description = "Over price" });
                db.Products.Add(new Product { Name = "Honda", Description = "Fast and fury" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await db.Products.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            Product product = await db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return NotFound();
            return new ObjectResult(product);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Product>> Put(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            if (!db.Products.Any(x => x.Id == product.Id))
            {
                return NotFound();
            }

            db.Update(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(Guid id)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }
    }
}
