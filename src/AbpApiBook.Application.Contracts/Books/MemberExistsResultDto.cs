using System;

namespace AbpApiBook.Books;

public class MemberExistsResultDto
{
    public Guid MemberId { get; set; }
    public bool Exists { get; set; }
}