using System.Threading.Tasks;
using ImageServiceYandexApi.Models;
using Refit;

namespace ImageServiceYandexApi.Clients
{
    public interface IYandexDriveImageClient
    {
 
        // Получить список изображений.
        [Get("/v1/disk/resources/files?media_type=image")]
        Task<Image> GetAll([Header("Authorization")] string authorization);

        // Получить изображение с яндекс диска по указанному полному пути.

        [Get("/v1/disk/resources?path={fullPath}")]
        Task<Image> Get(string fullPath, [Header("Authorization")] string authorization);

        // Загрузить файл в Диск по URL.
        [Post("/v1/disk/resources/upload?path={fullPath}&url={imageUrl}")]
        Task<string> Upload(string imageUrl, string fullPath, [Header("Authorization")] string authorization);
    }
}