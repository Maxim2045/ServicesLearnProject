using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProductService.Clients;
using ProductService.Interfaces;
using ProductService.Models;
using ProductService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductService
{
    public class ProductService : IProductService
    {      
        private readonly IImageClient _imageClient;
        private readonly IPriceClient _priceClient;
        private readonly ProductContext _productContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IImageClient imageClient, IPriceClient priceClient, ProductContext productContext, IHttpContextAccessor httpContextAccessor)
        {
            _imageClient = imageClient ?? throw new ArgumentNullException(nameof(imageClient));
            _priceClient = priceClient ?? throw new ArgumentNullException(nameof(priceClient));
            _productContext = productContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<ProductDb>> GetAll()
        {
            return await _productContext.Products.ToListAsync();
        }

        public async Task<ProductDb> Get(Guid id)
        {
            var product = await _productContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new ArgumentException($"Изображение с идентификатором {id} не найдено в БД.");
            }

            return product;
        }

        public async Task Create(ProductDb entity)
        {
            // Guid.Empty - "00000000-0000-0000-0000-000000000000"
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            entity.CreatedDate = DateTime.UtcNow;
            entity.LastSavedDate = DateTime.UtcNow;

            if (Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                out var userId))
            {
                entity.CreatedBy = userId;
                entity.LastSavedBy = userId;
            }

            await _productContext.Products.AddAsync(entity);
            await _productContext.SaveChangesAsync();
        }

        public async Task Update(ProductDb productDb)
        {
            productDb.LastSavedDate = DateTime.UtcNow;

            if (Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                out var userId))
            {
                productDb.LastSavedBy = userId;
            }

            _productContext.Products.Update(productDb);
            await _productContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = await _productContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new ArgumentException($"Не найден продукт с {id} в БД");
            }

            if (Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                out var userId))
            {
                product.LastSavedBy = userId;
            }

            _productContext.Products.Remove(product);
            await _productContext.SaveChangesAsync();
        }

        /* private void FillProducts()
         {
             _products = new List<ProductDb>
             {
                 new ProductDb
                 {
                     Id = Guid.Parse("F76C0D19-9357-4A6C-963A-EF0E5D6A87FD"),
                     Name = "Тойота крузер",
                     Description="Черная, удобная машина",
                     Image = _imageClient.GetAll().Result.Where(x => x.ProductId==Guid.Parse("F76C0D19-9357-4A6C-963A-EF0E5D6A87FD")),
                     Price = _priceClient.GetAll().Result.Where(x => x.ProductId==Guid.Parse("F76C0D19-9357-4A6C-963A-EF0E5D6A87FD")),
                     CreatedDate=new DateTime(),
                     CreatedBy= new Guid(),
                     LastSavedDate=new DateTime(),
                     LastSavedBy=new Guid(),
                     IsDeleted=false

                 },
                 new ProductDb
                 {
                     Id = Guid.Parse("8AA0B6DB-259C-4B6A-B24E-BD9A1148190A"),
                     Name = "Тойота седан",
                     Description="Красивая, надежная машина",
                  //   Image = _imageClient.GetAll().Result.Where(x => x.ProductId==Guid.Parse("8AA0B6DB-259C-4B6A-B24E-BD9A1148190A")),
                  //   Price = _priceClient.GetAll().Result.Where(x => x.ProductId==Guid.Parse("8AA0B6DB-259C-4B6A-B24E-BD9A1148190A")),
                     CreatedDate=new DateTime(),
                     CreatedBy= new Guid(),
                     LastSavedDate=new DateTime(),
                     LastSavedBy=new Guid(),
                     IsDeleted=false
                 },
                 new ProductDb
                 {
                     Id = Guid.Parse("8870A505-01EA-4382-959B-DA5403B49029"),
                     Name = "Жигули",
                     Description="Дешевая, легко ремонтируемая машина",
                  //   Image = _imageClient.GetAll().Result.Where(x => x.ProductId==Guid.Parse("8870A505-01EA-4382-959B-DA5403B49029")),
                  //   Price = _priceClient.GetAll().Result.Where(x => x.ProductId==Guid.Parse("8870A505-01EA-4382-959B-DA5403B49029")),
                     CreatedDate=new DateTime(),
                     CreatedBy= new Guid(),
                     LastSavedDate=new DateTime(),
                     LastSavedBy=new Guid(),
                     IsDeleted=false
                 },
                 new ProductDb
                 {
                     Id = Guid.Parse("53F264F2-ACA4-4499-87A3-F45D62EBBF13"),
                     Name = "Гранта",
                     Description="Лучше от автоваз на данный момент по цене и качеству",
                  //   Image = _imageClient.GetAll().Result.Where(x => x.ProductId==Guid.Parse("53F264F2-ACA4-4499-87A3-F45D62EBBF13")),
                 //    Price = _priceClient.GetAll().Result.Where(x => x.ProductId==Guid.Parse("53F264F2-ACA4-4499-87A3-F45D62EBBF13")),
                     CreatedDate=new DateTime(),
                     CreatedBy= new Guid(),
                     LastSavedDate=new DateTime(),
                     LastSavedBy=new Guid(),
                     IsDeleted=false
                 },
                     new ProductDb
                 {
                     Id = Guid.Parse("F5065C3B-EE61-45C1-8F32-1B572C556A2D"),
                     Name = "Крутая тачка",
                     Description="Самая быстрая и дорогая из представленных",
                  //   Image = _imageClient.GetAll().Result.Where(x => x.ProductId==Guid.Parse("F5065C3B-EE61-45C1-8F32-1B572C556A2D")),
                  //   Price = _priceClient.GetAll().Result.Where(x => x.ProductId==Guid.Parse("F5065C3B-EE61-45C1-8F32-1B572C556A2D")),
                     CreatedDate=new DateTime(),
                     CreatedBy= new Guid(),
                     LastSavedDate=new DateTime(),
                     LastSavedBy=new Guid(),
                     IsDeleted=false

                 }
             };*/
    } 
}

