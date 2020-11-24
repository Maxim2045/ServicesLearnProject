using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImageService.Repositories;

namespace ImageService.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<ImageRepository>> GetAll();

        Task<ImageRepository> Get(Guid id);

        Task Create(ImageRepository entity);

        Task Update(ImageRepository entity);

        Task Delete(Guid id);
    }
}