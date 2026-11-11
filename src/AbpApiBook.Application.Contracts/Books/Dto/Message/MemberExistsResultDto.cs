using System;

namespace AbpApiBook.Books.Dto.Message;

public class MemberExistsResultDto
{
    public Guid MemberId { get; set; }
    public bool Exists { get; set; }
}