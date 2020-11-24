using ProductService.Models;
using ProductService.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Interfaces
{
   public interface IProductService
    {
        Task<IEnumerable<ProductDb>> GetAll();

        Task<ProductDb> Get(Guid id);

        Task Create(ProductDb entity);

        Task Update(ProductDb entity);

        Task Delete(Guid id);
    }
}
