using System.IO;
using System.Threading.Tasks;

namespace AbpApiBook.MinIO
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(
            string objectName,
            Stream fileStream,
            long fileSize,
            string contentType);

        Task DeleteAsync(string objectName);

        Task<Stream> DownloadAsync(string objectName);
    }
}