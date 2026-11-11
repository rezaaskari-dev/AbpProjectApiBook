using Volo.Abp.Content;

namespace AbpApiBook.Books.Dto.MinIO
{
    public class UploadBookImageDto
    {
        public IRemoteStreamContent File { get; set; } = default!;
    }
}