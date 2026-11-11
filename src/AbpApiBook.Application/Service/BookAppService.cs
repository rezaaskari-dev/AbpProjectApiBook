using AbpApiBook.Books.Dto;
using AbpApiBook.Books.Dto.Message;
using AbpApiBook.Books.Dto.MinIO;
using AbpApiBook.Books.Message;
using AbpApiBook.Entities;
using AbpApiBook.MinIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace AbpApiBook.Books
{
    public class BookAppService : ApplicationService, IBookAppService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly MemberServiceClient _memberCheckService;

        private readonly IFileStorageService _fileStorageService;

        public BookAppService(
     IRepository<Book, Guid> bookRepository,
     MemberServiceClient memberCheckService, IFileStorageService fileStorageService)
        {
            _bookRepository = bookRepository;
            _memberCheckService = memberCheckService;
            _fileStorageService = fileStorageService;

        }



        public async Task<List<BookDto>> GetListAsync()
        {
            var books = await _bookRepository.GetListAsync();

            return books.Select(book => MapToDto(book)).ToList();
        }


        public async Task<BookDto> GetAsync(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);

            return MapToDto(book);
        }

        public async Task<BookDto> CreateAsync(CreateBookDto input)
        {
            var book = new Book(
                GuidGenerator.Create(),
                input.Title,
                input.Author,
                input.PublishedYear
            );

            await _bookRepository.InsertAsync(book, autoSave: true);

            return MapToDto(book);
        }

        public async Task<BookDto> UpdateAsync(
            Guid id,
            UpdateBookDto input)
        {
            var book = await _bookRepository.GetAsync(id);

            book.Update(
                input.Title,
                input.Author,
                input.PublishedYear
            );

            await _bookRepository.UpdateAsync(book, autoSave: true);

            return MapToDto(book);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id, autoSave: true);
        }

        private BookDto MapToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedYear = book.PublishedYear,

                ImageUrl = book.CoverImageObjectKey == null
                    ? null
                    : $"https://localhost:5001/api/app/books/{book.Id}/image"
            };
        }

        //public async Task TestRabbitAsync(Guid memberId)
        //{
        //    await _testRabbitPublisher.PublishAsync(memberId);
        //}

        public async Task<MemberExistsResultDto> CheckMemberExistsAsync(Guid memberId)
        {
            var exists = await _memberCheckService.CheckMemberAsync(memberId);

            return new MemberExistsResultDto
            {
                MemberId = memberId,
                Exists = exists
            };
        }

        public async Task UploadBookImageAsync(
      Guid bookId,
      UploadBookImageDto input)
        {
            var book = await _bookRepository.GetAsync(bookId);

            if (input.File == null)
            {
                throw new Exception("Image file is required.");
            }

            var fileName = input.File.FileName;
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            if (!allowedExtensions.Contains(extension))
            {
                throw new Exception("Invalid image format.");
            }

            var objectKey = $"books/{bookId}/cover{extension}";

            var stream = input.File.GetStream();

            await _fileStorageService.UploadAsync(
                objectKey,
                stream,
                input.File.ContentLength ?? stream.Length,
                input.File.ContentType
            );

            book.SetCoverImage(objectKey);

            await _bookRepository.UpdateAsync(book, autoSave: true);
        }
    }
}
