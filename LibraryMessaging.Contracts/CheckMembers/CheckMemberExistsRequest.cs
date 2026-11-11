using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMessaging.Contracts.Members
{
    public class CheckMemberExistsRequest
    {
        public Guid MemberId { get; set; }

        public string CorrelationId { get; set; } = string.Empty;
    }
}