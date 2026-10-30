using AbpApiBook.Entities;
using System;
using System.Collections.Generic;
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
        private readonly MemberCheckService _memberCheckService;
        public BookAppService(
     IRepository<Book, Guid> bookRepository,
     MemberCheckService memberCheckService)
        {
            _bookRepository = bookRepository;
            _memberCheckService = memberCheckService;
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

        private static BookDto MapToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedYear = book.PublishedYear
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

      
        
    }
}
