using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMemberService.Members
{
    public class MemberExistsDto
    {
        public Guid Id { get; set; }

        public bool Exists { get; set; }
    }
}