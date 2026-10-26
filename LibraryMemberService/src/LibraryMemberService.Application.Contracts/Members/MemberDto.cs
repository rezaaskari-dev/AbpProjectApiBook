using System;
using Volo.Abp.Application.Dtos;

namespace LibraryMemberService.Members;

public class MemberDto : EntityDto<Guid>
{
    public string FullName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime MembershipDate { get; set; }
}