using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ImageServiceYandexApi.Clients;
using ImageServiceYandexApi.Interfaces;
using ImageServiceYandexApi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ImageServiceYandexApi
{
    //Сервис для работы с файлами на Яндекс Диске
    public class ImageService : IImageService
    {
        private readonly string _token;
        private readonly IYandexDriveImageClient _yandexDriveImageClient;
        private readonly IImageDbClient _imageDbClient;

        public ImageService(
            IConfiguration cfg,
            IYandexDriveImageClient yandexDriveImageClient, 
            IImageDbClient imageDbClient)
        {
            _token = cfg.GetValue<string>("YandexToken");
            _yandexDriveImageClient = yandexDriveImageClient ?? throw new ArgumentException(null, nameof(yandexDriveImageClient));
            _imageDbClient = imageDbClient ?? throw new ArgumentException(null, nameof(yandexDriveImageClient));
        }

        // Получить список изображений напрямую с яндекс диска.

        public async Task<IEnumerable<Image>> GetAll()
        {
            try
            {
                // Like JsonConvert.DeserializeObject<CustomImageModel> from string
                var data = await _yandexDriveImageClient.GetAll(_token);
                return data.Images;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        //Получить ссылку на изображение в яндекс диске из БД по Id записи.
        public async Task<string> GetImageUrl(Guid id)
        {
            try
            {
                var image = await _imageDbClient.Get(id);

                if (image == null)
                {
                    throw new ArgumentException($"Изображение с идентификатором {id} не найдено в БД.");
                }

                return image.Url;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        // Получить метаинфу изображения с Яндекс Диска с помощью Id записи в БД.
        public async Task<Image> GetImageMetaInfo(Guid id)
        {
            try
            {
                var imageInfoFromDb = await _imageDbClient.Get(id);
                var imageFromYandex = await _yandexDriveImageClient.Get(imageInfoFromDb.FullPathOnDisk,_token);

                if (imageFromYandex == null)
                {
                    throw new ArgumentException($"По ссылке {imageInfoFromDb.FullPathOnDisk} не найдено изображений в Яндекс Диске.");
                }

                return imageFromYandex;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        //Загрузить файл в Диск по URL.
        public async Task Upload(string imageUrl)
        {
            try
            {
                var fullPath = GetFullPathForImage(imageUrl);
                var response = await _yandexDriveImageClient.Upload(imageUrl, fullPath, _token);
                var deserializedResponse = JsonConvert.DeserializeObject<UploadResponseModel>(response);

                await _imageDbClient.Create(new ImageDbModel
                {
                    Id = new Guid(),
                    Url = deserializedResponse.Href,
                    FullPathOnDisk = fullPath,
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000000")
                });
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        // Сформировать полный путь для заливки файла в папку CustomImageFolder 
        private static string GetFullPathForImage(string imageUrl)
        {
            // Вытягиваем регулярками название изображения с расширением
            var imgNameWithExtensionPattern = new Regex(@"[\w-]+\.(jpg|jpeg|png|bmp|gif)");
            var imgNameWithExtension = imgNameWithExtensionPattern.Match(imageUrl).Value;

            // Если у файла нет расшриения и названия, льём с названием "image" без расширения
            if (string.IsNullOrEmpty(imgNameWithExtension))
            {
                imgNameWithExtension = "image";
            }

            return $"CustomImageFolder/{imgNameWithExtension}";
        }
    }
}