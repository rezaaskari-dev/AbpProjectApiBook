using System.Threading.Tasks;
using Minio;
using Minio.DataModel.Args;

namespace AbpApiBook.MinIO
{
    public class MinioHealthCheckService
    {
        private readonly IMinioClient _minioClient;

        public MinioHealthCheckService(IMinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        public async Task<bool> IsConnectedAsync()
        {
            var buckets = await _minioClient.ListBucketsAsync();

            return buckets.Buckets != null;
        }
    }
}