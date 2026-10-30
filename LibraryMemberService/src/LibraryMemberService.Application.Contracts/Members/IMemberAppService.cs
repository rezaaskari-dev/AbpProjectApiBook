 using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LibraryMemberService.Members;

public interface IMemberAppService : IApplicationService
{
    Task<MemberDto> GetAsync(Guid id);

    Task<PagedResultDto<MemberDto>> GetListAsync(
        PagedAndSortedResultRequestDto input);

    Task<MemberDto> CreateAsync(CreateUpdateMemberDto input);

    Task<MemberDto> UpdateAsync(
        Guid id,
        CreateUpdateMemberDto input);

    Task DeleteAsync(Guid id);

    //Task<MemberExistsDto> ExistsAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);

}