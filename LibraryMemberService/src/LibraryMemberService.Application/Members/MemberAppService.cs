
using LibraryMemberService.Members;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LibraryMemberService.Members;

public class MemberAppService : ApplicationService, IMemberAppService
{
    private readonly IRepository<Member, Guid> _memberRepository;

    public MemberAppService(IRepository<Member, Guid> memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<MemberDto> GetAsync(Guid id)
    {
        var member = await _memberRepository.GetAsync(id);
        return MapToDto(member);
    }

    public async Task<PagedResultDto<MemberDto>> GetListAsync(
        PagedAndSortedResultRequestDto input)
    {
        var totalCount = await _memberRepository.GetCountAsync();

        var members = await _memberRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting ?? nameof(Member.FullName),
            includeDetails: false);

        var memberDtos = members.Select(MapToDto).ToList();

        return new PagedResultDto<MemberDto>(
            totalCount,
            memberDtos);
    }

    public async Task<MemberDto> CreateAsync(CreateUpdateMemberDto input)
    {
        var member = new Member(
            GuidGenerator.Create(),
            input.FullName,
            input.NationalCode,
            input.PhoneNumber);

        await _memberRepository.InsertAsync(member);

        return MapToDto(member);
    }

    public async Task<MemberDto> UpdateAsync(
        Guid id,
        CreateUpdateMemberDto input)
    {
        var member = await _memberRepository.GetAsync(id);

        member.SetDetails(
            input.FullName,
            input.NationalCode,
            input.PhoneNumber);

        await _memberRepository.UpdateAsync(member);

        return MapToDto(member);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _memberRepository.DeleteAsync(id);
    }

    private static MemberDto MapToDto(Member member)
    {
        return new MemberDto
        {
            Id = member.Id,
            FullName = member.FullName,
            NationalCode = member.NationalCode,
            PhoneNumber = member.PhoneNumber,
            MembershipDate = member.MembershipDate
        };
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _memberRepository.AnyAsync(
            member => member.Id == id);
    }
}

