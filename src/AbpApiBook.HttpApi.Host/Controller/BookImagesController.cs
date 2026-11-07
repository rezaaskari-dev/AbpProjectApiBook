using AbpApiBook.Entities;
using AbpApiBook.MinIO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace AbpApiBook.Controllers
{
    [Route("api/app/books")]
    [ApiController]
    [AllowAnonymous]
    public class BookImagesController : ControllerBase
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IFileStorageService _fileStorageService;

        public BookImagesController(
            IRepository<Book, Guid> bookRepository,
            IFileStorageService fileStorageService)
        {
            _bookRepository = bookRepository;
            _fileStorageService = fileStorageService;
        }

        [HttpGet("{id:guid}/image")]
        public async Task<IActionResult> GetImage(Guid id)
        {
            var book = await _bookRepository.FindAsync(id);

            if (book == null)
                return NotFound("Book not found.");

            if (string.IsNullOrWhiteSpace(book.CoverImageObjectKey))
                return NotFound("Book image not found.");

            var stream = await _fileStorageService.DownloadAsync(
                book.CoverImageObjectKey);

            var extension = Path.GetExtension(
                book.CoverImageObjectKey).ToLowerInvariant();

            var contentType = extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };

            return File(stream, contentType);
        }
    }
}