using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImageServiceYandexApi.Models;

namespace ImageServiceYandexApi.Interfaces
{
    public interface IImageService
    {

        //Получить список изображений
        Task<IEnumerable<Image>> GetAll();

        //Получить ссылку на изображение в яндекс диске из БД по Id записи
        Task<string> GetImageUrl(Guid id);

        // Получить метаинфу изображения с Яндекс Диска с помощью Id записи в БД.
        Task<Image> GetImageMetaInfo(Guid id);

        // Загрузить файл в Диск по URL.
        Task Upload(string imageUrl);
    }
}