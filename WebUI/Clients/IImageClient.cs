using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Clients
{
   public interface IImageClient
    {
        [Get("/images")]
        Task<IEnumerable<Image>> GetAll();
    }
}
