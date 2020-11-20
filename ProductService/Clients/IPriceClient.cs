using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using ProductService.Models;

namespace ProductService.Clients
{
    public interface IPriceClient
    {
        [Get("/price")]
        Task<IEnumerable<Price>> GetAll();
    }
}