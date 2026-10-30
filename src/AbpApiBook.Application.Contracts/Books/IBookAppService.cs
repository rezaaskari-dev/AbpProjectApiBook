using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AbpApiBook.Books
{
    public interface IBookAppService : IApplicationService
    {
        Task<List<BookDto>> GetListAsync();
        Task<BookDto> GetAsync(Guid id);

        Task<BookDto> CreateAsync(CreateBookDto input);

        Task<BookDto> UpdateAsync(Guid id, UpdateBookDto input);

        Task DeleteAsync(Guid id);

        Task<MemberExistsResultDto> CheckMemberExistsAsync(Guid memberId);
        //Task TestRabbitAsync(Guid memberId);
    }
}