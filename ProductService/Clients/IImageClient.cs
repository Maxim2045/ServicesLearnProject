using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using ProductService.Models;

namespace ProductService.Clients
{
    public interface IImageClient
    {
        [Get("/images")]
        Task<IEnumerable<Image>> GetAll();
    }    
}