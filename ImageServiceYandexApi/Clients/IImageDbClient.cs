using System;
using System.Threading.Tasks;
using ImageServiceYandexApi.Models;
using Refit;

namespace ImageServiceYandexApi.Clients
{
    public interface IImageDbClient
    {
        [Get("/api/images/{id}")]
        Task<ImageDbModel> Get(Guid id);

        [Post("/api/images")]
        Task Create(ImageDbModel image);
    }
}