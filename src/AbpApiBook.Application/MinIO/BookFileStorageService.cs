using AbpApiBook.MinIO.AbpApiBook.MinIO;
using Minio;
using Minio.DataModel.Args;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AbpApiBook.MinIO
{
    public class BookFileStorageService : IFileStorageService
    {
        private readonly IMinioClient _minioClient;
        private readonly MinioOptions _options;

        public BookFileStorageService(
            IMinioClient minioClient,
            Microsoft.Extensions.Options.IOptions<MinioOptions> options)
        {
            _minioClient = minioClient;
            _options = options.Value;
        }

        public async Task<string> UploadAsync(
            string objectName,
            Stream fileStream,
            long fileSize,
            string contentType)
        {
            if (string.IsNullOrWhiteSpace(objectName))
                throw new ArgumentException("Object name is required.");

            if (fileStream == null || !fileStream.CanRead)
                throw new ArgumentException("Invalid file stream.");

            if (fileSize <= 0)
                throw new ArgumentException("File size must be greater than zero.");

            var bucketExists = await _minioClient.BucketExistsAsync(
                new BucketExistsArgs()
                    .WithBucket(_options.BucketName));

            if (!bucketExists)
            {
                await _minioClient.MakeBucketAsync(
                    new MakeBucketArgs()
                        .WithBucket(_options.BucketName));
            }

            await _minioClient.PutObjectAsync(
                new PutObjectArgs()
                    .WithBucket(_options.BucketName)
                    .WithObject(objectName)
                    .WithStreamData(fileStream)
                    .WithObjectSize(fileSize)
                    .WithContentType(contentType));

            return objectName;
        }

        public async Task DeleteAsync(string objectName)
        {
            await _minioClient.RemoveObjectAsync(
                new RemoveObjectArgs()
                    .WithBucket(_options.BucketName)
                    .WithObject(objectName));
        }

        public async Task<Stream> DownloadAsync(string objectName)
        {
            if (string.IsNullOrWhiteSpace(objectName))
                throw new ArgumentException("Object name is required.");

            var memoryStream = new MemoryStream();

            try
            {
                await _minioClient.GetObjectAsync(
                    new GetObjectArgs()
                        .WithBucket(_options.BucketName)
                        .WithObject(objectName)
                        .WithCallbackStream(stream =>
                        {
                            stream.CopyTo(memoryStream);
                        }));

                memoryStream.Position = 0;

                return memoryStream;
            }
            catch
            {
                memoryStream.Dispose();
                throw;
            }
        }
    }
}