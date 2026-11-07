using Volo.Abp.Content;

namespace AbpApiBook.Books
{
    public class UploadBookImageDto
    {
        public IRemoteStreamContent File { get; set; } = default!;
    }
}