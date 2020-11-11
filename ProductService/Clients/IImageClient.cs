using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using WebApiService.Models;

namespace WebApiService.Clients
{
    public interface IImageClient
    {
        [Get("/images")]
        Task<IEnumerable<Image>> GetAll();
    }    
}